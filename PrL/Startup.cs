using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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
        public static HttpConfiguration HTTP_Config { get; private set; }
        private IKernel kernel;

        public void Configuration(IAppBuilder app)
        {
            //--------------Cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //--------------DI
            kernel = NinjectWebCommon.CreateKernel();
            app.UseNinjectMiddleware(() => kernel);

            //--------------Service
            app.CreatePerOwinContext<IMainService>(() => kernel.Get<IMainService>());

            //-------------Auth with Token
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };
            // Enable the application to use bearer tokens to authenticate users
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
