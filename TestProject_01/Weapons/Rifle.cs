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
using TestProject_01.Managers;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;           //I have the Unreal Engine extension, so it forces me to specify the sprite batch

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Class for the rifle weapon
    /// </summary>
    public class Rifle : Weapon
    {

        public int Accuracy { get; set; } = 5;

        /// <summary>
        /// create rifle with minimum stuff
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="owner"></param>
        public Rifle(Game game, SpriteBatch spriteBatch, Character owner) : base(game, spriteBatch, owner)
        {
            CoolDown = 40;
            //ShotSound = g.Content.Load<SoundEffect>("sounds/weapons/rifleShot_01");
            SetUpAssets();
        }

        /// <summary>
        /// Create rifle with overrided shot variance
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="owner"></param>
        /// <param name="shotVariance"></param>
        public Rifle(Game game, SpriteBatch spriteBatch, Character owner, int shotVariance) : base(game, spriteBatch, owner)
        {
            CoolDown = 40;
            //ShotSound = g.Content.Load<SoundEffect>("sounds/weapons/rifleShot_01");

            Accuracy = shotVariance;
            SetUpAssets();


        }

        private void SetUpAssets()
        {
            ShotSound = SContentManager.instance.sndRifleShot;
            Tex = SContentManager.instance.texRifleSheet;
        }

        /// <summary>
        /// Release a projectile when fire button pressed
        /// </summary>
        public override void Fire()
        {

            if (coolDownCount > 0 || Reloading)
                return;

            //Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, _owner.direction);
            Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, GetDirectionVariance(Accuracy), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(newBullet);

            ShotSound.Play();

            coolDownCount = CoolDown;

            if (!OverrideReload)
            {
                CurrentShots++;
            }

        }

        /// <summary>
        /// Fire with custom direction
        /// </summary>
        /// <param name="direction">the direction</param>
        public override void Fire(Vector2 direction)
        {
            if (coolDownCount > 0 || Reloading)
                return;

            //Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, _owner.direction);
            Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, GetDirectionVariance(Accuracy, direction), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(newBullet);

            ShotSound.Play();

            coolDownCount = CoolDown;

            if(!OverrideReload)
            {
                CurrentShots++;
            }
        }

        /// <summary>
        /// Do nothing for now. Reserved for action when button released
        /// </summary>
        public override void ReleaseFire()
        {
            return;
        }

        public override void StopSounds()
        {
            ShotSound.Dispose();
        }

    }
}
