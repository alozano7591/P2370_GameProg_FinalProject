/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * To fix formatting ctrl + k release + d
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
using Microsoft.Xna.Framework.Audio;
using TestProject_01.Weapons;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;

namespace TestProject_01.Managers
{
    /// <summary>
    /// Class is responsible to providing easy access to game resoucres.
    /// For textures and sound files this is idealy where code will look
    /// This also makes the game more efficient since it isn't loading textures and sounds
    /// every time a new asset is introduced (which is what i did before)
    /// </summary>
    public class SContentManager
    {
        //instance and stuff
        public static SContentManager instance;
        private SpriteBatch _spriteBatch;
        private Game1 g;

        //texture assets
        public Texture2D texWhitePixel;

        //main menu
        public Texture2D texTitleBackground;
        public Texture2D texTitleText;

        //Characters
        public Texture2D texHeroSoldier;
        public Texture2D texGraySoldier;
        public Texture2D texTanSoldier;
        public Texture2D texCivilian01;

        //Environment 
        public Texture2D texTileSpriteSheet01;
        public Texture2D texFire;
        public Texture2D texRocks;


        //Weapons sprite sheets
        public Texture2D texRifleSheet;
        public Texture2D texMGSheet;
        public Texture2D texShotgunSheet;
        public Texture2D texFlamethrowerSheet;


        //Projectiles
        public Texture2D texFireball;


        //items
        public Texture2D texMedPack;
        public Texture2D texWeaponPickup;

        //sounds

        //weapons
        public SoundEffect sndRifleShot;
        public SoundEffect sndFlameThrowerLoop1;
        public SoundEffect sndFlameThrowerLoop2;
        public SoundEffect sndMachineGun;
        public SoundEffect sndShotgun;

        //characters
        public SoundEffect sndManGrunt01;
        public SoundEffect sndManGrunt02;
        public SoundEffect sndOof;
        public SoundEffect sndBurnScream1;
        public SoundEffect sndBurnScream2;

        //music 
        //public Song titleSong;
        //public Song gameSong1;
        //public Song gameSong2;

        public SoundEffect titleSong;
        public SoundEffect gameSong1;
        public SoundEffect gameSong2;

        private static SoundEffectInstance sndMusicAudio = null;
        private static SoundEffectInstance sndCueAudio = null;

        public float MusicVolume { get; set; } = .7f;


        //fonts
        public SpriteFont fontRegular;
        public SpriteFont fontHilight;
        public SpriteFont fontTitle;

        /// <summary>
        /// Bare minimum constructor for functioning content manager
        /// </summary>
        /// <param name="game"></param>
        public SContentManager(Game game)
        {
            g = (Game1)game;
            instance = this;
        }

        /// <summary>
        /// Create our content manager instance. There will only be one ever.
        /// </summary>
        /// <param name="game">game1</param>
        /// <param name="spriteBatch">the sprite batch</param>
        public SContentManager(Game game, SpriteBatch spriteBatch)
        {
            g = (Game1)game;
            _spriteBatch = spriteBatch;
            instance = this;
        }

        /// <summary>
        /// load function for textures
        /// </summary>
        public void LoadImages()
        {
            //texture assets
            texWhitePixel = g.Content.Load<Texture2D>("images/1pxWhite");

            //main menu
            texTitleBackground = g.Content.Load<Texture2D>("images/MainMenu/TitlePageInsertText");
            texTitleText = g.Content.Load<Texture2D>("images/MainMenu/JustTitle");

            //Characters
            texHeroSoldier = g.Content.Load<Texture2D>("images/PlayerSoldier");
            texGraySoldier = g.Content.Load<Texture2D>("images/characters/SoldierGray");
            texTanSoldier = g.Content.Load<Texture2D>("images/characters/SoldierTan");
            texCivilian01 = g.Content.Load<Texture2D>("images/spritesheet_randomguy");

            //Environment 
            texTileSpriteSheet01 = g.Content.Load<Texture2D>("images/environment/kostarelas-16-bit-tileset");
            texFire = g.Content.Load<Texture2D>("images/basicFireSpritesheet");
            texRocks = g.Content.Load<Texture2D>("images/environment/rocks");

            //Weapons
            texRifleSheet = g.Content.Load<Texture2D>("images/weapons/RifleSpritesheet");
            texMGSheet = g.Content.Load<Texture2D>("images/weapons/MgSpritesheet");
            texShotgunSheet = g.Content.Load<Texture2D>("images/weapons/ShotgunSpritesheet");
            texFlamethrowerSheet = g.Content.Load<Texture2D>("images/weapons/FlamethrowerWeaponSheet");

            //Projectiles
            texFireball = g.Content.Load<Texture2D>("images/fireball-1");

            //items
            texMedPack = g.Content.Load<Texture2D>("images/items/Medipack");
            texWeaponPickup = g.Content.Load<Texture2D>("images/weapons/GunsSheet");

            //main menu


            //fonts

            fontRegular = g.Content.Load<SpriteFont>("fonts/regularFont");
            fontHilight = g.Content.Load<SpriteFont>("fonts/hilightedFont");
            fontTitle = g.Content.Load<SpriteFont>("fonts/headerText");
        }

