using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using PrL.Models;

namespace PrL.Controllers
{
    [Authorize]
    public class ApplicationController : ApiController
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().UserServices;
            }
        }

        private IRepositoryBll<ApplicationDTO> ApplicationsService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().ApplicationServices;
            }
        }

        private IRepositoryBll<StatusDTO> StatusService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().StatusServices;
            }
        }

        public IHttpActionResult GetAllApplications()
        {
            if (User.IsInRole("admin"))
            {
                var resultList = CollectInfoAboutApplications(ApplicationsService.GetAll());
                return Ok(resultList);
            }
            if (User.IsInRole("manager"))
            {
                var currentUser = UserService.GetAll().Where(x => x.UserName == User.Identity.Name).First();
                var statusOfApplicationToShow = StatusService.Find(y => y.Name == "new").First();
                IEnumerable<ApplicationDTO> appList = ApplicationsService.Find(x => x.StatusId == statusOfApplicationToShow.Id || x.ExecutorId == currentUser.Id);
                var resultList = CollectInfoAboutApplications(appList);
                return Ok(resultList);
            }
            if (User.IsInRole("user"))
            {
                var currentUser = UserService.GetAll().Where(x => x.UserName == User.Identity.Name).First();
                IEnumerable<ApplicationDTO> appList = ApplicationsService.Find(x => x.UserOwnerId == currentUser.Id);
                var resultList = CollectInfoAboutApplications(appList);
                return Ok(resultList);
            }
            return Content(HttpStatusCode.Forbidden, "You have no rights for this content");
        }

        public IHttpActionResult Get(string id)
        {
            ApplicationDTO appDTO = ApplicationsService.Get(id);
            if (appDTO == null)
            {
                return NotFound();
            }
            return Ok(appDTO);
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public IHttpActionResult CreateApplication(ApplicationCreateModel app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationDTO newApp = new ApplicationDTO()
                                    {
                                        ApplicationName = app.ApplicationName,
                                        UserOwnerId = app.UserOwnerId
                                    };
            try
            {
                ApplicationsService.Create(newApp);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = newApp.Id }, newApp);
        }

        [HttpPut]
        public IHttpActionResult EditApplication(ApplicationEditModel app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationDTO editedApp = new ApplicationDTO()
                                        {
                                            Id = app.Id,
                                            ApplicationName = app.ApplicationName,
                                            StatusId = app.StatusId,
                                            ExecutorId = app.ExecutorId
                                        };
            try
            {
                ApplicationsService.Edit(editedApp);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok(editedApp);
        }

        [Authorize(Roles = "admin,user")]
        public IHttpActionResult DeleteApplication(string id)
        {
            try
            {
                ApplicationsService.Delete(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        private IEnumerable<ApplicationGetModel> CollectInfoAboutApplications(IEnumerable<ApplicationDTO> inputList)
        {
            var resultList = new List<ApplicationGetModel>();
            foreach (var item in inputList)
            {
                var newResultItem = new ApplicationGetModel
                {
                    Id = item.Id,
                    ApplicationName = item.ApplicationName,
                    DateOfCreationApplication = item.DateOfCreateApplication,
                    StatusId = item.StatusId,
                    StatusName = StatusService.Get(item.StatusId).Name,
                    UserOwnerId = item.UserOwnerId,
                    UserOwnerName = UserService.Get(item.UserOwnerId).UserName,
                    ExecutorId = item.ExecutorId,
                    DateOfChangeStatus = item.DateOfChangeStatus
                };
                if (item.ExecutorId != null)
                {
                    newResultItem.ExecutorName = UserService.Get(item.ExecutorId).Name;
                }
                resultList.Add(newResultItem);
            }
            return resultList;
        }
    }
}