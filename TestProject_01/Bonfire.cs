/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using TestProject_01.Managers;

namespace TestProject_01
{
    /// <summary>
    /// A fire that adds visuals and a dangerous obstacle to the scene
    /// </summary>
    public class Bonfire : Sprite
    {

        //positioning
        public Vector2 direction { get; set; } = Vector2.Zero;

        //movement variables, for still targets these will generally be meaningless
        public float speed { get; set; } = 0;
        public Vector2 velocity { get; set; }

        //in game dimensions
        public int customWidth { get; set; } = 40;
        public int customHeight { get; set; } = 64;

        //rectangles for in game bounding box and sprite sheet box
        public Rectangle SpriteSheetRect { get; set; }

        //number of sprites in sprite sheets in general directions
        public int customSheetCountX = 5;
        public int customSheetCountY = 1;

        const int _animationTickRate = 8;
        int animationTick = 0;

        public int LifeTime { get; set; } = -1;
        private int lifeTimeCount = 0;

        public DamageType damageType = DamageType.Fire;

        /// <summary>
        /// Constructor with default values
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch"> the batch</param>
        /// <param name="position">sapwn position</param>
        public Bonfire(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {

            Width = customWidth;
            Height = customHeight;
            spriteSheetCountX = customSheetCountX;
            spriteSheetCountY = customSheetCountY;

            SetOurTexture();
            //this.Position = position;
            //CenterSpawn();
            CenterTileSpawn(TileManager.Instance.tileSizeX);

        }

        /// <summary>
        /// Constructor that makes fire with limited lifetime
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="lifeTime"></param>
        public Bonfire(Game game, SpriteBatch spriteBatch, Vector2 position, int lifeTime) : base(game, spriteBatch, position)
        {

            Width = customWidth;
            Height = customHeight;
            spriteSheetCountX = customSheetCountX;
            spriteSheetCountY = customSheetCountY;

            SetOurTexture();
            CenterTileSpawn(TileManager.Instance.tileSizeX);

            LifeTime = lifeTime;

        }

        /// <summary>
        /// Constructor that sets custom texture
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        public Bonfire(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game, spriteBatch, tex, position)
        {

            Width = customWidth;
            Height = customHeight;
            spriteSheetCountX = customSheetCountX;
            spriteSheetCountY = customSheetCountY;

            //this.Position = position;
            //CenterSpawn();
            Position += new Vector2(Width / 2, 0);

        }

        private void SetOurTexture()
        {
            //Tex = g.Content.Load<Texture2D>("images/basicFireSpritesheet");
            Tex = SContentManager.instance.texFire;
        }

        public override void Update(GameTime gameTime)
        {

            if(PlayerTestie.instance != null)
            {
                if (PlayerTestie.instance.GetBounds().Intersects(GetBounds()))
                {
                    PlayerTestie.instance.Damage(1, damageType);
                }
            }
            
            if(LifeTime != -1)
            {
                if(lifeTimeCount >= LifeTime)
                {
                    Game1.ACTIVESCENE.DeleteItemSprite(this);
                }
                lifeTimeCount++;
            }

            AnimateSpriteSheet(spriteSheetCoord, spriteSheetCountX);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _spriteBatch.Draw(
                Tex,
                GetBounds(),
                SpriteSheetRect,
                Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AnimateSpriteSheet(Vector2 startCo, int animationLength)
        {

            if (animationTick >= _animationTickRate)
            {

                if (startCo.Y != spriteSheetCoord.Y)
                {
                    spriteSheetCoord = startCo;
                }
                else
                {

                    if (spriteSheetCoord.X < animationLength)
                    {
                        SpriteSheetRect = GetSourceRect((int)spriteSheetCoord.X, (int)spriteSheetCoord.Y);
                        spriteSheetCoord.X++;
                    }

                    if (spriteSheetCoord.X >= animationLength)
                    {
                        spriteSheetCoord.X = 0;
                    }

                }
                animationTick = 0;
            }

            animationTick++;

        }

    }
}
