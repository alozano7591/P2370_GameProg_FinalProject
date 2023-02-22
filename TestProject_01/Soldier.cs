/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using System;
using TestProject_01.Managers;
using TestProject_01.Scenes;
using TestProject_01.UI;
using TestProject_01.Weapons;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace TestProject_01
{
    /// <summary>
    /// An AI class for a soldier character on field. Used for both enemy and friendly soldiers
    /// </summary>
    public class Soldier : Character, IHealth
    {
        //Basic AI
        public enum AgressionState { Gaurd, Attack, Charge};
        public int accuracy = 65;
        public float shootingRange = 350f;
        public bool enemyInRange;
        public bool isShooting;

        //these variables allow for pausing in between shots
        public int shotsBeforeReload = 6;
        public int currentShots = 0;
        public int reloadTime = 200;
        private int currentReloadCount = 0;
        private bool reloading = false;

        //weapon variables
        private int capRifle = 2;
        private int reloadRifle = 180;
        private int capMG = 3;
        private int reloadMG = 200;
        private int capShotgun = 1;
        private int reloadShotgun = 200;
        private int capFlameThrower = 5;
        private int reloadFlameThrower = 180;

        public bool isAlive = true;

        public float Speed { get; set; } = 4;

        public Vector2 velocity { get; set; }

        //Animation Values
        public int spriteSheetX = 0;        //the current X position of the animation
        public int spriteSheetY = 0;        //the current Y position of the animation
        const int _animationTickRate = 6;   //frames between animation switches
        int animationTick = 0;              //used to track animation tick count

        //Health stuff
        public int hp = 0;
        public int totalHP = 2;

        //life stuff
        public int deathDeleteDelay = 300;
        private int deleteCount = 0;

        private Color colorMultiplier = Color.White;
        private Color hurtColor = Color.Red;

        public PlayerUI playerUI;

        //the following three variables make it so that character doesn't get damaged every frame
        private const int customInvicibilityPeriod = 20;
        //private int hitInvicibilityCounter = 0;
        //private bool hitInvincibleOn = false;

        public Weapon weapon;

        public GameScene ActiveScene { get; set; }

        public float rotationInRads;

        public int pointValue = 10;

        /// <summary>
        /// Constructor with minimum props
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">spawn pos</param>
        public Soldier(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {

            Width = characterWidth;
            Height = characterHeight;

            velocity = Direction * Speed;
            SpriteColor = Color.White;

            Tex = Tex = SContentManager.instance.texCivilian01;

            SpriteSheetRect = GetSourceRect(0, 0);

            hp = totalHP;

            weapon = new Rifle(game, spriteBatch, this);

            hitInvicibilityPeriod = customInvicibilityPeriod;

        }

        /// <summary>
        /// Creates character with default sprite values, along with team association
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="team">The team this character is on</param>
        public Soldier(Game game, SpriteBatch spriteBatch, Vector2 position, TeamType team) : base(game, spriteBatch, position)
        {

            Width = characterWidth;
            Height = characterHeight;

            velocity = Direction * Speed;
            SpriteColor = Color.White;

            this.Team= team;
            Tex = Tex = SContentManager.instance.texCivilian01;

            SetTeam(team);
            spriteSheetCountX = 4;
            spriteSheetCountY = 4;

            SpriteSheetRect = GetSourceRect(0, 0);

            hp = totalHP;
            isAlive = true;

            GetWeapon((Game1)game, spriteBatch, 0);


            hitInvicibilityPeriod = customInvicibilityPeriod;

            
        }

        /// <summary>
        /// Creates character with default sprite values, along with team association
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the spritebatch</param>
        /// <param name="position">spawn pos</param>
        /// <param name="team">team</param>
        /// <param name="weaponNum">weapon</param>
        public Soldier(Game game, SpriteBatch spriteBatch, Vector2 position, TeamType team, int weaponNum) : base(game, spriteBatch, position)
        {

            Width = characterWidth;
            Height = characterHeight;

            velocity = Direction * Speed;
            SpriteColor = Color.White;

            this.Team = team;
            Tex = Tex = SContentManager.instance.texCivilian01;

            SetTeam(team);
            spriteSheetCountX = 4;
            spriteSheetCountY = 4;

            SpriteSheetRect = GetSourceRect(0, 0);

            hp = totalHP;
            isAlive = true;

            GetWeapon((Game1)game, spriteBatch, weaponNum);


            hitInvicibilityPeriod = customInvicibilityPeriod;


        }

        /// <summary>
        /// constructor with sprite counts
        /// </summary>
        /// <param name="game">teh game</param>
        /// <param name="spriteBatch">the batch</param>
        /// <param name="tex">the texture</param>
        /// <param name="position">spawn pos</param>
        /// <param name="sheetCountX">number of sprite X</param>
        /// <param name="sheetCountY">number of sprites Y</param>
        public Soldier(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int sheetCountX, int sheetCountY) : base(game, spriteBatch, tex, position)
        {

            Width = characterWidth;
            Height = characterHeight;

            spriteSheetCountX = sheetCountX;
            spriteSheetCountY = sheetCountY;

            SpriteSheetRect = GetSourceRect(0, 0);

            hp = totalHP;
            isAlive = true;

            GetWeapon((Game1)game, spriteBatch, 0);

            hitInvicibilityPeriod = customInvicibilityPeriod;

        }

        /// <summary>
        /// Set up the soldier team
        /// </summary>
        /// <param name="team">soldier's team</param>
        private void SetTeam(TeamType team)
        {
            switch (team)
            {
                case TeamType.Green:
                    colorMultiplier = Color.Green;
                    break;
                case TeamType.Grey:
                    //colorMultiplier = Color.Gray;
                    Tex = SContentManager.instance.texGraySoldier;
                    break;
                case TeamType.Tan:
                    //colorMultiplier = Color.Tan;
                    Tex = SContentManager.instance.texTanSoldier;
                    break;
                case TeamType.Neutral:
                    colorMultiplier = Color.White;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// assign weapon and stats
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="spriteBatch">the batch</param>
        /// <param name="weaponNum">weapon number</param>
        public void GetWeapon(Game1 game, SpriteBatch spriteBatch, int weaponNum)
        {
            switch (weaponNum)
            {
                case 0:
                    weapon = new Rifle(game, spriteBatch, this);
                    weapon.ShotCapacity = capRifle;
                    weapon.CoolDown = 100;
                    weapon.ReloadTime = reloadRifle;
                    break;
                case 1:
                    weapon = new MachineGun(game, spriteBatch, this);
                    weapon.ShotCapacity = capMG;
                    weapon.ReloadTime = reloadMG;
                    pointValue = 20;
                    break;
                case 2:
                    weapon = new Shotgun(game, spriteBatch, this);
                    weapon.ShotCapacity = capShotgun;
                    weapon.ReloadTime = reloadShotgun;
                    pointValue = 25;
                    break;
                case 3:
                    weapon = new FlameThrower(game, spriteBatch, this);
                    weapon.ShotCapacity = capFlameThrower;
                    weapon.ReloadTime = reloadFlameThrower;
                    pointValue = 50;
                    break;
                default:
                    weapon = new Rifle(game, spriteBatch, this);
                    weapon.ShotCapacity = capRifle;
                    weapon.ReloadTime = reloadRifle;
                    break;
            }
        }

        /// <summary>
        /// update look
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            if(isAlive)
            {
                if (PlayerTestie.instance != null)
                {
                    if (PlayerTestie.instance.Team != Team)
                    {
                        if (PlayerTestie.instance.GetBounds().Intersects(GetBounds()))
                        {
                            PlayerTestie.instance.Damage(1);
                        }
                    }

                }

                if (weapon != null)
                {
                    weapon.CountCoolDown();
                }

                CheckIfEnemyInRange();
                weapon.ManageShots();

                DamageInvincibilityTick();
            }
            else if (isAlive == false)
            {

                if (deleteCount >= deathDeleteDelay)
                {
                    CallDelete();
                }
                deleteCount++;

            }

            //AnimateSpriteSheet(spriteSheetCoord, spriteSheetCountX);
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

            if (weapon != null)
            {
                _spriteBatch.Draw(
                weapon.Tex,
                GetBounds(),
                SpriteSheetRect,
                colorMultiplier);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Damage our player
        /// </summary>
        /// <param name="damageAmt"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Damage(int damageAmt)
        {
            hp -= damageAmt;
            
            if (hp <= 0 && isAlive == true)
            {

                Die();
            }
            else
            {
                //we are still alive so do hit grunt
                HitGrunt();
            }
        }

        /// <summary>
        /// take damage and do things
        /// </summary>
        /// <param name="damageAmt">damage amount</param>
        /// <param name="damageType">type of damage</param>
        public void Damage(int damageAmt, DamageType damageType)
        {
            hp -= damageAmt;

            if (hp <= 0 && isAlive)
            {

                Die(damageType);
            }
            else
            {
                //we are still alive so do hit grunt
                HitGrunt();
            }
        }

        /// <summary>
        /// Play sound of soldier grunting from hit.
        /// Method makes it so that i can change sound once here 
        /// while having the exact same call for all instances
        /// </summary>
        public void HitGrunt()
        {
            if (!isAlive)
                return;

            hitGruntSound.Play();
        }

        public void Heal(int healAmt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// End the life 
        /// </summary>
        private void Die()
        {
            isAlive = false;
            deathNormalSound.Play();
            //weapon.StopSounds();
            deathDeleteDelay = 30;
        }

        /// <summary>
        /// Die, but with a special type 
        /// </summary>
        /// <param name="damageType">Damage type for death</param>
        private void Die(DamageType damageType)
        {

            isAlive = false;
            switch (damageType)
            {
                case DamageType.Normal:
                    break;
                case DamageType.Fire:
                    //deathFireSound01.Play();
                    PlayFireScream();

                    Vector2 spawnPos = new Vector2(Position.X - 12f, Position.Y);
                    Bonfire newFire = new Bonfire(g, _spriteBatch, spawnPos, 300);
                    TileManager.Instance.SpawnWorldSprite(newFire);

                    deathDeleteDelay = 180;
                    break;
                case DamageType.Explosion:
                    break;
                default:
                    break;
            }

            //weapon.StopSounds();

            ScoreManager.AddToCurrentScore(pointValue);

        }



        /// <summary>
        /// Check if enemy in range
        /// </summary>
        public void CheckIfEnemyInRange()
        {

            if(Team != TeamType.Green)
            {
                
                if(Vector2.Distance(PlayerTestie.instance.Position, Position) <= shootingRange)
                {
                    if(!weapon.Reloading)
                    {
                        currentShots++;
                        weapon.Fire(GetTargetDirection(PlayerTestie.instance.Position));
                        
                    }
                    else
                    {
                        //weapon.ReleaseFire();
                    }
                        
                }
            }

        }


        /// <summary>
        /// Aims at the target. 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector2 GetTargetDirection(Vector2 target)
        {
            Vector2 direction = Vector2.Normalize(Position - target) * -1;


            return GetShotVariance(accuracy, direction); 
        }

        /// <summary>
        /// Use this when deleting sprite from game
        /// </summary>
        public void CallDelete()
        {
            Game1.ACTIVESCENE.DeleteItemSprite(this);
        }

        /// <summary>
        /// Much like weapon variance, but adds variance to the actual aim of the soldier.
        /// </summary>
        /// <param name="angleVariance">desired accuracy</param>
        /// <param name="aimDirection">the direction to shoot at</param>
        /// <returns></returns>
        public Vector2 GetShotVariance(int angleVariance, Vector2 aimDirection)
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

    }
}
