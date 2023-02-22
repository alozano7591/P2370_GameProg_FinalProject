/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using TestProject_01.Managers;
using TestProject_01.Scenes;
using TestProject_01.UI;
using TestProject_01.Weapons;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TestProject_01
{

    /// <summary>
    /// Class for playable character
    /// </summary>
    public class PlayerTestie : Character, IHealth
    {

        public static PlayerTestie instance;

        public bool isAlive { get; set; } = true;

        public float speed { get; set; } = 4;
        
        public Vector2 velocity { get; set; }

        //characterDimensions

        public Rectangle spriteBounds;

        //Animation Values
        int defaultSpriteSheetNumX = 4;     //the default number of sprites to the x
        int defaultSpriteSheetNumY = 4;     //the default number of sprite to the y 
        public int spriteSheetX = 0;        //the current X position of the animation
        public int spriteSheetY = 0;        //the current Y position of the animation
        const int _animationTickRate = 6;   //frames between animation switches
        int animationTick = 0;              //used to track animation tick count

        //Health stuff
        public int hp = 3;
        public int totalHP = 3;
        
        //private Color colorMultiplier= Color.White;
        //private Color hurtColor = Color.Red;

        public PlayerUI playerUI;

        //the following three variables make it so that player doesn't get damaged every frame
        private const int HitInvicibilityPeriod = 40;
        //private int hitInvicibilityCounter = 0;
        //private bool hitInvincibleOn = false;

        public Weapon playerWeapon;

        TeamType defaultTeam = TeamType.Green;

        public GameScene ActiveScene { get; set; }

        /// <summary>
        /// Uses default sprite values, but takes weapon number
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="weaponNum">Used to determine what weapon player spawns with</param>
        public PlayerTestie(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int weaponNum) : base(game, spriteBatch, tex, position)
        {

            Width = characterWidth;
            Height = characterHeight;

            spriteSheetCountX = defaultSpriteSheetNumX;
            spriteSheetCountY = defaultSpriteSheetNumY;

            SpriteSheetRect = GetSourceRect(0, 0);

            instance = this;

            hp = totalHP;

            OverridesReload = true;

            GetWeapon(weaponNum);

            Team = defaultTeam;

            

        }

        /// <summary>
        /// Constructor with minimum peramiters. Uses default sprite variables
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerTestie(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed) : base(game, spriteBatch, tex, position)
        {

            this.velocity = speed;

            //character dimesions are defaulted in Character parent class to 40,64
            Width = characterWidth;
            Height = characterHeight;

            SpriteSheetRect = GetSourceRect(0, 0);

            OverridesReload = true;

            instance = this;
            Team = defaultTeam;
        }

        /// <summary>
        /// Constructor that allows user to also specify sprite sheet counts in the x and y directions
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="sheetCountX"></param>
        /// <param name="sheetCountY"></param>
        public PlayerTestie(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed, int sheetCountX, int sheetCountY) : base(game, spriteBatch, tex, position)
        {

            Width= characterWidth;
            Height = characterHeight;
            
            spriteSheetCountX = sheetCountX;
            spriteSheetCountY = sheetCountY;

            SpriteSheetRect = GetSourceRect(0, 0);

            instance = this;

            hp = totalHP;

            OverridesReload = true;

            //playerWeapon = new Rifle(game, spriteBatch, this);
            GetWeapon(0);

            Team = defaultTeam;
            
        }

        /// <summary>
        /// spawn our player weapon
        /// </summary>
        /// <param name="weaponNum"></param>
        public void GetWeapon(int weaponNum)
        {
            switch (weaponNum)
            {
                case 0:
                    playerWeapon = new Rifle(g, _spriteBatch, this);
                    break;
                case 1:
                    playerWeapon = new MachineGun(g, _spriteBatch, this);
                    break;
                case 2:
                    playerWeapon = new Shotgun(g, _spriteBatch, this);
                    break;
                case 3:
                    playerWeapon = new FlameThrower(g, _spriteBatch, this);
                    break;
                default:
                    playerWeapon = new Rifle(g, _spriteBatch, this);
                    break;
            }
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            if (!isAlive) return;
            

            KeyboardState ks = Keyboard.GetState();
            int horDir = 0;
            int vertDir = 0;

            if(playerWeapon != null)
            {
                playerWeapon.CountCoolDown();
                //playerWeapon.ManageShots();
            }

            if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                //position -= velocity;
                horDir = -1;

            }
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                //position += velocity;
                horDir = 1;
                
            }

            if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
            {
                //position -= new Vector2(0, 4);
                vertDir = -1;

            }

            if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
            {
                //position += new Vector2(0, 4); 
                vertDir = 1;

            }

            Position = new Vector2(Shared.stage.X / 2 - (Width / 2), Shared.stage.Y / 2 - (Height / 2));

            //check if we are changing direction before updating.
            //if we are receiving a new direction then reset animation tick
            if (Direction.X != horDir || Direction.Y != vertDir)
            {
                SetSpriteDirection();
                animationTick = _animationTickRate;
            }

            if (horDir != 0 || vertDir != 0)
            {
                Direction = new Vector2(horDir, vertDir);

                if (Direction != Vector2.Zero)
                {
                    Direction.Normalize();
                }

                Vector2 newVelocity = Direction * speed;

                //check if moving will hit anything
                if (CheckForCollision(GetNewBounds(Position + newVelocity)))
                {

                    velocity = GetMoveableVelocity(horDir, vertDir);
                }
                else
                {
                    velocity = newVelocity;
                }

            }
            else
            {
                velocity = Vector2.Zero;
            }

            Position += velocity;

            if (velocity.LengthSquared() > 0)
            {

                if (animationTick >= _animationTickRate)
                {
                    SetAnimation();
                    animationTick = 0;
                }
                animationTick++;
            }

            if (ks.IsKeyDown(Keys.Space))
            {

                playerWeapon.Fire();

            }

            if(ks.IsKeyUp(Keys.Space))
            {
                playerWeapon.ReleaseFire();
            }

            DamageInvincibilityTick();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw call
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(
                Tex,
                GetBounds(), 
                SpriteSheetRect,
                colorMultiplier);

            if(playerWeapon != null)
            {
                _spriteBatch.Draw(
                playerWeapon.Tex,
                GetBounds(),
                SpriteSheetRect,
                colorMultiplier);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Get a new bounds rect
        /// </summary>
        /// <param name="newPos">new postition</param>
        /// <returns></returns>
        public Rectangle GetNewBounds(Vector2 newPos)
        {
            return new Rectangle((int)newPos.X, (int)newPos.Y, Width, Height);
        }

        /// <summary>
        /// Set a specific sprite sheet count
        /// </summary>
        /// <param name="x">Number of sprites length wise</param>
        /// <param name="y">Number of sprites height wise</param>
        public void SetSpriteSheetCounts(int x, int y)
        {

        }

        /// <summary>
        /// Set our animation
        /// </summary>
        public void SetAnimation()
        {

            if (velocity.X > 0)
            {
                AnimateSpriteSheet(new Vector2(0, 3), 4);
                //spriteSheetY = 3;

            }
            else if (velocity.X < 0)
            {
                AnimateSpriteSheet(new Vector2(0, 2), 4);
                //spriteSheetY = 2;

            }
            else if (velocity.Y > 0)
            {
                AnimateSpriteSheet(new Vector2(0, 0), 4);
                //spriteSheetY = 0;

            }
            else if (velocity.Y < 0)
            {
                AnimateSpriteSheet(new Vector2(0, 1), 4);
                //spriteSheetY = 1;

            }

        }

        /// <summary>
        /// Animate the sprite sheet with values
        /// </summary>
        /// <param name="startCo">Start of the spritesheet</param>
        /// <param name="animationLength">the length of the animation</param>
        public void AnimateSpriteSheet(Vector2 startCo, int animationLength)
        {

            int spriteSheetXCount = Tex.Width / Width;

            if(startCo.Y != spriteSheetCoord.Y)
            {
                spriteSheetCoord = startCo;
            }
            else
            {

                if(spriteSheetCoord.X < animationLength)
                {
                    SpriteSheetRect = GetSourceRect((int)spriteSheetCoord.X, (int)spriteSheetCoord.Y);
                    spriteSheetCoord.X++;
                }

                if(spriteSheetCoord.X >= animationLength)
                {
                    spriteSheetCoord.X = 0;
                }
    
            }
            
        }

        /// <summary>
        /// Set up direction for our sprite
        /// </summary>
        public void SetSpriteDirection()
        {

            if (velocity.X > 0)
            {
                
                spriteSheetY = 3;

            }
            else if (velocity.X < 0)
            {

                spriteSheetY = 2;

            }
            else if (velocity.Y > 0)
            {

                spriteSheetY = 0;

            }
            else if (velocity.Y < 0)
            {

                spriteSheetY = 1;

            }

            SpriteSheetRect = GetSourceRect(0, spriteSheetY);

        }

        /// <summary>
        /// Check if there will be collision at rect
        /// </summary>
        /// <param name="testRect">the rectanlge to test</param>
        /// <returns></returns>
        public bool CheckForCollision(Rectangle testRect)
        {

            for(int i = 0; i < TileManager.Instance.gameTiles.GetLength(0); i++)
            {
                for(int j = 0; j < TileManager.Instance.gameTiles.GetLength(1); j++)
                {
                    //if(TileManager.Instance.gameTiles[i, j].tileType == Tile.TileType.Rock ||
                    //    TileManager.Instance.gameTiles[i, j].tileType == Tile.TileType.Water)
                    //{

                    //    if (TileManager.Instance.gameTiles[i, j].GetBounds().Intersects(testRect))
                    //    {
                    //        //Debug.WriteLine("Collision detected");
                    //        return true;
                    //    }
                    //}

                    if (TileManager.Instance.gameTiles[i, j].ImpedesPath)
                    {

                        if (TileManager.Instance.gameTiles[i, j].GetBounds().Intersects(testRect))
                        {
                            //Debug.WriteLine("Collision detected");
                            return true;
                        }
                    }

                }
                
            }

            for(int i = 0; i < TileManager.Instance.itemSprites.Count; i++)
            {
                if (TileManager.Instance.itemSprites[i].ImpedesPath)
                {
                    if (TileManager.Instance.itemSprites[i].GetCollisionBounds().Intersects(testRect))
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        /// <summary>
        /// This should be called when a move in a direction fails due to collision.
        /// Method checks if the player can still move unblocked in either just x or y directions.
        /// This prevents the player from getting stuck while moving against a wall even when a direction is still clear.
        /// </summary>
        /// <param name="xMove"></param>
        /// <param name="yMove"></param>
        /// <returns></returns>
        private Vector2 GetMoveableVelocity(int xMove, int yMove)
        {
            //try moving in the x first
            Vector2 newVelocity = new Vector2(xMove * speed, 0);
            //try moving in the y direction since x failed


            if (!CheckForCollision(GetNewBounds(Position + newVelocity)))
            {
                return newVelocity;
            }

            newVelocity = new Vector2(0, yMove * speed);
            if (!CheckForCollision(GetNewBounds(Position + newVelocity)))
            {
                return newVelocity;
            }
            


            return Vector2.Zero;
        }


        /// <summary>
        /// take damage and do things
        /// </summary>
        /// <param name="damageAmt">damage amount</param>
        public void Damage(int damageAmt)
        {
            //only damage player if they are not invincible
            if(hitInvincibleOn)
            {
                return;
            }
            else
            {
                
                hp -= damageAmt;
                PlayerUI.Instance.UpdateHealthBar(hp, totalHP);

                hitInvincibleOn = true;
                hitInvicibilityCounter= 0;

                if(hp <= 0)
                {
                    Die();
                }
                else
                {
                    HitGrunt();
                }
            }
            

        }

        /// <summary>
        /// take damage and do things
        /// </summary>
        /// <param name="damageAmt">damage amount</param>
        /// <param name="damageType">type of damage</param>
        public void Damage(int damageAmt, DamageType damageType)
        {
            //only damage player if they are not invincible
            if (hitInvincibleOn)
            {
                return;
            }
            else
            {

                hp -= damageAmt;
                PlayerUI.Instance.UpdateHealthBar(hp, totalHP);

                hitInvincibleOn = true;
                hitInvicibilityCounter = 0;

                if (hp <= 0)
                {
                    Die(damageType);
                }
                else
                {
                    HitGrunt();
                }
            }
        }

        /// <summary>
        /// Play sound of soldier grunting from hit.
        /// Method makes it so that i can change sound once here 
        /// while having the exact same call for all instances
        /// </summary>
        public void HitGrunt()
        {
            hitGruntSound.Play();
        }

        /// <summary>
        /// Restore health
        /// </summary>
        /// <param name="healAmt">Heal amount</param>
        public void Heal(int healAmt)
        {
            if(hp < totalHP)
            {
                hp += healAmt;
                PlayerUI.Instance.UpdateHealthBar(hp, totalHP);
            }
            
        }

        /// <summary>
        /// End the life 
        /// </summary>
        private void Die()
        {
            playerWeapon.ReleaseFire();
            playerWeapon.StopSounds();
            DoOtherDeathThings();
        }

        /// <summary>
        /// Die, but with a special type 
        /// </summary>
        /// <param name="damageType">Damage type for death</param>
        private void Die(DamageType damageType)
        {
            playerWeapon.ReleaseFire();
            playerWeapon.StopSounds();

            switch (damageType)
            {
                case DamageType.Normal:
                    break;
                case DamageType.Fire:

                    break;
                case DamageType.Explosion:
                    break;
                default:
                    break;
            }

            DoOtherDeathThings();
        }

        /// <summary>
        /// Consolidating death actions here
        /// </summary>
        private void DoOtherDeathThings()
        {

            g.GoToScene(g.highscoreScene, true);
            
        }

    }
}
