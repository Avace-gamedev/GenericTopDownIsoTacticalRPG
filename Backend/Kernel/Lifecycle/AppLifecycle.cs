using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle.Attributes;
using Backend.Kernel.Logging;
using Backend.Kernel.Utils;

namespace Backend.Kernel.Lifecycle
{
    /// <summary>
    /// Lifecycle steps:
    ///     - AppStarting
    ///     - AppStarted
    ///     - .... App is running, no hooks
    /// </summary>
    /// <remarks>
    ///     Dependency injection MUST be setup before calling any of these hooks because this class uses a logger.
    /// </remarks>
    public static class AppLifecycle
    {
        private static readonly ILogger Logger = Injector.Get<ILoggerProvider>().GetLogger(nameof(AppLifecycle));

        public static void AppStarting()
        {
            Logger.Info("Running AppStarting hooks");
            RunLifecycleMethods(typeof(OnAppStartingAttribute));
        }

        public static void AppStarted()
        {
            Logger.Info("Running AppStarted hooks");
            RunLifecycleMethods(typeof(OnAppStartedAttribute));
        }
        
        private static void RunLifecycleMethods(Type lifecycleAttributeType)
        {
            if (!lifecycleAttributeType.IsSubclassOf(typeof(Attribute)))
            {
                throw new InvalidOperationException(
                    $"{lifecycleAttributeType} is not an instance of {nameof(Attribute)}, it cannot be used as lifecycle attribute type");
            }

            IEnumerable<MethodInfo> methods =
                AttributesUtils.GetAllMethodsWithAttribute(lifecycleAttributeType, typeof(LifecycleHookAttribute));

            foreach (MethodInfo method in methods)
            {
                if (!method.IsStatic)
                {
                    throw new InvalidOperationException($"Method with attribute {lifecycleAttributeType} should be static");
                }

                if (!method.GetParameters().Any())
                {
                    throw new InvalidOperationException($"Method with attribute {lifecycleAttributeType} MUST NOT have parameters");
                }

                Logger.Debug($"Running {method.GetSignature()}");
                method.Invoke(null, Array.Empty<object>());
            }
        }
    }
}