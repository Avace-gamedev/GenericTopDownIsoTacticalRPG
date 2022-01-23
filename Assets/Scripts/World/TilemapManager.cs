using System.Collections.Generic;
using System.Linq;
using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle.Attributes;
using Backend.Kernel.Logging;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using ILogger = Backend.Kernel.Logging.ILogger;

namespace Scripts.World
{
    /// <summary>
    /// Provides access and control over the currently rendered tilemap
    /// </summary>
    public static class TilemapManager
    {
        private static readonly ILogger Logger = Injector.Get<ILoggerProvider>().GetLogger(nameof(TilemapManager));
        private static readonly IRootGameObjectProvider RootProvider = Injector.Get<IRootGameObjectProvider>();

        private static Grid _grid;
        private static Tilemap _tilemap;
        
        [OnAppStarted]
        public static void AppStarted()
        {
            DestroyAllGrids();

            SetupProjectSettings();
            
            CreateGrid();
            CreateTilemap();
        }

        private static void SetupProjectSettings()
        {
            GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
            GraphicsSettings.transparencySortAxis = new Vector3(0, 1, -0.26f);
        }

        private static void DestroyAllGrids()
        {
            IEnumerable<Grid> grids = Object.FindObjectsOfType<Grid>();
            foreach (Grid grid in grids.ToArray())
            {
                Object.Destroy(grid);
            }
        }

        private static void CreateGrid()
        {
            GameObject gridObject = RootProvider.Root.CreateChildWithComponents("Grid", typeof(Grid));
            _grid = gridObject.GetComponent<Grid>();

            _grid.cellSize = new Vector3(1, 0.5f, 1);
            _grid.cellLayout = GridLayout.CellLayout.Isometric;
        }

        private static void CreateTilemap()
        {
            GameObject tilemapObject = _grid.gameObject.CreateChildWithComponents("Tilemap", typeof(Tilemap));
            _tilemap = tilemapObject.GetComponent<Tilemap>();
        }
    }
}