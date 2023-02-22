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
using SharpDX.Direct2D1;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using TestProject_01.Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestProject_01.Scenes
{
    /// <summary>
    /// highscore scene
    /// </summary>
    public class HighscoreScene : GameScene
    {

        private SpriteBatch spriteBatch;
        Game1 g;

        SoundEffect titleSong;

        TitleScreen titleBackground;
        Texture2D texTitleBackground;

        HighScoreComponent highScoreComponent;

        public BasicTextInput textInput;

        public bool CheckScore { get; set; } = false;

        /// <summary>
        /// Create our highscore scene
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the batch</param>
        public HighscoreScene(Game game) : base(game)
        {

            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            GenerateUI();

            highScoreComponent = new HighScoreComponent(game, spriteBatch);
            this.components.Add(highScoreComponent);

            textInput = new BasicTextInput(game, spriteBatch);
            this.components.Add(textInput);
            ToggleTextInput(false);

        }

        /// <summary>
        /// check for high score
        /// </summary>
        public void CheckForHighScore()
        {
            //if(ScoreManager.CheckHighScore() == true)
            //{
            //    TakePlayersName();
            //}
        }

        /// <summary>
        /// set on or off
        /// </summary>
        /// <param name="activate">on or off</param>
        public void ToggleTextInput(bool activate)
        {
            textInput.Enabled = activate;
            textInput.Visible = activate;
        }

        public override void GenerateUI()
        {
            //throw new NotImplementedException();
        }

        public override void RestartScene()
        {
           // throw new NotImplementedException();
        }

        protected override void OnVisibleChanged(object sender, EventArgs args)
        {
            if (this.Visible)
            {
                if(ScoreManager.currentScore > 0)
                {
                    if(ScoreManager.CheckHighScore())
                    {
                        ToggleTextInput(true);
                    }
                    
                }
                else
                {
                    ToggleTextInput(false);
                }
                
            }
            base.OnVisibleChanged(sender, args);
        }
    }
}
