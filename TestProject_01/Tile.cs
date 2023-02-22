/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using SharpDX.Direct2D1;
using static System.Net.Mime.MediaTypeNames;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace TestProject_01
{

    public class Tile : Sprite
    {
        
        public bool BlocksCharacter { get; set; }
        public bool BlocksProjectiles { get; set; }

        public enum TileType
        {
            Grass,
            Dirt,
            Sand,
            Bush,
            Water,
            Rock
        };

        public TileType tileType { get; set; } = TileType.Grass;


        /// <summary>
        /// Creates tile with default type
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="spriteSheetCoord"></param>
        public Tile(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int width, int height, Vector2 spriteSheetCoord) : base(game, spriteBatch, tex, position, width, height, spriteSheetCoord)
        {
            SetProjectileBlock();
        }

        /// <summary>
        /// Creates tile with specific type
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="spriteSheetCoord"></param>
        /// <param name="type"></param>
        public Tile(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int width, int height, Vector2 spriteSheetCoord, TileType type) : base(game, spriteBatch, tex, position, width, height, spriteSheetCoord)
        {

            this.tileType = type;
            spriteSheetCountX = 3;
            spriteSheetCountY = 2;
            SetProjectileBlock();

        }

        private void SetProjectileBlock()
        {
            switch (tileType)
            {
                case TileType.Grass:
                    BlocksCharacter = false;
                    BlocksProjectiles= false;
                    break;
                case TileType.Dirt:
                    BlocksCharacter = false;
                    BlocksProjectiles = false;
                    break;
                case TileType.Sand:
                    BlocksCharacter = false;
                    BlocksProjectiles = false;
                    break;
                case TileType.Bush:
                    BlocksCharacter = false;
                    BlocksProjectiles = false;
                    break;
                case TileType.Water:
                    BlocksCharacter = true;
                    BlocksProjectiles = false;
                    break;
                case TileType.Rock:
                    BlocksCharacter = true;
                    BlocksProjectiles = true;
                    break;
                default:
                    BlocksCharacter = false;
                    BlocksProjectiles = false;
                    break;
            }

            ImpedesPath = BlocksCharacter;
        }

    }
}
