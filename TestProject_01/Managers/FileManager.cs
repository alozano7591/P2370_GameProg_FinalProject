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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject_01.Managers
{
    /// <summary>
    /// File manager class is used for loading and saving game files.
    /// Reads level files that allow it to know where to spawn world tiles, characters, and pickups
    /// </summary>
    public static class FileManager
    {

        public const char rowDelim = '\n';          //every line break is a new row of tiles
        public const char colDelim = ',';           //each tile is seperated by a comma (used because csv makes making mass tiles easier)

        public const char tileDataDelim = '|';      //each tile's specific data is delimited with '|', but not always present

        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        private static string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest3.txt");

        private static string levelData;
        public static string[,] levelTileData;         //this is used for the spawning of the tiles themselves
        public static List<string> levelItemsList;      //this is used for spawning anything other than tiles (aka, items, decorations, bad guys, players)

        private static string heroPat = "^hero";        //used for regex               
        public static string playerSpawnData;           //holds all data related to player spawn (ie, position, weapon, etc)


        public static string[,] LoadLevelData()
        {

            string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LevelFiles\leveltest3.txt");

            levelData = OpenLevelFile(fullPath);

            levelTileData = ConvertStringToLevelData(levelData);

            return levelTileData;
        }

        public static string[,] LoadLevelData(string fileName)
        {

            //fullPath = path + fileName;

            fullPath = fileName;

            levelData = OpenLevelFile(fullPath);

            levelTileData = ConvertStringToLevelData(levelData);

            return levelTileData;
        }

        public static string OpenLevelFile(string path)
        {

            if (File.Exists(path))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(path))
                {

                    //in order for file to be able to be readable, i needed to change the file properties in
                    //the solution explorer: Build Action to Content, and copy output to always
                    levelData = file.ReadToEnd();

                    file.Close();
                }

                return levelData;
            }
            else
            {
                return "";
            }

        }


        public static string[,] ConvertStringToLevelData(string stringData)
        {


            List<string> lines = stringData.Split(Environment.NewLine).ToList();
            //List<string> lines = stringData.Split('\n').ToList<string>();

            int lvlFileRows = lines.Count;
            int lvlFileCols = lines[0].Split(',').Length;


            //gameTiles = new Tile[lvlFileRows, lvlFileCols];
            levelTileData = new string[lvlFileRows, lvlFileCols];

            levelItemsList = new List<string>();

            for (int i = 0; i < levelTileData.GetLength(0); i++)
            {
                string[] lvlTileRows = lines[i].Split(',');
                for (int j = 0; j < levelTileData.GetLength(1); j++)
                {

                    //split to check for further info
                    string[] tileInfo = lvlTileRows[j].Split(tileDataDelim);

                    if (tileInfo.Length > 0)
                    {

                        //save first item as tile info 
                        levelTileData[i, j] = tileInfo[0];

                        //if we have more info, then save that to items list
                        if (tileInfo.Length > 1)
                        {

                            //first check if item is player
                            //string heroPat = "^hero";
                            Regex geroReg = new Regex(heroPat);

                            //if this iteration is a hero tile (tile where hero spawns)
                            if (geroReg.IsMatch(tileInfo[1]))
                            {

                                //save player data along with the tile address 
                                //we keep only one string for saving this data, so if there is 
                                //another 'hero' stamp by error, it will still only pass 1 to prevent a crash
                                playerSpawnData = $"{tileInfo[1]},{j},{i}";

                            }

                            //save item character, and then save row and col data for later use
                            levelItemsList.Add($"{tileInfo[1]},{j},{i}");
                        }
                    }

                    if (lvlTileRows[j] == null)
                    {
                        //if our spot is empty then skip
                        continue;
                    }

                }


            }

            return levelTileData;

        }
    }
}
