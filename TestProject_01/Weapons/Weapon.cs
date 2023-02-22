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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;           //I have the Unreal Engine extension, so it forces me to specify the sprite batch

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Used as the base class for all weapons in game
    /// </summary>
    public abstract class Weapon : DrawableGameComponent
    {

        public Game1 g;                                         //give the main game reference. Used for referencing content.load and other essential things
        public SpriteBatch _spriteBatch { get; set; }           //comes from game1
        public Character _owner { get;set; }
        public Texture2D Tex { get; set; }


        // Damage amount
        public int Damage { get; set; } = 1;

        /// <summary>
        /// This will be used to control our rate of fire
        /// </summary>
        public int CoolDown { get; set; } = 50;
        public int coolDownCount = 0;

        //mostly for AI, paces shooting by simulating reload
        public int ShotCapacity { get; set; } = 4;
        public int CurrentShots { get; set; } = 0;
        public bool Reloading { get; set; } = false;
        public int ReloadTime { get; set; } = 300;
        private int currentReloadCount = 0;

        public bool OverrideReload { get; set; } = false;

        public SoundEffect ShotSound { get; set; }

        public Texture2D projectileTex;

        protected Weapon(Game game, SpriteBatch spriteBatch) : base(game)
        {
            g = (Game1)game;
            _spriteBatch = spriteBatch;
        }

        protected Weapon(Game game, SpriteBatch spriteBatch, Character owner) : base(game)
        {
            g = (Game1)game;
            _spriteBatch = spriteBatch;
            _owner = owner;
        }

        /// <summary>
        /// fires at the direction the owner is facing
        /// </summary>
        public abstract void Fire();

        /// <summary>
        /// fires at specific direction independent of where owner is facing
        /// </summary>
        /// <param name="direction">fire direction</param>
        public abstract void Fire(Vector2 direction);

        /// <summary>
        /// Tell gun that the player has released the fire button. Needed for weapons with looping sounds. 
        /// Won't be needed for all weapons
        /// </summary>
        public abstract void ReleaseFire();

        /// <summary>
        /// use to check whether to count down cool down or not
        /// </summary>
        public void CountCoolDown()
        {
            if (coolDownCount > 0)
            {
                coolDownCount--;

                if (coolDownCount <= 0)
                {
                    coolDownCount = 0;
                }
            }
        }

        /// <summary>
        /// Creates variation in the direction that projectiles shoot out at.
        /// Gets a random angle within the submitted range and adds or subtracts to the desired direction.
        /// </summary>
        /// <param name="angleVariance">The desired level of accuracy for a weapon</param>
        /// <returns></returns>
        public Vector2 GetDirectionVariance(int angleVariance)
        {
            //set up shotangle based on player's direction
            int randomDir;
            double randomAngle;

            Random random = new Random();

            //convert our player's current direction to Rads
            double shotRadAngle = MathF.Atan2(_owner.Direction.X, _owner.Direction.Y);

            randomDir = random.Next(2) == 1 ? 1 : -1;

            randomAngle = random.Next(angleVariance / 2);

            double spreadRad = (Math.PI / 180) * randomAngle;

            //gets random angle by multiplying shotAngle by random fraction, then multiplying by random direction (+/-)
            shotRadAngle += (spreadRad * randomDir);

            Vector2 shotDir = new Vector2((float)Math.Sin(shotRadAngle), (float)Math.Cos(shotRadAngle));

            return shotDir;

        }

        /// <summary>
        /// Creates variation in the direction that projectiles shoot out at using custom direction.
        /// Gets a random angle within the submitted range and adds or subtracts to the desired direction.
        /// </summary>
        /// <param name="angleVariance">desired accuracy</param>
        /// <param name="aimDirection">the direction to shoot at</param>
        /// <returns></returns>
        public Vector2 GetDirectionVariance(int angleVariance, Vector2 aimDirection)
        {
            //set up shotangle based on player's direction
            int randomDir;
            double randomAngle;

            Random random = new Random();

            //convert our player's current direction to Rads
            double shotRadAngle = MathF.Atan2(aimDirection.X, aimDirection.Y);

            randomDir = random.Next(2) == 1 ? 1 : -1;

            randomAngle = random.Next(angleVariance / 2);

            double spreadRad = (Math.PI / 180) * randomAngle;

            //gets random angle by multiplying shotAngle by random fraction, then multiplying by random direction (+/-)
            shotRadAngle += (spreadRad * randomDir);

            Vector2 shotDir = new Vector2((float)Math.Sin(shotRadAngle), (float)Math.Cos(shotRadAngle));

            return shotDir;

        }

        /// <summary>
        /// This emulates soldiers with magazines forcing them to reload sometimes
        /// meant to give player a window of opportunity and prevent soldier from always shooting
        /// </summary>
        public void ManageShots()
        {
            //if owner doesn't reload then do not manage shots
            if (_owner.OverridesReload)
                return;

            if (CurrentShots >= ShotCapacity && Reloading == false)
            {
                currentReloadCount = 0;
                Reloading = true;

            }
            else if (Reloading)
            {
                if (currentReloadCount < ReloadTime)
                {
                    currentReloadCount++;
                }
                else
                {
                    CurrentShots = 0;
                    Reloading = false;
                }
            }
        }

        /// <summary>
        /// use this to kill sound from weapon. Usually used upon character death
        /// </summary>
        public abstract void StopSounds();
    }
}
