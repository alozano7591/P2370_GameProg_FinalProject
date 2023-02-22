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
    /// Create our highscore component
    /// </summary>
    /// <param name="game">the game</param>
    /// <param name="spriteBatch">the batch</param>
    public class CreditsComponent : DrawableGameComponent
    {
        Game1 g;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private Vector2 position;

        string titleText = "Credits";
        string creditsText = "This game is brought to you by:";

        List<string> developers = new List<string> { "Alfredo Lozano", "Brooks Liu" };

        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;

        Texture2D background;
        Texture2D whiteBox;

        private SpriteFont titleFont;
        private SpriteFont normalFont;

        private Vector2 titlePos;


        public CreditsComponent(Game game,
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

            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 - 150);

            titlePos = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 - 200f);
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime">d time</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            //if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            //{
            //    selectedIndex++;
            //    if (selectedIndex == menuItems.Count)
            //    {
            //        selectedIndex = 0;
            //    }
            //}
            //if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            //{
            //    selectedIndex--;
            //    if (selectedIndex == -1)
            //    {
            //        selectedIndex = menuItems.Count - 1;
            //    }
            //}

            //oldState = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), null, Color.Black);

            spriteBatch.DrawString(titleFont, titleText, titlePos - GetTextCenteredPosition(titleFont, titleText), regularColor);

            spriteBatch.DrawString(normalFont, creditsText, position - GetTextCenteredPosition(normalFont, creditsText), regularColor);

            tempPos.Y += hilightFont.LineSpacing;

            for (int i = 0; i < developers.Count; i++)
            {

                spriteBatch.DrawString(normalFont, developers[i],
                    tempPos - GetTextCenteredPosition(normalFont, developers[i]), regularColor);
                tempPos.Y += hilightFont.LineSpacing;

            }

            spriteBatch.End();

            base.Draw(gameTime);
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
