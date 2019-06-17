using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class BllDIClass : NinjectModule
    {
         public override void Load()
        {
            Bind<IUnitOfWork>().To<UOW>();
        }
    }
}
