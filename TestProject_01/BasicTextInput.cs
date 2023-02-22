using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;
using TestProject_01.Scenes;

namespace TestProject_01
{
    public class BasicTextInput : DrawableGameComponent
    {

        private Game1 g;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        public int selectedIndex { get; set; }

        int menuWidth = 250;
        int menuHeight = 150;
        int menuDif = 10;

        private SpriteFont titleFont;
        private SpriteFont normalFont;
        private SpriteFont hilightedFont;

        Texture2D background;
        Texture2D whiteBox;

        private KeyboardState oldState;

        private string inputLabel = "Enter your name";
        private string inputString = "";
        private int characterLimit = 10;

        //public bool IsActive { get; set; } = false;

        /// <summary>
        /// constructor for our menu componenten
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">sprite batch</param>
        /// <param name="menus">the menu options</param>
        public BasicTextInput(Game game, SpriteBatch spriteBatch) : base(game)
        {

            g = (Game1)game;
            this.spriteBatch = spriteBatch;
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2 - 70);

            this.normalFont = SContentManager.instance.fontRegular;
            this.hilightedFont = SContentManager.instance.fontHilight;
            this.titleFont = SContentManager.instance.fontTitle;

            background = SContentManager.instance.texWhitePixel;
            whiteBox = SContentManager.instance.texWhitePixel;

        }

        

        /// <summary>
        /// update loop
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            if (!g.highscoreScene.Enabled)
                return;


            KeyboardState ks = Keyboard.GetState();
            
            if (ks.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
            {
                InputCharacter('q');
            }
            if (ks.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            {
                InputCharacter('w');
            }
            if (ks.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
            {
                InputCharacter('e');
            }
            if (ks.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
            {
                InputCharacter('r');
            }
            if (ks.IsKeyDown(Keys.T) && oldState.IsKeyUp(Keys.T))
            {
                InputCharacter('t');
            }
            if (ks.IsKeyDown(Keys.Y) && oldState.IsKeyUp(Keys.Y))
            {
                InputCharacter('y');
            }
            if (ks.IsKeyDown(Keys.U) && oldState.IsKeyUp(Keys.U))
            {
                InputCharacter('u');
            }
            if (ks.IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
            {
                InputCharacter('i');
            }
            if (ks.IsKeyDown(Keys.O) && oldState.IsKeyUp(Keys.O))
            {
                InputCharacter('o');
            }
            if (ks.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
            {
                InputCharacter('p');
            }

            //nextrow
            if (ks.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                InputCharacter('a');
            }
            if (ks.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                InputCharacter('s');
            }
            if (ks.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
            {
                InputCharacter('d');
            }
            if (ks.IsKeyDown(Keys.F) && oldState.IsKeyUp(Keys.F))
            {
                InputCharacter('f');
            }
            if (ks.IsKeyDown(Keys.G) && oldState.IsKeyUp(Keys.G))
            {
                InputCharacter('g');
            }
            if (ks.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
            {
                InputCharacter('h');
            }
            if (ks.IsKeyDown(Keys.J) && oldState.IsKeyUp(Keys.J))
            {
                InputCharacter('j');
            }
            if (ks.IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K))
            {
                InputCharacter('k');
            }
            if (ks.IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L))
            {
                InputCharacter('l');
            }

            //third row
            if (ks.IsKeyDown(Keys.Z) && oldState.IsKeyUp(Keys.Z))
            {
                InputCharacter('z');
            }
            if (ks.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
            {
                InputCharacter('x');
            }
            if (ks.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
            {
                InputCharacter('c');
            }
            if (ks.IsKeyDown(Keys.V) && oldState.IsKeyUp(Keys.V))
            {
                InputCharacter('v');
            }
            if (ks.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
            {
                InputCharacter('b');
            }
            if (ks.IsKeyDown(Keys.N) && oldState.IsKeyUp(Keys.N))
            {
                InputCharacter('n');
            }
            if (ks.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
            {
                InputCharacter('m');
            }

            //delete stuff
            if (ks.IsKeyDown(Keys.Delete) && oldState.IsKeyUp(Keys.Delete))
            {
                DeleteLastCharacter();
            }
            if (ks.IsKeyDown(Keys.Delete) && oldState.IsKeyUp(Keys.Delete))
            {

            }

            if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                
                if(inputString.Length > 0)
                {
                    ScoreManager.CreateNewScore(inputString);
                    this.Visible = false;
                    this.Enabled = false;
                }
                
            }

            oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// constructor for our menu componenten
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">sprite batch</param>
        /// <param name="menus">the menu options</param>
        public override void Draw(GameTime gameTime)
        {

            Vector2 tempPos = position;

            int tempWidth = menuWidth;
            int tempHeight = menuHeight;

            spriteBatch.Begin();

            //spriteBatch.Draw(background, Vector2.Zero, null, , null, Color.Black);
            //spriteBatch.Draw(background, new Rectangle((int)Shared.stage.X/2, (int)Shared.stage.Y /2, 500, 500), null, Color.Black, 0, Shared.midPoint, 0, 0);
            spriteBatch.Draw(background,
                new Rectangle((int)(Shared.stage.X / 2) - (menuWidth / 2), (int)(Shared.stage.Y / 2) - (menuHeight / 2), menuWidth, menuHeight)
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

            tempPos.Y += hilightedFont.LineSpacing;

            //the text
            spriteBatch.DrawString(hilightedFont, inputLabel,
                tempPos - GetTextCenteredPosition(hilightedFont, inputLabel), hilightColor);
            tempPos.Y += hilightedFont.LineSpacing;

            spriteBatch.DrawString(normalFont, inputString,
                tempPos - GetTextCenteredPosition(normalFont, inputString), regularColor);
            tempPos.Y += normalFont.LineSpacing;

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// input characters
        /// </summary>
        /// <param name="character"></param>
        private void InputCharacter(char character)
        {
            if(inputString.Length < characterLimit)
            {
                inputString += character;
            }
            
        }

        /// <summary>
        /// delete character
        /// </summary>
        private void DeleteLastCharacter()
        {
            if(inputString != null)
            {
                if (inputString.Length > 0)
                {
                    inputString = inputString.Remove(inputString.Length - 1, 1);
                }
            }
            
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
