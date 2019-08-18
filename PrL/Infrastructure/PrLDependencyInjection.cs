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