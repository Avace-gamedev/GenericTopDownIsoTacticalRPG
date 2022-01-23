using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle;
using UnityEngine;

namespace Scripts.Lifecycle
{
    public static class LifecycleTriggerer
    {
        [RuntimeInitializeOnLoadMethod]
        public static void OnLoad()
        {
            Debug.Log("Initialize DependencyInjection");
            // DI MUST be initialized first
            Injector.Initialize();
            
            Debug.Log("Run lifecycle hooks");

            AppLifecycle.AppStarting();
            AppLifecycle.AppStarted();
        }
    }
}