using UnityEngine.Tilemaps;

namespace Scripts.World.Tiles
{
    public interface ITileProvider
    {
        TileBase Get(string name);
    }
}