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
using Microsoft.Xna.Framework.Media;
using TestProject_01.Managers;

namespace TestProject_01.Scenes
{

    /// <summary>
    /// the game scenes
    /// </summary>
    public abstract class GameScene:DrawableGameComponent
    {
        public List<GameComponent> components { get; set; }

        public bool restartingScene = false;



        public float MusicVolume { get; set; } = .9f;

        public float SoundEffectVolume { get; set; } = .5f;

        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        protected GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }

        

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

        public void DeleteGameComponent(GameComponent gameComponent)
        {
            components.Remove(gameComponent);
            
        }


        public override void Update(GameTime gameTime)
        {

            for(int i = 0; i < components.Count; i++)
            {
                if (components[i].Enabled)
                {
                    components[i].Update(gameTime);
                }
                else if (components[i] == null)
                {
                    components[i].Dispose();
                    //components.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }


        //Abstract Methods

        public abstract void GenerateUI();

        public abstract void RestartScene();


        public void DeleteItemSprite(Sprite sprite)
        {

            components.Remove(sprite);

            if (TileManager.Instance != null)
            {
                TileManager.Instance.itemSprites.Remove(sprite);
            }

        }

    }
}
