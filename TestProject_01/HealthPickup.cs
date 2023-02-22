/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Scenes;

namespace TestProject_01
{
    /// <summary>
    /// Health pickup that can be picked up by player
    /// </summary>
    public class HealthPickup : Pickup
    {

        //in game dimensions
        int pickUpSize = 32;

        //rectangles for in game bounding box and sprite sheet box
        public Rectangle SpriteSheetRect { get; set; }

        //the actual positions in the sprite sheet
        public int spriteSheetX = 0;
        public int spriteSheetY = 0;

        public int healAmount = 1;
        public bool itemUsed = false;

        public HealthPickup(Game game, GameScene scene, SpriteBatch spriteBatch, Vector2 position) : base(game, scene, spriteBatch, position)
        {

            SetOurTexture();
        }

        public override void SetOurTexture()
        {
            Tex = g.Content.Load<Texture2D>("images/items/Medipack"); 
        }

        public override void PickupItem()
        {
            PlayerTestie.instance.Heal(healAmount);
        }
    }
}
