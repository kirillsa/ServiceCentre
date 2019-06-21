using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using BLL.Services;
using BLL.Interfaces;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using BLL;
using Ninject.Modules;
using PrL.Infrastructure;
using BLL.Infrastructure;
using Ninject;

[assembly: OwinStartup(typeof(PrL.Startup))]

namespace PrL
{
    public partial class Startup
        {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureWebApi(httpConfig);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            

            IMainService _coreService = new CoreServices(new UnitOfWork());
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(_coreService.);
            app.CreatePerOwinContext<IUserService>(_coreService.UserServices);
            //app.CreatePerOwinContext<ApplicationUserManager>(_coreService.UserServices.AppUserManager);
            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here

        }


        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}