        /// <summary>
        /// Load function for sounds
        /// </summary>
        public void LoadSounds()
        {
            //guns
            sndRifleShot = g.Content.Load<SoundEffect>("sounds/weapons/rifleShot_01");
            sndMachineGun = g.Content.Load<SoundEffect>("sounds/weapons/rifleShot_01");
            sndShotgun = g.Content.Load<SoundEffect>("sounds/weapons/shotgunshot");
            sndFlameThrowerLoop1 = g.Content.Load<SoundEffect>("sounds/weapons/FlamethrowerLoop01");
            sndFlameThrowerLoop2 = g.Content.Load<SoundEffect>("sounds/weapons/FlamethrowerLoop02");

            //characters
            sndManGrunt01 = g.Content.Load<SoundEffect>("sounds/characters/ManGrunt_01");
            sndManGrunt02 = g.Content.Load<SoundEffect>("sounds/characters/deathGrunt");
            sndOof = g.Content.Load<SoundEffect>("sounds/characters/oof01");
            sndBurnScream1 = g.Content.Load<SoundEffect>("sounds/characters/BurnScream_01");
            sndBurnScream2 = g.Content.Load<SoundEffect>("sounds/characters/BurnScream_02");

            //music 
            //titleSong = g.Content.Load<Song>("sounds/music/PlatSongTitle_01");
            //gameSong1 = g.Content.Load<Song>("sounds/music/PlatCombatMusic");
            //gameSong = g.Content.Load<Song>("sounds/music/PlatSongBattle02");

            titleSong = g.Content.Load<SoundEffect>("sounds/music/PlatSongTitle_01");
            gameSong1 = g.Content.Load<SoundEffect>("sounds/music/PlatCombatMusic");
            gameSong2 = g.Content.Load<SoundEffect>("sounds/music/PlatSongBattle02");
        }

        /// <summary>
        /// Start playing the selected song. Can choose whether to repeat or not
        /// doesn't work... media player is broken and plays random songs because monogame sucks
        /// </summary>
        /// <param name="song">the song</param>
        /// <param name="volume">the volume</param>
        /// <param name="repeat">if it should repeat</param>
        public void PlaySong(Song song, float volume, bool repeat)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Volume = volume;
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        /// <summary>
        /// tried playing music with media maker and it ruined my life. using sound effects.
        /// call this to specifcally reload a new song. sounds are buggy and seems to work everytime
        /// </summary>
        /// <param name="songString">location string</param>
        /// <param name="volume">volume</param>
        public void PlayMusic(string songString, float volume)
        {

            StopMusic();

            SoundEffect song = g.Content.Load<SoundEffect>(songString);

            if (song != null)
            {
                volume = MathHelper.Clamp(volume, 0f, 1f);
                StartMusic(song, volume);
            }

        }

        /// <summary>
        /// play song with given soundeffect
        /// </summary>
        /// <param name="song">song name</param>
        /// <param name="volume">volume</param>
        public void PlayMusic(SoundEffect song, float volume)
        {

            StopMusic();

            if (song != null)
            {
                volume = MathHelper.Clamp(volume, 0f, 1f);
                StartMusic(song, volume);
            }

        }

        /// <summary>
        /// Play sound effect as music because mediaplayer is fucking broken
        /// </summary>
        /// <param name="song"></param>
        /// <param name="volume"></param>
        public void StartMusic(SoundEffect song, float volume)
        {
            sndMusicAudio = song.CreateInstance();
            sndMusicAudio.IsLooped = true;
            sndMusicAudio.Volume = volume;
            sndMusicAudio.Play();
        }

        /// <summary>
        /// try to stop playing music. fuck the media player
        /// </summary>
        static public void StopMusic()
        {
            if (null != sndMusicAudio)
            {
                sndMusicAudio.Pause();
                sndMusicAudio.Stop();
                sndMusicAudio.Volume = 0f;

                sndMusicAudio.Dispose();
            }
            sndMusicAudio = null;
        }

        public void KeepSpecificSong(SoundEffect song, float vol)
        {
            if (null != sndMusicAudio)
            {
                if(sndMusicAudio.State.Equals(song))
                {
                    return;
                }
                else
                {
                    StopMusic();
                    PlayMusic(song, vol);
                }
                
            }
        }

    }
}
