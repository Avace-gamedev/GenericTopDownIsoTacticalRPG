using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.World.Map
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        /// <summary>
        /// Layer order of the player's layer. Background elements will be on layers with a smaller order, and front elements will be on layers with
        /// a bigger order.
        /// There might a layer with PlayerLayerOrder order in the Map, its elements will be elements that can be either in front, or behind the player
        /// depending on it Y position. 
        /// </summary>
        public int PlayerLayerOrder { get; private set; }

        private readonly List<MapLayer> _layers = new List<MapLayer>();

        private Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        private void Add(MapLayer layer)
        {
            if (_layers.Any(l => l.Order == layer.Order))
            {
                throw new InvalidOperationException($"There is already a layer with order {layer.Order}.");
            }
            
            _layers.Add(layer);
        }

        public IEnumerable<MapLayer> Layers()
        {
            return _layers.OrderBy(l => l.Order);
        }

        public static Map CreateDevMap()
        {
            Map map = new Map(50, 50)
            {
                PlayerLayerOrder = 1
            };

            MapLayer bottomLayer = new MapLayer(map, "base", 0);

            for (int x = 0; x < 50; x++)
            for (int y = 0; y < 50; y++)
            {
                bottomLayer.Set(x, y, "default");
            }
            
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int x = random.Next(0, 50);
                int y = random.Next(0, 50);
                bottomLayer.Set(x, y, "grass");
            }

            MapLayer topLayer = new MapLayer(map, "base", 1);
            
            for (int i = 0; i < 25; i++)
            {
                int x = random.Next(0, 50);
                int y = random.Next(0, 50);
                topLayer.Set(x, y, "wall");
            }

            map.Add(bottomLayer);
            map.Add(topLayer);
            
            return map;
        }
    }
}