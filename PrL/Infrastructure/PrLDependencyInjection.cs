using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using BLL.Interfaces;
using Ninject.Modules;

namespace PrL.Infrastructure
{
    public class PrLDependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainService>().To<CoreServices>();
        }
    }
}