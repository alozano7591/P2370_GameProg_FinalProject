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
using System.Reflection.Metadata;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using TestProject_01.UI;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using TestProject_01.Managers;
using TestProject_01.Scenes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace TestProject_01
{
    public class PauseMenuComponent :DrawableGameComponent
    {
        private Game1 g;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        public int selectedIndex { get; set; }
        public List<string> menuItems = new List<string> {"Resume", "Main Menu", "Quit"};

        int menuWidth = 250;
        int menuHeight = 150;
        int menuDif = 10;

        private SpriteFont titleFont;
        private SpriteFont normalFont;
        private SpriteFont hilightedFont;

        Texture2D background;
        Texture2D whiteBox;

        private KeyboardState oldState;

        private ActionScene actionScene;

        /// <summary>
        /// constructor for our menu componenten
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">sprite batch</param>
        /// <param name="menus">the menu options</param>
        public PauseMenuComponent(Game game,
            SpriteBatch spriteBatch, ActionScene actionScene) : base(game)
        {

            g = (Game1)game;
            this.spriteBatch = spriteBatch;
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 - 70);

            this.regularFont = SContentManager.instance.fontRegular;
            this.hilightFont = SContentManager.instance.fontHilight;
            this.titleFont = SContentManager.instance.fontTitle;

            background = SContentManager.instance.texWhitePixel;
            whiteBox = SContentManager.instance.texWhitePixel;

            this.actionScene = actionScene;
        }

        /// <summary>
        /// constructor for our menu componenten
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">sprite batch</param>
        /// <param name="menus">the menu options</param>
        public PauseMenuComponent(Game game,
        SpriteBatch spriteBatch,
        string[] menus) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = spriteBatch;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 + 50f);

            background = SContentManager.instance.texWhitePixel;
            whiteBox = SContentManager.instance.texWhitePixel;


            this.regularFont = SContentManager.instance.fontRegular;
            this.hilightFont = SContentManager.instance.fontHilight;
            this.titleFont = SContentManager.instance.fontTitle;
        }

        /// <summary>
        /// update loop
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            if (!g.actionScene.Enabled)
                return;


            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// draw call
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;

            int tempWidth = menuWidth;
            int tempHeight = menuHeight;    

            spriteBatch.Begin();

            //spriteBatch.Draw(background, Vector2.Zero, null, , null, Color.Black);
            //spriteBatch.Draw(background, new Rectangle((int)Shared.stage.X/2, (int)Shared.stage.Y /2, 500, 500), null, Color.Black, 0, Shared.midPoint, 0, 0);
            spriteBatch.Draw(background, 
                new Rectangle((int)(Shared.stage.X / 2) - (menuWidth /2), (int)(Shared.stage.Y / 2) - (menuHeight / 2), menuWidth, menuHeight)
                , null, Color.Black);

            tempHeight -= menuDif;
            tempWidth -= menuDif;

            spriteBatch.Draw(whiteBox,
                new Rectangle((int)(Shared.stage.X / 2) - (tempWidth / 2), (int)(Shared.stage.Y / 2) - (tempHeight / 2), tempWidth, tempHeight)
                , null, Color.White);

            tempHeight -= menuDif;
            tempWidth -= menuDif;

            spriteBatch.Draw(background,
                new Rectangle((int)(Shared.stage.X / 2) - (tempWidth / 2), (int)(Shared.stage.Y / 2) - (tempHeight / 2), tempWidth, tempHeight)
                , null, Color.Black);

            tempPos.Y += hilightFont.LineSpacing;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i],
                        tempPos - GetTextCenteredPosition(hilightFont, menuItems[i]), hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                        tempPos - GetTextCenteredPosition(regularFont, menuItems[i]), regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// get center for text
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
