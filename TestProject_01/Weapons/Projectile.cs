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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestProject_01.Managers;

namespace TestProject_01.Weapons
{
    /// <summary>
    /// Parent class for all projectiles fired from weapons, bombs, etc
    /// </summary>
    public abstract class Projectile : Sprite
    {

        public int Speed { get; set; }
        public int Damage { get; set; }
        public Vector2 direction;
        public TeamType Team { get; set; } = TeamType.Neutral;
        public DamageType DamageType { get; set; } = DamageType.Normal;

        /// <summary>
        /// constructor with minimum requirements
        /// </summary>
        /// <param name="game">main game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        public Projectile(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {
        }

        /// <summary>
        /// Fire projectile with custom speed, damage, and direction
        /// </summary>
        /// <param name="game">main game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        /// <param name="projectileSpeed">speed of projectile</param>
        /// <param name="projectileDamage">damage amount</param>
        /// <param name="direction">travel direction</param>
        public Projectile(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction) : base(game, spriteBatch, position)
        {
            Speed = projectileSpeed;
            Damage = projectileDamage;
            this.direction = direction;
        }


        /// <summary>
        /// This consructor includes team type, making it only hurt those that aren't on the shooter's team.
        /// Use this when you need a projectile that only hurt other team members
        /// </summary>
        /// <param name="game">main game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        /// <param name="projectileSpeed">custom speed</param>
        /// <param name="projectileDamage">custom damage</param>
        /// <param name="direction">travel direction</param>
        /// <param name="team">what team the projectile belongs to</param>
        public Projectile(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction, TeamType team) : base(game, spriteBatch, position)
        {
            Speed = projectileSpeed;
            Damage = projectileDamage;
            this.direction = direction;
            Team = team;
        }

        /// <summary>
        /// Use this to specifiy a specfic damage type other than normal, like fire of explosion
        /// </summary>
        /// <param name="game">main game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn position</param>
        /// <param name="projectileSpeed">the projectile speed</param>
        /// <param name="projectileDamage">cusotm damage</param>
        /// <param name="direction">travel direction</param>
        /// <param name="team">team it belongs to</param>
        /// <param name="damageType">set damage type</param>
        public Projectile(Game game, SpriteBatch spriteBatch, Vector2 position, int projectileSpeed, int projectileDamage, Vector2 direction, TeamType team, DamageType damageType) : base(game, spriteBatch, position)
        {
            Speed = projectileSpeed;
            Damage = projectileDamage;
            this.direction = direction;
            Team = team;
            DamageType = damageType;
        }

        /// <summary>
        /// Remove from game and delete
        /// </summary>
        /// <param name="projectile"></param>
        public void EndLife(Projectile projectile)
        {
            if (projectile != null)
            {
                Game1.ACTIVESCENE.DeleteGameComponent(projectile);
            }
        }

        /// <summary>
        /// Check if sprite is impacting any solid tiles (rocks and stuff)
        /// </summary>
        public void CheckTileImpact()
        {
            for (int i = 0; i < TileManager.Instance.gameTiles.GetLength(0); i++)
            {
                for (int j = 0; j < TileManager.Instance.gameTiles.GetLength(1); j++)
                {
                    if (TileManager.Instance.gameTiles[i, j].BlocksProjectiles)
                    {

                        if (TileManager.Instance.gameTiles[i, j].GetBounds().Intersects(GetBounds()))
                        {
                            //Debug.WriteLine("Collision detected");
                            Game1.ACTIVESCENE.DeleteItemSprite(this);
                            return;
                        }
                    }

                }

            }
        }

        /// <summary>
        /// Checks if hits anything and if that thing should be damaged
        /// </summary>
        public void CheckSpriteImpact()
        {

            List<Sprite> items = TileManager.Instance.itemSprites;

            for (int i = 0; i < items.Count; i++)
            {
                if (GetBounds().Intersects(items[i].GetBounds()))
                {

                    Character hitChar = items[i] as Character;
                    if (hitChar != null)
                    {
                        //if we aren't neutral then check for potential team hit
                        if (hitChar.Team != TeamType.Neutral)
                        {
                            //if we are hitting our own character/team then do nothing else and keep moving
                            if (hitChar.Team == Team)
                            {
                                return;
                            }
                        }

                    }

                    //check if object has health, if so and we got this far, then hurt it
                    if (items[i] is IHealth)
                    {
                        IHealth hitItem = items[i] as IHealth;

                        if(DamageType == DamageType.Normal)
                        {
                            hitItem.Damage(Damage);
                        }
                        else
                        {
                            hitItem.Damage(Damage, DamageType);
                        }
                        
                    }

                    //destroy projectile
                    Game1.ACTIVESCENE.DeleteItemSprite(this);
                }
            }

        }

        /// <summary>
        /// Checks if hits anything and if that thing should be damaged
        /// </summary>
        public void CheckSpriteImpact(bool goThroughCharacters)
        {

            List<Sprite> items = TileManager.Instance.itemSprites;

            for (int i = 0; i < items.Count; i++)
            {
                if (GetBounds().Intersects(items[i].GetBounds()))
                {

                    Character hitChar = items[i] as Character;
                    if (hitChar != null)
                    {
                        //if we aren't neutral then check for potential team hit
                        if (hitChar.Team != TeamType.Neutral)
                        {
                            //if we are hitting our own character/team then do nothing else and keep moving
                            if (hitChar.Team == Team)
                            {
                                return;
                            }
                        }
                    }

                    //check if object has health, if so and we got this far, then hurt it
                    if (items[i] is IHealth)
                    {
                        IHealth hitItem = items[i] as IHealth;

                        if (DamageType == DamageType.Normal)
                        {
                            hitItem.Damage(Damage);
                        }
                        else
                        {
                            hitItem.Damage(Damage, DamageType);
                        }

                    }

                    //destroy projectile
                    if(!goThroughCharacters)
                    {
                        Game1.ACTIVESCENE.DeleteItemSprite(this);
                    }
                    
                }
            }

        }

    }
}
