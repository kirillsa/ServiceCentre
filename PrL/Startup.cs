using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Owin;
using PrL.App_Start;
using PrL.Providers;

[assembly: OwinStartup(typeof(PrL.Startup))]

namespace PrL
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        private IKernel kernel;
        //internal static IDataProtectionProvider DataProtectionProvider;   //???
        public static HttpConfiguration HTTP_Config { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //--------------DI
            kernel = NinjectWebCommon.CreateKernel();
            app.UseNinjectMiddleware(() => kernel);

            //--------------Context for AppUserMngr
            //DataProtectionProvider = app.GetDataProtectionProvider();   //???

            //--------------Service
            app.CreatePerOwinContext<IMainService>(() => kernel.Get<IMainService>());

            //-------------Cookie Auth
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //});

            //-------------Auth with Token
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(2),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };
            //// Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //-------------HttpConfig for WebAPI
            HTTP_Config = new HttpConfiguration();
            WebApiConfig.Register(HTTP_Config);
            app.UseWebApi(HTTP_Config);
            
            //-----------Test OWIN
            //app.UseWelcomePage();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
