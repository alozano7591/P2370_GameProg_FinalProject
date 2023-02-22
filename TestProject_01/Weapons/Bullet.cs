/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Bullet class that is used in most weapons
    /// </summary>
    internal class Bullet : Projectile
    {

        int bulletRadius = 10;      //i'm going to start with making the bullets squares, at some point they'll become circles, but radius will be the name
        int lifeTime = 180;         //after 600 frames bullet kills itself
        int lifeTimeCount = 0;
        Vector2 velocity = Vector2.Zero;

        /// <summary>
        /// Constructor with minimum attributes
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">position</param>
        /// <param name="projectileSpeed">the projectile speed</param>
        /// <param name="projectileDamage">the damage</param>
        /// <param name="direction">the direction of travel</param>
        public Bullet(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction) :
            base(game, spriteBatch, position, projectileSpeed, projectileDamage, direction)
        {

            Tex = Tex = SContentManager.instance.texWhitePixel;
            Width = bulletRadius;
            Height = bulletRadius;

            velocity = direction * Speed;
        }

        /// <summary>
        /// Constructor for bullet with team declaration
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        /// <param name="projectileSpeed">speed of projectile</param>
        /// <param name="projectileDamage">projectile damnage</param>
        /// <param name="direction">travel direction</param>
        /// <param name="team">the owning team</param>
        public Bullet(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction, TeamType team) : 
            base(game, spriteBatch, position, projectileSpeed, projectileDamage, direction, team)
        {

            Tex = g.Content.Load<Texture2D>("images/1pxWhite");
            Width = bulletRadius;
            Height = bulletRadius;

            velocity = direction * Speed;
        }


        /// <summary>
        /// our update loop
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            Move();

            //check if hitting player
            if (PlayerTestie.instance != null)
            {
                if (PlayerTestie.instance.Team != Team)
                {
                    if (PlayerTestie.instance.GetBounds().Intersects(GetBounds()))
                    {
                        PlayerTestie.instance.Damage(Damage);
                    }
                }

            }

            //check all items if hits anything
            CheckTileImpact();
            CheckSpriteImpact();

            base.Update(gameTime);
        }

        /// <summary>
        /// Send our bullet flying
        /// </summary>
        public void Move()
        {
            Position += velocity;
            CheckLifeTime();
        }

        /// <summary>
        /// Projectile needs to destroy itself after a bit of time
        /// </summary>
        void CheckLifeTime()
        {
            if (lifeTimeCount >= lifeTime)
            {
                //Enabled = false;
                EndLife(this);
            }

            lifeTimeCount++;
        }


        /// <summary>
        /// draw call to make it showup
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(Tex, GetBounds(), Color.Yellow);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
