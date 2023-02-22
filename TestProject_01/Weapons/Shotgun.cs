/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Reflection.Metadata;
using TestProject_01.Managers;

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Shotgun class allows character to shoot spread of bullets
    /// </summary>
    public class Shotgun : Weapon
    {
        int numberOfProjectiles = 6;
        int spreadAngle = 40;

        public int shotCap = 4;

        /// <summary>
        /// Creates shotgun with default characteristics
        /// </summary>
        /// <param name="game">The main game class</param>
        /// <param name="spriteBatch">Sprite batch</param>
        /// <param name="owner">The owner of the weapon</param>
        public Shotgun(Game game, SpriteBatch spriteBatch, Character owner) : base(game, spriteBatch, owner)
        {
            CoolDown = 40;

            SetUpAssets();

            ShotCapacity = shotCap;
        }

        /// <summary>
        /// Creates shotgun with a custom spread
        /// </summary>
        /// <param name="game">Main game class</param>
        /// <param name="spriteBatch">Main sprite batch</param>
        /// <param name="owner">Onwer of the weapon</param>
        /// <param name="spread">Custom spread of shot</param>
        public Shotgun(Game game, SpriteBatch spriteBatch, Character owner, int spread) : base(game, spriteBatch, owner)
        {
            CoolDown = 40;

            SetUpAssets();

            ShotCapacity = shotCap;
        }

        private void SetUpAssets()
        {
            ShotSound = SContentManager.instance.sndShotgun;
            Tex = SContentManager.instance.texShotgunSheet;
        }

        /// <summary>
        /// How the weapon shoots when fired
        /// </summary>
        public override void Fire()
        {
            if (coolDownCount > 0)
                return;

            ShotSound.Play();

            //set up shotangle based on player's direction
            double shotRadAngle;
            double spreadRad;
            int randomDir;
            double randomAngle;

            Random random= new Random();


            for(int i = 0; i < numberOfProjectiles; i++)
            {
                //convert our player's current direction to Rads
                shotRadAngle = MathF.Atan2(_owner.Direction.X, _owner.Direction.Y);

                randomDir = random.Next(2) == 1 ? 1 : -1;

                randomAngle = random.Next(spreadAngle / 2);

                spreadRad = (Math.PI / 180) * randomAngle;

                shotRadAngle += (spreadRad * randomDir);                      //gets random angle by multiplying shotAngle by random fraction, then multiplying by random direction (+/-)

                Vector2 shotDir = new Vector2((float)Math.Sin(shotRadAngle), (float)Math.Cos(shotRadAngle));

                Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, shotDir, _owner.Team);
                TileManager.Instance.AddProjectileToWorld(newBullet);
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

            ShotSound.Play();

            //set up shotangle based on player's direction
            double shotRadAngle;
            double spreadRad;
            int randomDir;
            double randomAngle;

            Random random = new Random();


            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //convert our player's current direction to Rads
                shotRadAngle = MathF.Atan2(direction.X, direction.Y);

                randomDir = random.Next(2) == 1 ? 1 : -1;

                randomAngle = random.Next(spreadAngle / 2);

                spreadRad = (Math.PI / 180) * randomAngle;

                shotRadAngle += (spreadRad * randomDir);                      //gets random angle by multiplying shotAngle by random fraction, then multiplying by random direction (+/-)

                Vector2 shotDir = new Vector2((float)Math.Sin(shotRadAngle), (float)Math.Cos(shotRadAngle));

                Bullet newBullet = new Bullet(g, _spriteBatch, _owner.GetCenterPos(), 20, Damage, shotDir, _owner.Team);
                TileManager.Instance.AddProjectileToWorld(newBullet);
            }

            coolDownCount = CoolDown;

            if (_owner.OverridesReload != true)
            {
                CurrentShots++;
            }
        }

        /// <summary>
        /// What to do when fire button released
        /// In this case nothing
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
