using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BLL;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;

namespace PrL.Controllers
{
    public class ValuesController : ApiController
    {
        //private IMainService _coreService;

        //public ValuesController(IMainService coreService)
        //{
        //    _coreService = coreService;
        //}

        private IUserService UserService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<IMainService>().UserServices;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        //public ValuesController()
        //{
        //    _coreService = new CoreServices();
        //}
        [Authorize]
        // GET api/values
        public IEnumerable<UserDTO> Get()
        {

            return UserService.GetAllUsers();
            //return _coreService.UserServices.GetAllUsers();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
