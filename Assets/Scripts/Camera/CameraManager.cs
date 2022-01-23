using System.Collections.Generic;
using System.Linq;
using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle.Attributes;
using Backend.Kernel.Logging;
using Scripts.Utils;
using UnityEngine;
using ILogger = Backend.Kernel.Logging.ILogger;

namespace Scripts.Camera
{
    public class CameraManager
    {
        private static readonly ILogger Logger = Injector.Get<ILoggerProvider>().GetLogger(nameof(CameraManager));
        private static readonly IRootGameObjectProvider RootProvider = Injector.Get<IRootGameObjectProvider>();

        private static UnityEngine.Camera _camera;
        
        //[OnAppStarted]
        public static void AppStarted()
        {
            DestroyAllCameras();
            
            CreateCamera();
        }
        
        private static void DestroyAllCameras()
        {
            IEnumerable<UnityEngine.Camera> grids = Object.FindObjectsOfType<UnityEngine.Camera>();
            foreach (UnityEngine.Camera camera in grids.ToArray())
            {
                Object.Destroy(camera.gameObject);
            }
        }

        private static void CreateCamera()
        {
            GameObject cameraObject = RootProvider.Root.CreateChildWithComponents("Camera", typeof(UnityEngine.Camera));
            _camera = cameraObject.GetComponent<UnityEngine.Camera>();

            _camera.orthographic = true;
        }
    }
}