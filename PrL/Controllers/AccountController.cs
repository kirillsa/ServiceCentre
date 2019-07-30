using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PrL.Models;
using Microsoft.Owin.Security.Cookies;
using System.Threading.Tasks;
using BLL.Infrastructure;

namespace PrL.Controllers
{
    [Authorize]
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
        public async Task<IHttpActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserDTO() { UserName = model.Email, Email = model.Email, Password = model.Password, Roles = "manager" };

            OperationDetails result = UserService.Create(user);

            if (!result.Succedeed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("Login")]
        public void Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = UserService.Authenticate(userDto, DefaultAuthenticationTypes.ApplicationCookie);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    //return RedirectToAction("Index", "Home");
                }
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(UserService.GetAllUsers());
        }

        public IHttpActionResult GetUser(string id) {
            var userToGet = UserService.GetUser(id);
            if (userToGet == null)
            {
                return NotFound();
            }
            return Ok(userToGet);
        }

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    if (error != null)
        //    {
        //        return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
        //    }

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return new ChallengeResult(provider, this);
        //    }

        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    if (externalLogin == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (externalLogin.LoginProvider != provider)
        //    {
        //        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //        return new ChallengeResult(provider, this);
        //    }

        //    UserDTO user = UserService.Find(new UserLoginInfo(externalLogin.LoginProvider,
        //        externalLogin.ProviderKey));

        //    bool hasRegistered = user != null;

        //    if (hasRegistered)
        //    {
        //        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //        ClaimsIdentity oAuthIdentity = UserService.Authenticate(user,
        //           OAuthDefaults.AuthenticationType);
        //        ClaimsIdentity cookieIdentity = UserService.Authenticate(user,
        //            CookieAuthenticationDefaults.AuthenticationType);

        //        AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        //        AuthenticationManager.SignIn(properties, oAuthIdentity, cookieIdentity);
        //    }
        //    else
        //    {
        //        IEnumerable<Claim> claims = externalLogin.GetClaims();
        //        ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        //        AuthenticationManager.SignIn(identity);
        //    }

        //    return Ok();
        //}

        //private class ExternalLoginData
        //{
        //    public string LoginProvider { get; set; }
        //    public string ProviderKey { get; set; }
        //    public string UserName { get; set; }

        //    public IList<Claim> GetClaims()
        //    {
        //        IList<Claim> claims = new List<Claim>();
        //        claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

        //        if (UserName != null)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
        //        }

        //        return claims;
        //    }

        //    public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        //    {
        //        if (identity == null)
        //        {
        //            return null;
        //        }

        //        Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        //        if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
        //            || String.IsNullOrEmpty(providerKeyClaim.Value))
        //        {
        //            return null;
        //        }

        //        if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
        //        {
        //            return null;
        //        }

        //        return new ExternalLoginData
        //        {
        //            LoginProvider = providerKeyClaim.Issuer,
        //            ProviderKey = providerKeyClaim.Value,
        //            UserName = identity.FindFirstValue(ClaimTypes.Name)
        //        };
        //    }
        //}

        
    }
}
