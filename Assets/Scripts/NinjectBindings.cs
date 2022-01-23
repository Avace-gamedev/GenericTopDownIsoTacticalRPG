using Backend.Kernel.Logging;
using Ninject.Modules;
using Scripts.Logging;
using Scripts.World.Tiles;

namespace Scripts
{
    public class NinjectBindings: NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggerProvider>().To<UnityLoggerProvider>();
            
            Bind<IRootGameObjectProvider>().ToConstant(RootGameObject.Instance);
            
            Bind<ITileProvider>().To<ColoredTileProvider>().InSingletonScope();
        }
    }
}