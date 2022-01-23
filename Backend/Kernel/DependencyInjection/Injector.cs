using System;
using Ninject;

namespace Backend.Kernel.DependencyInjection
{
    public static class Injector
    {
        private static IKernel _kernel;
        
        public static void Initialize()
        {
            _kernel = new StandardKernel();
            _kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
        }
        
        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static void Bind<TInterface, TType>() where TType: TInterface
        {
            _kernel.Bind<TInterface>().To<TType>();
        }
    }
}