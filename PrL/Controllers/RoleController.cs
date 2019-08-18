using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using PrL.Models;

namespace PrL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : ApiController
    {
        private IRepositoryBll<RoleDTO> RoleService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().RoleServices;
            }
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().UserServices;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(RoleService.GetAll());
        }

        [HttpGet]
        public IHttpActionResult GetRole(string id)
        {
            RoleDTO role = RoleService.Get(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public IHttpActionResult CreateRole(RoleCreateModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RoleDTO newRole = new RoleDTO
                                    {
                                        Name = role.RoleName
                                    };
            try
            {
                RoleService.Create(newRole);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = newRole.Id }, newRole);
        }

        [HttpPut]
        public IHttpActionResult EditRole(RoleEditModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RoleDTO editedRole = new RoleDTO
                                    {
                                        Id = role.Id,
                                        Name =role.RoleName
                                    };
            try
            {
                RoleService.Edit(editedRole);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok(editedRole);
        }

        [HttpDelete]
        public IHttpActionResult DeleteRole(string id)
        {
            try
            {
                RoleService.Delete(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [Route("api/role/{id}/users")]
        [HttpGet]
        public IHttpActionResult GetUsersByRole(string id)
        {
            var role = RoleService.Get(id);
            if (role == null)
            {
                return NotFound();
            }
            var users = UserService.GetAll();
            var list = new List<UserDTO>();
            foreach (var user in users)
            {
                if (user.Roles == null)
                {
                    continue;
                }
                if (user.Roles.Contains(role.Name))
                {
                    list.Add(user);
                }
            }
            return Ok(list);
        }
    }
}