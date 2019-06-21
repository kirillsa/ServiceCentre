using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Modules;
using PrL.Infrastructure;
using BLL.Infrastructure;
using Ninject;

namespace PrL
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule prLDI = new PrLDependencyInjection();
            NinjectModule bLLDI = new BllDependencyInjection();
            var kernel = new StandardKernel(prLDI, bLLDI);
            var ninjectResolver = new NinjectDependencyResolver(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;

        }
    }
}
