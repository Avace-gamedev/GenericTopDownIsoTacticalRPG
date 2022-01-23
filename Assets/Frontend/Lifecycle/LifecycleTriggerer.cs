using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle;
using UnityEngine;

namespace Frontend.Lifecycle
{
    public static class LifecycleTriggerer
    {
        [RuntimeInitializeOnLoadMethod]
        public static void OnLoad()
        {
            // DI MUST be initialized first
            Injector.Initialize();
            
            AppLifecycle.AppStarting();
            AppLifecycle.AppStarted();
        }
    }
}