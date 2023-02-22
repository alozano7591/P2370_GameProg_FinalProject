/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject_01.Managers
{
    public class LevelManager
    {

        public List<string> LevelFilePaths= new List<string>();

        //make a level Dictionary. Stores the level number and its associated path
        public Dictionary<int, string> LevelDictionary = new Dictionary<int, string>();

        /// <summary>
        /// Load our levels
        /// </summary>
        public LevelManager() 
        {

            
        }


        public void SetupLevelDictionary()
        {
            LevelDictionary.Add(1, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest3.txt"));
            LevelDictionary.Add(2, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest3.txt"));
            LevelDictionary.Add(3, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest3.txt"));
        }

        /// <summary>
        /// Returns the level path based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetLevelPathById(int id)
        {
            return LevelFilePaths[id];
        }

    }
}
