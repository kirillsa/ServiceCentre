using System.Net.Http;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PrL.Models;
using Microsoft.Owin.Security.Cookies;
using BLL.Infrastructure;

namespace PrL.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        //private IMainService _userService;

        //public AccountController(IMainService mainServices)
        //{
        //    appMngr = mainServices;

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

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new UserDTO()
            {
                UserName = model.Name,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Roles = "user"
            };
            OperationDetails result = UserService.Create(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        //[AllowAnonymous]
        //[Route("Login")]
        //public IHttpActionResult Login(UserLoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
        //        ClaimsIdentity claim = UserService.Authenticate(userDto, DefaultAuthenticationTypes.ApplicationCookie);
        //        if (claim == null)
        //        {
        //            ModelState.AddModelError("", "Неверный логин или пароль.");
        //        }
        //        else
        //        {
        //            AuthenticationManager.SignOut();
        //            AuthenticationManager.SignIn(new AuthenticationProperties
        //            {
        //                IsPersistent = true
        //            }, claim);
        //            return Ok(userDto);
        //        }
        //    };
        //    return NotFound();
        //}

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(UserService.GetAll());
        }

        [HttpGet]
        public IHttpActionResult GetUser(string id) {
            var userToGet = UserService.Get(id);
            if (userToGet == null)
            {
                return NotFound();
            }
            return Ok(userToGet);
        }
    }
}
