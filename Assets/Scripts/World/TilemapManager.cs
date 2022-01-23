using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Kernel.DependencyInjection;
using Backend.Kernel.Lifecycle.Attributes;
using Backend.Kernel.Logging;
using Scripts.Utils;
using Scripts.World.Tiles;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using ILogger = Backend.Kernel.Logging.ILogger;
using Object = UnityEngine.Object;

namespace Scripts.World
{
    /// <summary>
    /// Provides access and control over the currently rendered tilemap
    /// </summary>
    public static class TilemapManager
    {
        private static readonly ILogger Logger = Injector.Get<ILoggerProvider>().GetLogger(nameof(TilemapManager));
        private static readonly IRootGameObjectProvider RootProvider = Injector.Get<IRootGameObjectProvider>();
        private static readonly ITileProvider TileProvider = Injector.Get<ITileProvider>();

        private static Grid _grid;
        private static readonly HashSet<Tilemap> _activeTilemaps = new HashSet<Tilemap>();

        [OnAppStarted]
        public static void AppStarted()
        {
            DestroyAllGrids();

            SetupProjectSettings();

            CreateGrid();

            Load();
        }

        public static void Load()
        {
            if (!_grid)
            {
                throw new InvalidOperationException("Cannot load tilemaps before grid is created");
            }

            TileBase defaultTile = TileProvider.Get("default");

            Logger.Info(defaultTile ? defaultTile.ToString() : "NULL");

            Tilemap tilemap = CreateTilemap();
            tilemap.SetTile(new Vector3Int(), defaultTile);

            tilemap.RefreshAllTiles();
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
                Object.Destroy(grid.gameObject);
            }
        }

        private static void CreateGrid()
        {
            GameObject gridObject = RootProvider.Root.CreateChildWithComponents("Grid", typeof(Grid));
            _grid = gridObject.GetComponent<Grid>();

            _grid.cellSize = new Vector3(1, 0.5f, 1);
            _grid.cellLayout = GridLayout.CellLayout.Isometric;
        }

        private static Tilemap CreateTilemap()
        {
            GameObject tilemapObject = _grid.gameObject.CreateChildWithComponents("Tilemap", typeof(Tilemap), typeof(TilemapRenderer));
            Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
            _activeTilemaps.Add(tilemap);

            TilemapRenderer renderer = tilemapObject.GetComponent<TilemapRenderer>();
            renderer.mode = TilemapRenderer.Mode.Individual;
            
            return tilemap;
        }
    }
}