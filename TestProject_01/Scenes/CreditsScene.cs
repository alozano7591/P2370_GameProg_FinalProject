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
using Color = Microsoft.Xna.Framework.Color;

namespace TestProject_01.Scenes
{
    public class CreditsScene : GameScene
    {

        private SpriteBatch spriteBatch;
        Game1 g;

        CreditsComponent creditsComponent;

        /// <summary>
        /// Constructor for credits scene
        /// </summary>
        /// <param name="game">the game</param>
        public CreditsScene(Game game) : base(game)
        {

            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            creditsComponent = new CreditsComponent(game, spriteBatch);
            this.components.Add(creditsComponent);

        }

        public override void GenerateUI()
        {
            
        }

        public override void RestartScene()
        {
            throw new NotImplementedException();
        }

    }
}
