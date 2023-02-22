using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_01.Levels
{
    public abstract class Level
    {

        public int EnemeyCount { get; set; }

        public int EnemyKillCount { get; set; } = 0;

        public bool DestinationReached { get; set; }

        public Vector2 DestinationPos { get; set; }
        public Vector2 DestinationRect { get; set; }

        public Vector2 PlayerSpawnPos { get; set; }

        public string DataFilePath;

        public Level()
        {

        }

        public Level(int enemeyCount)
        {
            EnemeyCount = enemeyCount;
        }

        /// <summary>
        /// set up objective logic here for each level
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckIfObjectiveMet();


        public abstract void SetupLevelObjectives();

    }
}
