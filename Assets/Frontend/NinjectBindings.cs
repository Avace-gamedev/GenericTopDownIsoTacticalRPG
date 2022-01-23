using Backend.Kernel.Logging;
using Frontend.Logging;
using Ninject.Modules;

namespace Frontend
{
    public class NinjectBindings: NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggerProvider>().To<UnityLoggerProvider>();
        }
    }
}