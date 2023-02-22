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

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestProject_01.Scenes;
using TestProject_01.Weapons;
using Microsoft.Xna.Framework.Content;
using TestProject_01.Managers;

namespace TestProject_01
{
    public class WeaponPickup : Pickup
    {

        int spriteCountX = 2;
        int spriteCountY = 2;

        int spritePosX = 0;
        int spritePosY = 0;

        public enum WeaponType 
        {
            Rifle, 
            MachineGun, 
            Shotgun, 
            Bazooka, 
            FlameThrower
        };

        public WeaponType weaponType = WeaponType.Rifle;

        //public Color color= Color.White;

        public WeaponPickup(Game game, GameScene scene, SpriteBatch spriteBatch, Vector2 position) : base(game, scene, spriteBatch, position)
        {

            SetOurTexture();
            Tex = SContentManager.instance.texWeaponPickup;
            Width = 32;
            Height = 32;

        }

        public WeaponPickup(Game game, GameScene scene, SpriteBatch spriteBatch, Vector2 position, WeaponType type) : base(game, scene, spriteBatch, position)
        {
            SetOurTexture();
            Width = 32;
            Height = 32;
            weaponType = type;
            SetWeaponType(type);
        }


        public void SetWeaponType(WeaponType type)
        {

            switch (type)
            {
                case WeaponType.Rifle:
                    //SpriteColor = Color.Yellow;
                    spritePosX = 0; spritePosY = 1;
                    
                    break;
                case WeaponType.MachineGun:
                    //SpriteColor = Color.DarkOliveGreen;
                    spritePosX = 1; spritePosY = 0;
                    break;
                case WeaponType.Shotgun:
                    //SpriteColor = Color.Brown;
                    spritePosX = 0; spritePosY = 0;
                    break;
                case WeaponType.Bazooka:
                    SpriteColor = Color.GhostWhite;
                    spritePosX = 0; spritePosY = 0;
                    break;
                case WeaponType.FlameThrower:
                    //SpriteColor = Color.Red;
                    spritePosX = 1; spritePosY = 1;
                    break;
                default:
                    break;

            }
            spriteSheetCoord = new Vector2(spritePosX, spritePosY);

            SpriteSheetRect = GetSourceRect(spritePosX, spritePosY);

            weaponType = type;


        }

        public override void SetOurTexture()
        {
            //Tex = g.Content.Load<Texture2D>("images/weapons/GunsSheet");
            Tex = SContentManager.instance.texWeaponPickup;

            spriteSheetCountX = spriteCountX;
            spriteSheetCountY = spriteCountY;
        }

        public override void PickupItem()
        {

            Weapon newWeapon;

            switch (weaponType)
            {
                case WeaponType.Rifle:
                    newWeapon = new Rifle(g, _spriteBatch, PlayerTestie.instance);
                    AssignWeaponToCharacter(newWeapon, PlayerTestie.instance);
                    break;
                case WeaponType.MachineGun:
                    newWeapon = new MachineGun(g, _spriteBatch, PlayerTestie.instance);
                    AssignWeaponToCharacter(newWeapon, PlayerTestie.instance);
                    break;
                case WeaponType.Shotgun:
                    newWeapon = new Shotgun(g, _spriteBatch, PlayerTestie.instance);
                    AssignWeaponToCharacter(newWeapon, PlayerTestie.instance);
                    break;
                case WeaponType.Bazooka:
                    break;
                case WeaponType.FlameThrower:
                    newWeapon = new FlameThrower(g, _spriteBatch, PlayerTestie.instance);
                    AssignWeaponToCharacter(newWeapon, PlayerTestie.instance);
                    break;
                default:
                    newWeapon = new Rifle(g, _spriteBatch, PlayerTestie.instance);
                    AssignWeaponToCharacter(newWeapon, PlayerTestie.instance);
                    break;

            }

            
            
        }

        public void AssignWeaponToCharacter(Weapon weapon, PlayerTestie player)
        {

            player.playerWeapon = weapon;

        }
    }
}
