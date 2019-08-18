using DAL;
using DAL.Interfaces;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class BllDependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UOW>();
        }
    }
}
