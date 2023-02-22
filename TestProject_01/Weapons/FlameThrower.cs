/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Flamethrower shoots streams of flames at enemies with flame effect
    /// </summary>
    public class FlameThrower : Weapon
    {

        int projectileSpeed = 6;

        public int Accuracy { get; set; } = 8;

        SoundEffectInstance soundEffectInstance;

        /// <summary>
        /// Create flame thrower with minimum properties
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="owner">the owner of the weapon</param>
        public FlameThrower(Game game, SpriteBatch spriteBatch, Character owner) : base(game, spriteBatch, owner)
        {
            CoolDown = 5;

            SetUpAssets();
            
        }

        private void SetUpAssets()
        {
            ShotSound = SContentManager.instance.sndFlameThrowerLoop2;
            soundEffectInstance = ShotSound.CreateInstance();
            soundEffectInstance.IsLooped = true;

            Tex = SContentManager.instance.texFlamethrowerSheet;
        }

        /// <summary>
        /// Unleash hell
        /// </summary>
        public override void Fire()
        {
            
            if (coolDownCount > 0)
                return;
            //Projectile fireBall = new FireBall(g, _spriteBatch, _owner.GetCenterPos(), projectileSpeed, Damage, _owner.direction);
            Projectile fireBall = new FireBall(g, _spriteBatch, _owner.GetCenterPos(), projectileSpeed, Damage, GetDirectionVariance(Accuracy), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(fireBall);

            if(soundEffectInstance.State != SoundState.Playing)
            {
                soundEffectInstance.IsLooped = true;
                soundEffectInstance.Play();
            }

            coolDownCount = CoolDown;

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
            //Projectile fireBall = new FireBall(g, _spriteBatch, _owner.GetCenterPos(), projectileSpeed, Damage, _owner.direction);
            Projectile fireBall = new FireBall(g, _spriteBatch, _owner.GetCenterPos(), projectileSpeed, Damage, GetDirectionVariance(Accuracy, direction), _owner.Team);
            TileManager.Instance.AddProjectileToWorld(fireBall);

            if(_owner is PlayerTestie)
            {
                if (soundEffectInstance.State != SoundState.Playing)
                {
                    soundEffectInstance.IsLooped = true;
                    soundEffectInstance.Play();
                }
            }
            else
            {
                if (soundEffectInstance.State != SoundState.Playing)
                {
                    soundEffectInstance.IsLooped = false;
                    soundEffectInstance.Play();
                }
            }
            

            coolDownCount = CoolDown;

            if (_owner.OverridesReload != true)
            {
                CurrentShots++;
            }
        }

        /// <summary>
        /// Stop releasing fire. This is mostly used for stopping loop
        /// </summary>
        public override void ReleaseFire()
        {
            if (soundEffectInstance.State == SoundState.Playing)
            {
                //soundEffectInstance.IsLooped = false;
                soundEffectInstance.Stop();
            }
        }

        public override void StopSounds()
        {
            soundEffectInstance.IsLooped = false;
            soundEffectInstance.Stop();
        }
    }
}
