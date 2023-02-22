/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using TestProject_01.Levels;
using TestProject_01.Scenes;
using TestProject_01.Weapons;
using static System.Net.Mime.MediaTypeNames;
using static TestProject_01.WeaponPickup;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace TestProject_01.Managers
{

    /// <summary>
    /// This is responsible for generating all tiles in the world and moving tiles around as player moves
    /// </summary>
    public class TileManager : DrawableGameComponent
    {
        public static TileManager Instance;

        private Game1 g;
        private GameScene _activeScene;
        public SpriteBatch _spriteBatch { get; set; }
        public Texture2D tex { get; set; }

        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }
        public Vector2 velocity { get; set; }
        public int tileSizeX = 64;
        public int tileSizeY = 64;

        private string[,] tileData;
        private List<string> itemData;

        public int spriteSheetCountX = 3;
        public int spriteSheetCountY = 2;

        public Rectangle spriteBounds;
        public Tile[,] gameTiles;

        public List<Sprite> itemSprites = new List<Sprite>();
        public List<Sprite> projectileSprites = new List<Sprite>();
        public List<Character> characterSprites = new List<Character>();

        string levelData;

        public string playerData;

        Level level;

        /// <summary>
        /// Constructor with assigned texture
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="activeScene">the active scene</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="tex">texture to use</param>
        public TileManager(Game game, GameScene activeScene, SpriteBatch spriteBatch, Texture2D tex) : base(game)
        {
            Instance = this;
            g = (Game1)game;
            _activeScene = activeScene;
            _spriteBatch = spriteBatch;
            this.tex = tex;
            //OpenLevelFile(path);
            //GenerateTiles();

            LoadFileLevelData();

        }

        /// <summary>
        /// Constructor with assigned texture with level object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="activeScene"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="level"></param>
        public TileManager(Game game, GameScene activeScene, SpriteBatch spriteBatch, Texture2D tex, Level level) : base(game)
        {
            Instance = this;
            g = (Game1)game;
            _activeScene = activeScene;
            _spriteBatch = spriteBatch;
            this.tex = tex;

            this.level = level;

            //OpenLevelFile(path);
            //GenerateTiles();

            LoadFileLevelData(level.DataFilePath);

        }

        /// <summary>
        /// Find out how many tiles needed X
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNeededTilesX()
        {
            int numTilesX = (int)Shared.stage.X / 64 + 1;

            return numTilesX;
        }

        /// <summary>
        /// find out how many tiles needed in the Y
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNeededTilesY()
        {
            int numTilesY = (int)Shared.stage.Y / 64 + 1;

            return numTilesY;
        }

        /// <summary>
        /// Fill screen with tiles
        /// </summary>
        public void GenerateTilesToFillScreen()
        {
            gameTiles = new Tile[GetNumberOfNeededTilesX(), GetNumberOfNeededTilesY()];

            Vector2 tilePos = new Vector2(0, 0);

            for (int i = 0; i < gameTiles.GetLength(0); i++)
            {
                for (int j = 0; j < gameTiles.GetLength(1); j++)
                {
                    tilePos = new Vector2(tileSizeX * i, tileSizeY * j);
                    gameTiles[i, j] = new Tile(Game, _spriteBatch, tex, tilePos, tileSizeX, tileSizeY, new Vector2(0, 0));

                }

            }
        }

        /// <summary>
        /// Recreate the tiles
        /// </summary>
        public void ReCreateTiles()
        {

            Array.Clear(gameTiles, 0, gameTiles.Length);

            //OpenLevelFile(path);

        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Update(GameTime gameTime)
        {
            if (PlayerTestie.instance != null)
            {
                if (PlayerTestie.instance.velocity.LengthSquared() > 0)
                {
                    //UpdateTilePositions(PlayerTestie.instance.velocity);
                    //UpdateWorldItemPositions(PlayerTestie.instance.velocity);
                    UpdateWorldPositionsBaseOnPlayer(PlayerTestie.instance.velocity);
                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Draw call
        /// </summary>
        /// <param name="gameTime">delta time</param>
        public override void Draw(GameTime gameTime)
        {
            //making point clamp so that sprites don't come out blurry
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            for (int i = 0; i < gameTiles.GetLength(0); i++)
            {
                for (int j = 0; j < gameTiles.GetLength(1); j++)
                {
                    _spriteBatch.Draw(
                    gameTiles[i, j].Tex,
                    gameTiles[i, j].GetBounds(),
                    gameTiles[i, j].GetSourceRect(),
                    Color.White
                    );
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Sets the position of everything in the world to exact positions based on the player's world position
        /// This is the equivalent of teleporting the player
        /// </summary>
        /// <param name="playerPos">Where the player is in the world</param>
        public void SetWorldTopPlayerPosition(Vector2 playerPos)
        {
            Debug.WriteLine($"Player world position is {playerPos}");
            Debug.WriteLine($"Screen center position is {Shared.midPoint}");
            //using center screen pos since player will always be in the center
            Vector2 centerPos = Shared.midPoint;

            //take the position the player is supposed to be in and add it to the camera and then add center pos to make sure tile is centered
            Vector2 displacement = (playerPos - centerPos) + new Vector2((float)tileSizeX / 2, (float)tileSizeY / 2);

            UpdateTilePositions(displacement);
            UpdateWorldItemPositions(displacement);
            //UpdateCharacterPositions(displacement);

            for (int i = 0; i < projectileSprites.Count; i++)
            {
                projectileSprites[i].Position += -displacement;

            }

        }

        /// <summary>
        /// Updates the position of everything in the world based on how the player is moving.
        /// </summary>
        /// <param name="pVelocity"></param>
        void UpdateWorldPositionsBaseOnPlayer(Vector2 pVelocity)
        {

            UpdateTilePositions(pVelocity);
            UpdateWorldItemPositions(pVelocity);
            //UpdateCharacterPositions(pVelocity);

            for (int i = 0; i < projectileSprites.Count; i++)
            {
                projectileSprites[i].Position += -pVelocity;

            }

        }

        /// <summary>
        /// Updated position of static tiles based on player's velocity
        /// </summary>
        /// <param name="pVelocity"></param>
        void UpdateTilePositions(Vector2 pVelocity)
        {

            for (int i = 0; i < gameTiles.GetLength(0); i++)
            {
                for (int j = 0; j < gameTiles.GetLength(1); j++)
                {
                    gameTiles[i, j].Position += -pVelocity;
                }
            }

        }

        /// <summary>
        /// Updates position of the items and other sprites in the world based on player's movement
        /// </summary>
        /// <param name="pVelocity"></param>
        void UpdateWorldItemPositions(Vector2 pVelocity)
        {

            for (int i = 0; i < itemSprites.Count; i++)
            {
                itemSprites[i].Position += -pVelocity;

            }

        }

        /// <summary>
        /// updates position of character sprites
        /// </summary>
        /// <param name="pVelocity"></param>
        void UpdateCharacterPositions(Vector2 pVelocity)
        {
            for (int i = 0; i < characterSprites.Count; i++)
            {
                characterSprites[i].Position += -pVelocity;

            }
        }

        /// <summary>
        /// Load the level data
        /// </summary>
        void LoadFileLevelData()
        {

            tileData = FileManager.LoadLevelData();

            GenerateTiles(tileData);
            GenerateWorldItems(FileManager.levelItemsList);

        }

        void LoadFileLevelData(string filePath)
        {

            tileData = FileManager.LoadLevelData(filePath);

            GenerateTiles(tileData);
            GenerateWorldItems(FileManager.levelItemsList);

        }

        /// <summary>
        /// Generate the level tiles
        /// </summary>
        /// <param name="tileStrings">tile strings</param>
        void GenerateTiles(string[,] tileStrings)
        {

            int tileRows = tileStrings.GetLength(0);
            int tileCols = tileStrings.GetLength(1);

            gameTiles = new Tile[tileRows, tileCols];

            for (int i = 0; i < tileStrings.GetLength(0); i++)
            {
                for (int j = 0; j < tileStrings.GetLength(1); j++)
                {

                    Vector2 tileSrcPos = Vector2.Zero;
                    Vector2 tileWorldPos = Vector2.Zero;

                    Tile.TileType type = Tile.TileType.Grass;


                    switch (tileStrings[i, j])
                    {
                        case "b":
                            tileSrcPos = new Vector2(0, 0);
                            type = Tile.TileType.Bush;
                            break;
                        case "g":
                            tileSrcPos = new Vector2(1, 0);
                            type = Tile.TileType.Grass;
                            break;
                        case "s":
                            tileSrcPos = new Vector2(2, 0);
                            type = Tile.TileType.Sand;
                            break;
                        case "w":
                            tileSrcPos = new Vector2(0, 1);
                            type = Tile.TileType.Water;
                            break;
                        case "d":
                            tileSrcPos = new Vector2(1, 1);
                            type = Tile.TileType.Dirt;
                            break;
                        case "r":
                            tileSrcPos = new Vector2(2, 1);
                            type = Tile.TileType.Rock;
                            break;
                        default:
                            //if null or anything else then just make grass
                            tileSrcPos = new Vector2(1, 0);
                            type = Tile.TileType.Grass;
                            break;
                    }

                    tileWorldPos = new Vector2(tileSizeX * j, tileSizeY * i);

                    gameTiles[i, j] = new Tile(Game, _spriteBatch, tex, tileWorldPos, tileSizeX, tileSizeY, tileSrcPos, type);

                }
            }

        }

        /// <summary>
        /// This checks for additional Tile info to see if anything else is spawning on it.
        /// Not just for items, but used for anything else like characters, trees, traps, etc
        /// </summary>
        /// <param name="itemStrings"></param>
        void GenerateWorldItems(List<string> itemStrings)
        {

            //if there are no items in the data then get out of the method
            if (itemStrings == null)
                return;


            for (int i = 0; i < itemStrings.Count; i++)
            {

                string[] itemStringData = itemStrings[i].Split(',');

                int xCoord = int.Parse(itemStringData[1]);
                int yCoord = int.Parse(itemStringData[2]);


                Vector2 itemPos = new Vector2(tileSizeX * xCoord, tileSizeY * yCoord);

                string itemString = itemStringData[0].ToLower().Trim();

                switch (itemString)
                {
                    case "i":
                        Pickup healthPickup = new HealthPickup(g, _activeScene, _spriteBatch, itemPos);
                        itemSprites.Add(healthPickup);
                        break;
                    case "f":
                        Bonfire newFire = new Bonfire(g, _spriteBatch, itemPos);
                        itemSprites.Add(newFire);
                        break;

                }

                /*****************************************************/
                /*** Rock spawning ******************************/
                string boulderPat = "^rock[0-9]";
                Regex boulderReg = new Regex(boulderPat);

                if (boulderReg.IsMatch(itemString))
                {

                    int rockNum = 0;
                    if (int.TryParse(itemString[4].ToString(), out rockNum))
                    {

                        Sprite boulder = new Boulder(g, _spriteBatch, itemPos, rockNum);
                        itemSprites.Add(boulder);
                        
                    }
                    else
                    {
                        Sprite boulder = new Boulder(g, _spriteBatch, itemPos);
                        itemSprites.Add(boulder);
                    }

                }

                /*****************************************************/
                /*** Weapon spawning ******************************/

                string weaponPat = "^wpn[0-9]";
                Regex weaponReg = new Regex(weaponPat);

                if (weaponReg.IsMatch(itemString))
                {

                    int weaponNum = 0;
                    if (int.TryParse(itemString[3].ToString(), out weaponNum))
                    {
                        Pickup weaponPickup = new WeaponPickup(g, _activeScene, _spriteBatch, itemPos, (WeaponType)weaponNum);
                        itemSprites.Add(weaponPickup);
                    }

                }

                /*****************************************************/
                /*** Character spawning ******************************/

                string charPat = "^char[0-9][0-9]";
                Regex charReg = new Regex(charPat);

                if (charReg.IsMatch(itemString))
                {

                    int teamNum = int.Parse(itemString[4].ToString());
                    int charNum = int.Parse(itemString[5].ToString());

                    Character soldier;

                    //charNum indicates weapon for now
                    if(charNum >= 0 && charNum <= 3)
                    {
                        soldier = new Soldier(g, _spriteBatch, itemPos, (TeamType)teamNum, charNum);
                        itemSprites.Add(soldier);

                    }
                    else
                    {
                        soldier = new Soldier(g, _spriteBatch, itemPos, (TeamType)teamNum);
                        itemSprites.Add(soldier);
                    }

                    if((TeamType)teamNum != TeamType.Green && (TeamType)teamNum != TeamType.Neutral)
                    {
                        if(level != null)
                            level.EnemeyCount++;
                    }

                }
                
            }

            level.SetupLevelObjectives();

        }

        /// <summary>
        /// Add world items
        /// </summary>
        public void AddWorldItems()
        {

            Vector2 itemPos = new Vector2(250, 100);

            Pickup newPickup = new HealthPickup(g, _activeScene, _spriteBatch, itemPos);

            itemSprites.Add(newPickup);

        }

        /// <summary>
        /// Adds an item
        /// </summary>
        public void SpawnWorldSprite(Sprite sprite)
        {
            _activeScene.components.Add(sprite);
            itemSprites.Add(sprite);

        }

        /// <summary>
        /// Add projectiles
        /// </summary>
        /// <param name="projectile"></param>
        public void AddProjectileToWorld(Projectile projectile)
        {
            _activeScene.components.Add(projectile);
            projectileSprites.Add(projectile);
        }


        public int GetNumberOfEnemiesLeft()
        {

            int enemies = 0;

            for(int i = 0; i < itemSprites.Count; i++)
            {

                Character character = itemSprites[i] as Character;

                if(character != null)
                {
                    //if we aren't neutral then check for potential team hit
                    if (character.Team != TeamType.Neutral && character.Team != TeamType.Green)
                    {
                        enemies++;
                    }
                }

            }

            return enemies; 
        }

    }
}
