using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.World.Tiles
{
    public class ColoredTileProvider : ITileProvider
    {
        private static readonly TileBase _default;
        private static readonly Dictionary<string, TileBase> _namedTiles = new Dictionary<string, TileBase>();

        static ColoredTileProvider()
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = "default_tile";
            tile.colliderType = Tile.ColliderType.None;
            tile.hideFlags = HideFlags.DontSave;

            Texture2D texture = CreateSolidIsometricTile(Color.white);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 256, 128), new Vector2(0.5f, 0.5f), 256);
            sprite.name = "default_tile_sprite";

            tile.sprite = sprite;
            
            _default = tile;
        }

        public TileBase Get(string name)
        {
            return _namedTiles.ContainsKey(name) ? _namedTiles[name] : _default;
        }
        
        private static Texture2D CreateSolidIsometricTile(Color color)
        {
            Texture2D texture = new Texture2D(256, 128, TextureFormat.RGBA32, false);
            texture.name = $"diamond_{color.ToString()}";
            texture.hideFlags = HideFlags.HideAndDontSave;

            texture.SetPixels(0, 0, 256, 128, Enumerable.Repeat(Color.clear, 256 * 128).ToArray());

            for (int x = 0; x < 256; x++)
            {
                if (x <= 128)
                {
                    texture.SetPixels(x, 64 - x / 2, 1,  x, Enumerable.Repeat(color, x).ToArray());
                }
                else
                {
                    texture.SetPixels(x, x / 2 - 64, 1, 256 - x, Enumerable.Repeat(color, 256 - x).ToArray());
                }
            }

            texture.Apply();

            return texture;
        }
    }
}