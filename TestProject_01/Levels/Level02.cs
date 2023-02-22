using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestProject_01.Managers;

namespace TestProject_01.Levels
{
    public class Level02 : Level
    {

        public int EnemyKillGoal { get; set; }

        string LevelFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest2.txt");

        public Level02()
        {

            DataFilePath = LevelFilePath;

            SetupLevelObjectives();

        }


        public override void SetupLevelObjectives()
        {

            EnemyKillGoal = EnemeyCount;

        }


        public override bool CheckIfObjectiveMet()
        {

            if (TileManager.Instance.GetNumberOfEnemiesLeft() <= 0)
            {
                return true;
            }

            return false;
        }

    }
}
