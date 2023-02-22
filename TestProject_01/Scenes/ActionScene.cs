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
using System.Reflection.Metadata;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using TestProject_01.UI;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using TestProject_01.Managers;
using TestProject_01.Levels;

namespace TestProject_01.Scenes
{
    /// <summary>
    /// the scene of our actual game
    /// </summary>
    public class ActionScene : GameScene
    {

        private SpriteBatch _spriteBatch;
        private Game1 g;

        //player
        PlayerTestie player;
        Texture2D playerTex;

        int xSpriteCount = 4;
        int ySpriteCount = 4;
        Vector2 playerMapPos = Vector2.Zero;
        Vector2 playerSpawnPos = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
        Vector2 playerSpeed = new Vector2(4, 0);

        //tile builder
        TileManager tileManager;
        Texture2D tileTex;
        
        Bonfire newFire;
        Pickup pickup;
        public PlayerUI playerUI;

        public int ActiveLevelNum { get; set; } = 1;
        public Level ActiveLevel { get; set; }

        //music stuff
        SoundEffect sceneSong;

        public PauseMenuComponent pauseMenuComponent;
        bool menuOneShot = false;

        //public BasicTextInput textInput;


        /// <summary>
        /// constructor for action scene
        /// </summary>
        /// <param name="game">the game</param>
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            _spriteBatch = g._spriteBatch;

            playerTex = SContentManager.instance.texHeroSoldier;

            tileTex = SContentManager.instance.texTileSpriteSheet01;

            sceneSong = SContentManager.instance.gameSong1;

            pauseMenuComponent = new PauseMenuComponent(g, _spriteBatch, this);
            //textInput = new BasicTextInput(g, _spriteBatch);

            RestartScene();

        }

        public void TogglePauseMenu(bool activate)
        {
            pauseMenuComponent.Enabled = activate;
            pauseMenuComponent.Visible = activate;
        }


        /// <summary>
        /// Generate the level assets (calling tile manager generates everything)
        /// </summary>
        public void GenerateLevel()
        {

            switch (ActiveLevelNum)
            {
                case 1:
                    ActiveLevel = new Level01();
                    break;
                case 2:
                    ActiveLevel = new Level02();
                    break;
                case 3:
                    ActiveLevel = new Level03();
                    break;
                default:
                    break;
            }
            tileManager = new TileManager(g, this, _spriteBatch, tileTex, ActiveLevel);

        }

        /// <summary>
        /// Spawn our hero
        /// </summary>
        public void SpawnPlayer()
        {

            int wType = 0;
            if(FileManager.playerSpawnData != null)
            {
                if (FileManager.playerSpawnData.Trim().Length > 0)
                {


                    string[] playerSpawnInfo = FileManager.playerSpawnData.Split(',');

                    //hero
                    //check weapon type (first index)
                    if (playerSpawnInfo[0].Length >= 5)
                    {

                        if (int.TryParse(playerSpawnInfo[0][4].ToString(), out wType))
                        {
                            //make sure that if we get a number it isnt beyond our weapon range
                            if (wType > 3)
                            {
                                wType = 0;
                            }
                        }
                    }

                    //check spawn tile 
                    float tileX = float.Parse(playerSpawnInfo[1]);
                    float tileY = float.Parse(playerSpawnInfo[2]);

                    playerMapPos = new Vector2(tileX * tileManager.tileSizeX, tileY * tileManager.tileSizeY);
                    tileManager.SetWorldTopPlayerPosition(playerMapPos);
                }

                player = new PlayerTestie(g, _spriteBatch, playerTex, playerSpawnPos, wType);
            }
            else
            {
                player = new PlayerTestie(g, _spriteBatch, playerTex, playerSpawnPos, wType);
            }
            
            player.ActiveScene = this;
            
        }

        /// <summary>
        /// generate ui
        /// </summary>
        public override void GenerateUI()
        {

            playerUI = new PlayerUI(g, _spriteBatch, player);

        }

        /// <summary>
        /// Add components
        /// </summary>
        public void AddComponents()
        {
            //Add to list of components to be rendered. Order is extremely important. The last will be rendered on top
            this.components.Add(tileManager);
            this.components.Add(player);

            //adds the items that go in front of the tiles and player
            foreach(Sprite sp in tileManager.itemSprites)
            {
                this.components.Add(sp);
            }

            this.components.Add(playerUI);

            components.Add(pauseMenuComponent);
            TogglePauseMenu(false);

            //components.Add(textInput);

            //g.gamePaused = false;
        }


        /// <summary>
        /// clear scene
        /// </summary>
        public void ClearScene()
        {

            components = new List<GameComponent>();

        }

        /// <summary>
        /// restart scene
        /// </summary>
        public override void RestartScene()
        {
            components = new List<GameComponent>();
            restartingScene = true;

            
            GenerateLevel();
            SpawnPlayer();
            GenerateUI();

            AddComponents();

            //ContentManager.instance.PlayMusic(sceneSong, MusicVolume);
        }

        public void ProcessPlayerDeath()
        {

            //ScoreManager.CheckHighScore();

        }

        public void CheckForHighScore()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if(this.Enabled && PlayerUI.Instance != null)
            {
                PlayerUI.Instance.UpdateScore(ScoreManager.currentScore);
            }
            
            if(ActiveLevel.CheckIfObjectiveMet())
            {
                ActiveLevelNum++;
                RestartScene();
            }

            base.Update(gameTime);
        }
    }
}
