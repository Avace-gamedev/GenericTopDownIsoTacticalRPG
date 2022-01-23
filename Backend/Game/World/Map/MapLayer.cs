namespace Game.World.Map
{
    public class MapLayer
    {
        public Map Map { get; private set; }
        public string Name { get; private set; }
        public int Order { get; set; }

        private readonly string[] _tiles;

        public MapLayer(Map map, string name, int order)
        {
            Map = map;
            Name = name;
            Order = order;
            _tiles = new string[map.Height * map.Width];
        }

        public string Get(int x, int y)
        {
            int index = IndexOfCoords(x, y);
            return _tiles[index];
        }
        
        public void Set(int x, int y, string tileName)
        {
            int index = IndexOfCoords(x, y);
            _tiles[index] = tileName;
        }

        private int IndexOfCoords(int x, int y)
        {
            return x + y * Map.Width;
        }
    }
}