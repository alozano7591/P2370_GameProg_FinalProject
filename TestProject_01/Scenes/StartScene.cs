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

namespace TestProject_01.Scenes
{
    /// <summary>
    /// the first scene the player will see
    /// </summary>
    public class StartScene : GameScene
    {

        //menu will be declared
        public MenuComponent menu { get; set; }

        private SpriteBatch spriteBatch;
        Game1 g;

        SoundEffect titleSong;

        TitleScreen titleBackground;
        Texture2D texTitleBackground;

        string[] menuItems = { "Start game", "Help", "High score", "Credit", "Quit" };

        /// <summary>
        /// Our constructor for the Start scene
        /// </summary>
        /// <param name="game">the game</param>
        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            titleSong = SContentManager.instance.titleSong;

            SpriteFont regular = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilight = game.Content.Load<SpriteFont>("fonts/hilightedFont");

            GenerateTitleScreen();

            menu = new MenuComponent(game, spriteBatch, regular, hilight, menuItems);
            
            this.components.Add(menu);

            //ContentManager.instance.PlaySong(titleSong, MusicVolume, true);



        }

        public void GenerateTitleScreen()
        {
            texTitleBackground = SContentManager.instance.texTitleBackground;
            Rectangle rect = new Rectangle(0, 0, (int)texTitleBackground.Width, (int)texTitleBackground.Height);
            titleBackground = new TitleScreen(g, spriteBatch, texTitleBackground, rect, Vector2.Zero);

            this.components.Add(titleBackground);
            
        }

        public override void RestartScene()
        {
            throw new NotImplementedException();
        }

        public override void GenerateUI()
        {
            throw new NotImplementedException();
        }
    }
}
