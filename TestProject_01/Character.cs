/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;

namespace TestProject_01
{
    /// <summary>
    /// Character class is the parent of all characters (playable and AI) in the game.
    /// </summary>
    public abstract class Character : Sprite
    {

        public TeamType Team { get; set; }
        public Vector2 Direction { get; set; } = Vector2.Zero;

        public int characterWidth = 40;
        public int characterHeight = 64;

        public float speed { get; set; } = 4;

        //makes character not bound to rule of reloading
        public bool OverridesReload { get; set; } = false;

        //the following three variables make it so that player doesn't get damaged every frame
        public int hitInvicibilityPeriod = 40;
        public int hitInvicibilityCounter = 0;
        public bool hitInvincibleOn = false;

        public Color colorMultiplier = Color.White;
        private Color hurtColor = Color.Red;

        //Death stuff
        public bool DeathActionStarted { get; set; }

        //sounds
        public SoundEffect hitGruntSound { get; set; }
        public SoundEffect deathNormalSound { get; set; }
        public List<SoundEffect> deathFireScreams = new List<SoundEffect>();
        public SoundEffect deathFireSound01 { get; set; }
        public SoundEffect deathFireSound02 { get; set; }

        protected Character(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {
            AssignSounds();
        }

        protected Character(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game, spriteBatch, tex, position)
        {
            AssignSounds();
        }

        /// <summary>
        /// Assign sounds here so that only one call needed per constructor. 
        /// Makes it easier to change here as well
        /// </summary>
        public void AssignSounds()
        {

            hitGruntSound = SContentManager.instance.sndManGrunt01;
            deathNormalSound = SContentManager.instance.sndManGrunt02;
            deathFireSound01 = SContentManager.instance.sndBurnScream1;
            deathFireSound02 = SContentManager.instance.sndBurnScream2;

            deathFireScreams.Add(deathFireSound01);
            deathFireScreams.Add(deathFireSound02);
        }


        /// <summary>
        /// When a player is hit, a count will start that will prevent the player from taking any damage.
        /// This gives the player a window of opportunity to run away.
        /// Also prevents player from recieving multiple hits for the same attack within multiple frames
        /// </summary>
        public void DamageInvincibilityTick()
        {

            if (hitInvincibleOn)
            {
                if (hitInvicibilityCounter < hitInvicibilityPeriod)
                {
                    colorMultiplier = hurtColor;
                    hitInvicibilityCounter++;
                }
                else
                {
                    hitInvincibleOn = false;
                    hitInvicibilityCounter = 0;
                    colorMultiplier = Color.White;
                }
            }

        }

        /// <summary>
        /// Use a random scream from the list of fire screams
        /// </summary>
        public void PlayFireScream()
        {
            int randomIndex;

            Random random = new Random();

            randomIndex = random.Next(deathFireScreams.Count);

            deathFireScreams[randomIndex].Play();
        }

    }
}
