using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using static System.Net.Mime.MediaTypeNames;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using TestProject_01.Managers;

namespace TestProject_01
{
    public class TitleScreen : DrawableGameComponent
    {
        Game1 g;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 pos;

        private Texture2D titleText;
        private Rectangle srcRectTT;
        private Vector2 posTT;
        int widthTT;
        int heightTT;

        public TitleScreen(Game game,SpriteBatch spriteBatch, Texture2D tex, Rectangle srcRect, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.pos = position;

            CreateTitleText();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, pos, srcRect, Color.White);
            spriteBatch.Draw(titleText, posTT, srcRectTT, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateTitleText()
        {
            titleText = SContentManager.instance.texTitleText;
            srcRectTT = new Rectangle(0, 0, titleText.Width, titleText.Height);
            widthTT = titleText.Width;
            heightTT = titleText.Height;
            posTT = new Vector2((Shared.stage.X / 2) - (widthTT / 2), Shared.stage.Y / 3);
        }

    }

    
}
