using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using Ninject.Modules;

namespace BLL_Console.infrastructure
{
    public class BLL_DI : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainService>().To<CoreServices>();
        }
    }
}
