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
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace TestProject_01
{
    /// <summary>
    /// unlike menu component, this is a simpler one for very simple controls and UI
    /// </summary>
    public class MenuComponent2 : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private List<string> menuItems;
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        public int selectedIndex { get; set; }

        public string titleText = "";
        private Vector2 titleTextPos;
        private Vector2 titleScale = new Vector2(2, 2);

        private KeyboardState oldState;


        public MenuComponent2(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string titleText,
            string[] menus) : base(game)
        {

            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList<string>();
            this.titleText = titleText;
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 + 50f);
            titleTextPos = new Vector2(Shared.stage.X / 2, (Shared.stage.Y / 2) + 20f);
        }

        public override void Update(GameTime gameTime)
        {
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

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;

            spriteBatch.Begin();

            spriteBatch.DrawString(hilightFont, titleText,
                        titleTextPos, hilightColor);

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

        private Vector2 GetTextCenteredPosition(SpriteFont spriteFont, string text)
        {
            Vector2 centeredPos = new Vector2(1, 0);

            centeredPos = centeredPos * new Vector2(spriteFont.MeasureString(text).X / 2, 0);

            return centeredPos;
        }


    }
}
