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
using TestProject_01.Scenes;
using TestProject_01.Managers;

namespace TestProject_01
{
    /// <summary>
    /// The pick up sprite class. Interactible with player
    /// </summary>
    public abstract class Pickup : Sprite
    {

        public GameScene activeScene;

        //positioning
        public Vector2 direction { get; set; } = Vector2.Zero;

        //in game dimensions
        int pickUpSize = 32;

        //rectangles for in game bounding box and sprite sheet box
        public Rectangle SpriteSheetRect { get; set; }

        //the actual positions in the sprite sheet
        public int spriteSheetX = 0;
        public int spriteSheetY = 0;

        public bool centerSpawn = true;

        const int _animationTickRate = 8;
        int animationTick = 0;

        public int healAmount = 1;
        public bool itemUsed = false;

        public int pointValue = 10;

        public Pickup(Game game, GameScene scene, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {
            activeScene = scene;
            SetOurTexture();
            this.Tex = Tex;

            Width = pickUpSize;
            Height = pickUpSize;

            if(centerSpawn)
            {
                CenterTileSpawn(TileManager.Instance.tileSizeX);
            }

            SpriteSheetRect = GetSourceRect(0, 0);
        }


        public override void Update(GameTime gameTime)
        {

            if (PlayerTestie.instance != null)
            {
                if (PlayerTestie.instance.GetBounds().Intersects(GetBounds()) && !itemUsed)
                {
                    PickupItem();
                    
                    itemUsed = true;

                    ScoreManager.AddToCurrentScore(pointValue);

                    activeScene.DeleteItemSprite(this);

                }
            }

            //make sure item is dead and not taking memory
            if (itemUsed)
            {
                this.Dispose();
            }

            //AnimateSpriteSheet(spriteSheetCoord, spriteSheetCountX);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _spriteBatch.Draw(
                Tex,
                GetBounds(),
                SpriteSheetRect,
                SpriteColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public abstract void SetOurTexture();

        public abstract void PickupItem();

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


        void DeleteItem()
        {

            TileManager.Instance.itemSprites.Remove(this);
            g.Components.Remove(this);

        }

    }
}
