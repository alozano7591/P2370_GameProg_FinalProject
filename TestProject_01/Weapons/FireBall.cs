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
using SharpDX.Direct3D9;
using TestProject_01.Managers;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Fireball projectile, hits enemies with fire type damage
    /// </summary>
    public class FireBall : Projectile
    {

        int startRadius = 10;               //i'm going to start with making the bullets squares, at some point they'll become circles, but radius will be the name
        int radius = 0;
        int growthAmt = 5;                  //amount of growth per tick
        int growthTick = 3;                 //how many frames pass before next growth
        int currentTick = 0;
        Single rotation = 0;
        float rotationAmt = .6f;

        int lifeTime = 65;                  //how many frames before projectile kills itself
        int lifeTimeCount = 0;
        Vector2 velocity = Vector2.Zero;
        

        Vector2 origin;

        /// <summary>
        /// Constructor with minimum requirements
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        /// <param name="projectileSpeed">travel speed</param>
        /// <param name="projectileDamage">damage</param>
        /// <param name="direction">travel direction</param>
        public FireBall(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction) : 
            base(game, spriteBatch, position, projectileSpeed, projectileDamage, direction)
        {

            //Tex = g.Content.Load<Texture2D>("images/1pxWhite");
            Width = startRadius;
            Height = startRadius;

            radius = startRadius;

            velocity = direction * Speed;
            SpriteColor= Color.White;

            Tex = SContentManager.instance.texFireball;
            origin = new Vector2(Tex.Width / 2f, Tex.Height / 2f);

            DamageType = DamageType.Fire;
        }

        /// <summary>
        /// This consructor includes team type, making it only hurt those that aren't on the shooter's team.
        /// Use this when you need a projectile that only hurt other team members
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="projectileSpeed"></param>
        /// <param name="projectileDamage"></param>
        /// <param name="direction"></param>
        /// <param name="team"></param>
        public FireBall(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction, TeamType team) : 
            base(game, spriteBatch, position, projectileSpeed, projectileDamage, direction, team)
        {

            //Tex = g.Content.Load<Texture2D>("images/1pxWhite");
            Width = startRadius;
            Height = startRadius;

            radius = startRadius;

            velocity = direction * Speed;
            SpriteColor = Color.White;

            Tex = g.Content.Load<Texture2D>("images/fireball-1");
            origin = new Vector2(Tex.Width / 2f, Tex.Height / 2f);

            DamageType = DamageType.Fire;
        }

        /// <summary>
        /// update loop
        /// </summary>
        /// <param name="gameTime">the deta time</param>
        public override void Update(GameTime gameTime)
        {
            Move();
            Grow();

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
            CheckSpriteImpact(true);

            base.Update(gameTime);
        }

        /// <summary>
        /// move our fireball
        /// </summary>
        public void Move()
        {
            Position += velocity;
            CheckLifeTime();
        }

        /// <summary>
        /// Grow our fire ball each call
        /// </summary>
        public void Grow()
        {
            if(currentTick >= growthTick)
            {
                radius += growthAmt;
                rotation+= rotationAmt;

                Width = radius; 
                Height = radius;
                currentTick = 0;
            }
            else
            {
                currentTick++;
            }

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
        /// Draw fireball 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(Tex, GetBounds(), null, SpriteColor, rotation, origin, SpriteEffects.None, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
