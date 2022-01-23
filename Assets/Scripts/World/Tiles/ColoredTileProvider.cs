using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.World.Tiles
{
    public class ColoredTileProvider : ITileProvider
    {
        private const int TileWidth = 256;
        private const int TileHeight = TileWidth / 2;

        private static readonly TileBase _default;
        private static readonly Dictionary<string, TileBase> _namedTiles = new Dictionary<string, TileBase>();

        public TileBase Get(string name)
        {
            return _namedTiles.ContainsKey(name) ? _namedTiles[name] : _default;
        }

        static ColoredTileProvider()
        {
            _default = CreateFloorTile("default", new Color(0.625f, 0.32f, 0.176f));
            _namedTiles.Add("grass", CreateFloorTile("grass", Color.green));
            _namedTiles.Add("wall", CreateWallTile("wall", Color.gray));
        }

        #region FloorTile

        private static Tile CreateFloorTile(string name, Color color)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = $"{name}_tile";
            tile.colliderType = Tile.ColliderType.None;
            tile.hideFlags = HideFlags.DontSave;
            tile.sprite = CreateFloorTileSprite(name, color);
            return tile;
        }

        private static Sprite CreateFloorTileSprite(string name, Color color)
        {
            Texture2D texture = CreateFloorTileTexture(name, color);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, TileWidth, TileHeight), new Vector2(0.5f, 0.5f), TileWidth);
            sprite.name = $"{name}_floortile_sprite";
            return sprite;
        }

        private static Texture2D CreateFloorTileTexture(string name, Color color)
        {
            Texture2D texture = new Texture2D(TileWidth, TileHeight, TextureFormat.RGBA32, false);
            texture.name = $"{name}_floortile_texture";
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;

            texture.SetPixels(0, 0, TileWidth, TileHeight, Enumerable.Repeat(Color.clear, TileWidth * TileHeight).ToArray());

            for (int x = 0; x < TileWidth; x++)
            {
                if (x <= TileHeight)
                {
                    texture.SetPixels(x, (TileHeight - x) / 2, 1, x, Enumerable.Repeat(color, x).ToArray());
                }
                else
                {
                    texture.SetPixels(x, (x - TileHeight) / 2, 1, TileWidth - x, Enumerable.Repeat(color, TileWidth - x).ToArray());
                }
            }

            texture.Apply();

            return texture;
        }

        #endregion

        #region WallTile

        private static Tile CreateWallTile(string name, Color color)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = $"{name}_tile";
            tile.colliderType = Tile.ColliderType.None;
            tile.hideFlags = HideFlags.DontSave;
            tile.sprite = CreateWallTileSprite(name, color);
            return tile;
        }

        private static Sprite CreateWallTileSprite(string name, Color color)
        {
            Texture2D texture = CreateWallTileTexture(name, color);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, TileWidth, 2 * TileWidth), new Vector2(0.5f, 0.25f), TileWidth);
            sprite.name = $"{name}_walltile_sprite";
            return sprite;
        }

        private static Texture2D CreateWallTileTexture(string name, Color color)
        {
            Texture2D texture = new Texture2D(TileWidth, 2 * TileWidth, TextureFormat.RGBA32, false);
            texture.name = $"{name}_walltile_texture";
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;

            texture.SetPixels(0, 0, TileWidth, 2 * TileWidth, Enumerable.Repeat(Color.clear, 2 * TileWidth * TileWidth).ToArray());

            for (int x = 0; x < TileWidth; x++)
            {
                if (x <= TileWidth / 2)
                {
                    texture.SetPixels(x, (TileWidth - x) / 2, 1, x + TileWidth, Enumerable.Repeat(color, x + TileWidth).ToArray());
                }
                else
                {
                    texture.SetPixels(x, x / 2, 1, 2 * TileWidth - x, Enumerable.Repeat(color, 2 * TileWidth - x).ToArray());
                }
            }

            texture.Apply();

            return texture;
        }

        #endregion
    }
}