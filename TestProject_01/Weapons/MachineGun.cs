using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using TestProject_01.Managers;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;           //I have the Unreal Engine extension, so it forces me to specify the sprite batch

namespace TestProject_01.Weapons
{
    /// <summary>
    /// A gun that fires bullets at a rapid rate
    /// </summary>
    public class MachineGun : Weapon
    {

        public int Accuracy { get; set; } = 16;

        //makes the shot capacity unique to this gun
        public int shotCap = 8;

        /// <summary>
        /// Create machine gun with default attributes
        /// </summary>
        /// <param name="game">main game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="owner">the owner of the weapon</param>
        public MachineGun(Game game, SpriteBatch spriteBatch, Character owner) : base(game, spriteBatch, owner)
        {
            
            CoolDown = 10;
            //ShotSound = g.Content.Load<SoundEffect>("sounds/weapons/rifleShot_01");
            SetUpAssets();

            ShotCapacity = shotCap;
        }

        private void SetUpAssets()
        {
            ShotSound = SContentManager.instance.sndRifleShot;
            Tex = SContentManager.instance.texMGSheet;
        }

        /// <summary>
        /// Fire bullets when called
        /// </summary>
        public override void Fire()
        {
            if (coolDownCount > 0)
                return;

            //Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, _owner.direction);
            Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, GetDirectionVariance(Accuracy), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(newBullet);

            ShotSound.Play();

            coolDownCount = CoolDown;

            ShotCapacity = shotCap;

            if (_owner.OverridesReload != true)
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
            if (coolDownCount > 0)
                return;

            //Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, _owner.direction);
            Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, GetDirectionVariance(Accuracy, direction), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(newBullet);

            ShotSound.Play();

            coolDownCount = CoolDown;

            if (_owner.OverridesReload != true)
            {
                CurrentShots++;
            }
        }

        /// <summary>
        /// Do nothing for now
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
