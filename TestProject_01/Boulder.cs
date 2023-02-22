using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;

namespace TestProject_01
{
    public class Boulder : Sprite, IHealth
    {

        public int spriteSheetLengthX = 2;
        public int spriteSheetLengthY = 2;

        public int RockWidth { get; set; } = 64;
        public int RockHeight { get; set; } = 64;

        public int xSheetPos = 0;
        public int ySheetPos = 0;

        public int RockNum { get; set; } = 1;

        public Boulder(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game, spriteBatch, position)
        {

            SetRockSpriteSheet();

        }

        public Boulder(Game game, SpriteBatch spriteBatch, Vector2 position, int rockNum) : base(game, spriteBatch, position)
        {
            RockNum = rockNum;
            SetRockSpriteSheet();

        }

        public void SetRockSpriteSheet()
        {

            Tex = SContentManager.instance.texRocks;

            ImpedesPath = true;

            switch (RockNum)
            {
                case 1:
                    xSheetPos= 0;
                    ySheetPos= 0;
                    BoundsMultiplyer = .7f;
                    break;
                case 2:
                    xSheetPos = 1;
                    ySheetPos = 0;
                    BoundsMultiplyer = .25f;
                    break;
                case 3:
                    xSheetPos = 0;
                    ySheetPos = 1;
                    BoundsMultiplyer = .5f;
                    break;
                case 4:
                    xSheetPos = 1;
                    ySheetPos = 1;
                    BoundsMultiplyer = .6f;
                    break;
                default:
                    xSheetPos = 0;
                    ySheetPos = 0;
                    BoundsMultiplyer = .7f;
                    break;

            }

            spriteSheetCountX = spriteSheetLengthX;
            spriteSheetCountY = spriteSheetLengthY;

            //set sizes
            Width = RockWidth; Height = RockHeight;

            SpriteSheetRect = GetSourceRect(xSheetPos, ySheetPos);
        }

        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _spriteBatch.Draw(
                Tex,
                GetBounds(),
                SpriteSheetRect,
                SpriteColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Damage(int damageAmt)
        {
            //just blocks bullets
        }

        public void Damage(int damageAmt, DamageType damageType)
        {
            //does nothing but blocks bullets
        }

        public void Heal(int healAmt)
        {
            //does nothing
        }
    }
}
