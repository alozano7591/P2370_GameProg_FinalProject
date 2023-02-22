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
    /// shows our help stuff
    /// </summary>
    public class HelpComponent : DrawableGameComponent
    {

        Game1 g;
        private SpriteBatch spriteBatch;
        private Vector2 position;

        string titleText = "Help and Info";
        string subTitleText = "Weclome to the fight soldier!";
        string description = "You are a soldier and you need to shoot the bad guys.\n" +
            "The bad guys took our stuff and are taking peoples' stuff.\n" +
            "Go and put bullets in them so they can stop. We tried asking nicely\n" +
            "but they shot at us and now half of us are dead. Please avenge my dead\n" +
            "cat. You are the only one left with legs.";
        string controlsTitle = "Controls";
        string controlsText = "Movement controls:\n" +
            "Left: press 'a' or left key \n " +
            "Right: press 'd' or right key \n" +
            "Up: press 'w' or down key\n" +
            "Down: press 's' or down key." +
            "To Shoot: press spacebar \n" +
            "Go back / menu: press escape";

        private Color normalColor = Color.White;
        private Color hilightColor = Color.Red;

        Texture2D background;
        Texture2D whiteBox;

        private SpriteFont titleFont;
        private SpriteFont normalFont;
        private SpriteFont hilightFont;

        private Vector2 titlePos;

        /// <summary>
        /// Create our highscore component
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the batch</param>
        public HelpComponent(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.normalFont = SContentManager.instance.fontRegular;
            this.hilightFont = SContentManager.instance.fontHilight;
            this.titleFont = SContentManager.instance.fontTitle;

            background = SContentManager.instance.texWhitePixel;
            whiteBox = SContentManager.instance.texWhitePixel;

            position = new Vector2(Shared.stage.X / 2, titlePos.Y + 150);

            titlePos = new Vector2(Shared.stage.X / 2, 50f);
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime">d time</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();


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

            spriteBatch.DrawString(titleFont, titleText, titlePos - GetTextCenteredPosition(titleFont, titleText), normalColor);

            spriteBatch.DrawString(hilightFont, subTitleText, position - GetTextCenteredPosition(hilightFont, subTitleText), normalColor);

            tempPos.Y += hilightFont.LineSpacing + 25;

            spriteBatch.DrawString(normalFont, description, tempPos - GetTextCenteredPosition(normalFont, description), normalColor);

            tempPos.Y += hilightFont.LineSpacing + 150;

            spriteBatch.DrawString(hilightFont, controlsTitle, tempPos - GetTextCenteredPosition(hilightFont, controlsTitle), normalColor);

            tempPos.Y += hilightFont.LineSpacing + 25;

            spriteBatch.DrawString(normalFont, controlsText, tempPos - GetTextCenteredPosition(normalFont, controlsText), normalColor);

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
