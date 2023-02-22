/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    public class HelpScene : GameScene
    {

        //menu will be declared
        public MenuComponent2 menu { get; set; }

        private SpriteBatch spriteBatch;
        Game1 g;

        string[] menuItems = { "Back", "Next" };

        HelpComponent helpComponent;

        public HelpScene(Game game) : base(game)
        {

            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            //GenerateTitleScreen();

            helpComponent = new HelpComponent(game, spriteBatch);

            this.components.Add(helpComponent);

        }
        

        public override void GenerateUI()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// writing description string here because anywhere else would be weird
        /// </summary>
        public void GenerateDescription()
        {
            
        }

        public override void RestartScene()
        {
            throw new NotImplementedException();
        }
    }
}
