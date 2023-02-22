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
using SharpDX.Direct3D9;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using static System.Net.Mime.MediaTypeNames;
using static TestProject_01.Tile;
using SharpDX.Direct2D1;
using TestProject_01.Managers;

namespace TestProject_01
{
    /// <summary>
    /// This is the parent class for virtually all objects that appear in game (except for font and some UI)
    /// </summary>
    public abstract class Sprite : DrawableGameComponent
    {
        
        public Game1 g;                                         //give the main game reference. Used for referencing content.load and other essential things
        public SpriteBatch _spriteBatch { get; set; }           //comes from game1

        //required for creating, sizing, and rendering sprite
        public Texture2D Tex { get; set; }
        public Vector2 Position { get; set; }
        public Color SpriteColor { get; set; } = Color.White;
        public int Width { get; set; }
        public int Height { get; set; }
        public int spriteSheetCountX = 1;                       //the max number of sprites in a row on the sheet
        public int spriteSheetCountY = 1;                       //number of columns in a sprite sheet
        public Vector2 spriteSheetCoord = new Vector2();        //the coordinates of the texture that should be rendered
        
        public Rectangle positionRect { get; set; }             //this will be used to set the rect for the world position of the sprite
        public Rectangle SpriteSheetRect { get; set; }          //this is used for the rect of the actual sprite sheet

        public bool ImpedesPath { get; set; } = false;          //determines whether this sprite will block movement of characters

        public float BoundsMultiplyer { get; set; } = 1;        //changes size of hysical rect bounds

        /// <summary>
        /// The constructor with the least amount of required values
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        protected Sprite(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {

            this.g = (Game1)game;
            _spriteBatch = spriteBatch;
            this.Position = position;

        }


        protected Sprite(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position): base(game)
        {

            this.g = (Game1)game;
            _spriteBatch = spriteBatch;
            this.Tex = tex;
            this.Position = position;

        }

        /// <summary>
        /// Use this constructor if you wish to override the default width, height, and sprite sheet position
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="spriteSheetCoord"></param>
        protected Sprite(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int width, int height, Vector2 spriteSheetCoord) : base(game)
        {

            this.g = (Game1)game;
            _spriteBatch = spriteBatch;
            this.Tex = tex;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.spriteSheetCoord = spriteSheetCoord;

            positionRect = GetBounds();
        }

        
        /// <summary>
        /// Returns the X position of the rect in the sprite texture
        /// </summary>
        /// <returns></returns>
        public int GetSrcPosX()
        {
            //return (Tex.Width / 3) * (int)spriteSheetCoord.X;
            return (Tex.Width / spriteSheetCountX) * (int)spriteSheetCoord.X;
        }

        /// <summary>
        /// Returns the Y position of the rect in the sprite texture
        /// </summary>
        /// <returns></returns>
        public int GetSrcPosY()
        {
            //return (Tex.Height / 2) * (int)spriteSheetCoord.Y;
            return (Tex.Height / spriteSheetCountY) * (int)spriteSheetCoord.Y;
        }

        /// <summary>
        /// Gets the rectangle that is used for the actual location of the sprite in world Pos
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            
            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public Rectangle GetCollisionBounds()
        {
            if (BoundsMultiplyer != 1)
            {
                return new Rectangle(
                    (int)Position.X + (int)((Width - (Width * BoundsMultiplyer)) / 2)
                    , (int)Position.Y + (int)((Height - (Height * BoundsMultiplyer)) / 2)
                    , (int)(Width * BoundsMultiplyer)
                    , (int)(Height * BoundsMultiplyer));
            }

            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        /// <summary>
        /// Returns the rectangle that is used for rendering a portion of the sprite texture
        /// </summary>
        /// <returns></returns>
        public Rectangle GetSourceRect()
        {
            return new Rectangle(GetSrcPosX(), GetSrcPosY(), Tex.Width / spriteSheetCountX, Tex.Height / spriteSheetCountY);
        }

        /// <summary>
        /// Returns the rectangle that is used for rendering a portion of the sprite texture using specific coordinates
        /// </summary>
        /// <param name="posX">X coordinate</param>
        /// <param name="posY">Y coordinate</param>
        /// <returns></returns>
        public Rectangle GetSourceRect(int posX, int posY)
        {
            int positionX = Tex.Width / spriteSheetCountX * posX;
            int positionY = Tex.Height / spriteSheetCountY * posY;

            Rectangle sourcePosRect = new Rectangle(positionX, positionY, Tex.Width / spriteSheetCountX, Tex.Height / spriteSheetCountY);

            return sourcePosRect;

        }

        /// <summary>
        /// By default sprite will spawn at the top left of tiles, this will make sprites spawn right at the center of the tiles.
        /// Assumes Tile is a square size so only one int is required. This game will only use square tiles anyway
        /// </summary>
        /// <param name="tileSize"></param>
        public void CenterTileSpawn(int tileSize)
        {
            if(TileManager.Instance != null)
            {
                Position += new Vector2((tileSize - Width) / 2, (tileSize - Height) / 2);
            }
            
        }

        public Vector2 GetCenterPos()
        {
            return new Vector2(Position.X + (Width / 2), Position.Y + Height / 2);
        }

    }
}
