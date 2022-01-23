using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle.Attributes;
using Backend.Kernel.Logging;
using UnityEngine;
using ILogger = Backend.Kernel.Logging.ILogger;

namespace Frontend.World
{
    /// <summary>
    /// Provides access and control over the currently rendered tilemap
    /// </summary>
    public static class TilemapManager
    {
        private static readonly ILogger Logger = Injector.Get<ILoggerProvider>().GetLogger(nameof(TilemapManager));
        
        [OnAppStarted]
        public static void AppStarted()
        {
            MonoBehaviour monoBehaviour = Injector.Get<IMonoBehaviourProvider>().GetMonoBehaviour();

            Logger.Info(monoBehaviour.name);
        }
    }
}