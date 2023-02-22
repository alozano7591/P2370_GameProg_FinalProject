/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace TestProject_01
{
    /// <summary>
    /// Higschore component that shows highscores
    /// </summary>
    public class HighScoreComponent : DrawableGameComponent
    {
        Game1 g;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private Vector2 position;

        string titleText = "HighScores";
        string creditsText = "This is a list of our finest soldiers:";
        string noScoresText = "No scores yet";

        List<string> scores = new List<string>();

        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;

        Texture2D background;
        Texture2D whiteBox;

        private SpriteFont titleFont;
        private SpriteFont normalFont;

        private Vector2 titlePos;

        /// <summary>
        /// Create our highscore component
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the batch</param>
        public HighScoreComponent(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.regularFont = SContentManager.instance.fontRegular;
            this.hilightFont = SContentManager.instance.fontHilight;
            this.titleFont = SContentManager.instance.fontTitle;

            background = SContentManager.instance.texWhitePixel;
            whiteBox = SContentManager.instance.texWhitePixel;
            titleFont = SContentManager.instance.fontTitle;
            normalFont = SContentManager.instance.fontRegular;

            position = new Vector2(Shared.stage.X / 2, titlePos.Y + 150);

            titlePos = new Vector2(Shared.stage.X / 2, 50f);
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime">d time</param>
        public override void Update(GameTime gameTime)
        {
            if(g.highscoreScene.Enabled)
            {
               // if(ScoreManager.currentScore > 0)
               // {
               //     if (ScoreManager.CheckHighScore())
               //     {
               //         //g.highscoreScene.CheckForHighScore();
               //     }
               // }
               

               //if(ScoreManager.currentScore > 0 && !g.highscoreScene.textInput.Enabled)
               // {
               //     g.highscoreScene.ToggleTextInput(true);
               // }

            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Draw call
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), null, Color.Black);

            spriteBatch.DrawString(titleFont, titleText, titlePos - GetTextCenteredPosition(titleFont, titleText), regularColor);

            spriteBatch.DrawString(normalFont, creditsText, position - GetTextCenteredPosition(normalFont, creditsText), regularColor);

            spriteBatch.DrawString(normalFont, ScoreManager.currentScore.ToString(), position - GetTextCenteredPosition(normalFont, creditsText) + new Vector2(0, 50)
                , regularColor);

            tempPos.Y += hilightFont.LineSpacing;

            if(LookForScores())
            {
                for (int i = 0; i < ScoreManager.highScores.Count; i++)
                {

                    spriteBatch.DrawString(normalFont, ScoreManager.highScores[i].name,
                        tempPos - new Vector2(25, 0), hilightColor);

                    spriteBatch.DrawString(normalFont, ScoreManager.highScores[i].score.ToString(),
                        tempPos + new Vector2(25, 0), regularColor);

                    tempPos.Y += hilightFont.LineSpacing;

                }
            }
            else
            {
                spriteBatch.DrawString(normalFont, noScoresText,
                        tempPos- GetTextCenteredPosition(normalFont, noScoresText), hilightColor);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// check if there are any highscores, if so then return true
        /// </summary>
        /// <returns></returns>
        private bool LookForScores()
        {
            if(ScoreManager.highScores != null)
            {
                if(ScoreManager.highScores.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// helps center text on screen
        /// </summary>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private Vector2 GetTextCenteredPosition(SpriteFont spriteFont, string text)
        {
            Vector2 centeredPos = new Vector2(1, 0);

            centeredPos = centeredPos * new Vector2(spriteFont.MeasureString(text).X / 2, 0);

            return centeredPos;
        }


    }
}
