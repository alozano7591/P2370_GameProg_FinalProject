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

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using SharpDX.Direct2D1;
using TestProject_01.Managers;

namespace TestProject_01.UI
{
    /// <summary>
    /// Call for the player UI that appears on screen
    /// </summary>
    public class PlayerUI : DrawableGameComponent
    {

        public static PlayerUI Instance;

        Texture2D rectTex;

        public Game1 g;
        public SpriteBatch _spriteBatch { get; set; }

        public int HealthBarWidth { get; set; } = 120;
        private Rectangle healthOutterRect;
        private Rectangle healthFillRect;

        private Vector2 scorePos = new Vector2(5, 10);

        private PlayerTestie ourPlayer;

        private SpriteFont scoreFont;
        private int score = 0;

        /// <summary>
        /// Constructor for ui with least variables
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="player">the player object</param>
        public PlayerUI(Game game, SpriteBatch spriteBatch, PlayerTestie player) : base(game)
        {
            this.g = (Game1)game;
            _spriteBatch = spriteBatch;
            ourPlayer = player;

            scoreFont = SContentManager.instance.fontRegular;

            UpdateHealthBar(player.hp, player.totalHP);
            Instance = this;

            //rectTex = g.Content.Load<Texture2D>("images/1pxWhite");
            rectTex = SContentManager.instance.texWhitePixel;
        }

        /// <summary>
        /// Whenever health is changed (either by damage or healing) send player's health info here
        /// </summary>
        /// <param name="currentHp">the current hp (I know obvious)</param>
        /// <param name="totalHp">total hp</param>
        public void UpdateHealthBar(int currentHp, int totalHp)
        {
            int healthFraction = HealthBarWidth / totalHp;

            healthOutterRect = new Rectangle((int)scorePos.X, (int)scorePos.Y + 50, HealthBarWidth, 30);
            healthFillRect = new Rectangle((int)scorePos.X, (int)scorePos.Y + 50, healthFraction * currentHp, 30);
        }


        public void UpdateScore(int mainScore)
        {
            if(score != mainScore)
            {
                score= mainScore;

            }
        }

        /// <summary>
        /// Once 
        /// </summary>
        public void DrawHealthBar()
        {

            //Texture2D rectTex = g.Content.Load<Texture2D>("images/1pxWhite");

            _spriteBatch.Draw(rectTex, healthOutterRect, Color.Red);
            _spriteBatch.Draw(rectTex, healthFillRect, Color.Green);

        }

        /// <summary>
        /// Draw call
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            _spriteBatch.DrawString(scoreFont, "Score: " + score, scorePos, Color.Black);
            DrawHealthBar();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
