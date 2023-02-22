/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace TestProject_01.Managers
{


    /// <summary>
    /// The score manager
    /// </summary>
    public static class ScoreManager
    {

        private static string _filename = "scores.xml";

        //score will be in the format of "name, score"
        public static List<Score> highScores= new List<Score>();

        public static int currentScore = 0;

        public static int highscoreLimit = 5;

        //files saving stuff
        public const char rowDelim = '\n';          //every line break is a new row 
        public const char colDelim = ',';           //each tile is seperated by a comma
        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        private static string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ScoreFiles\scores.txt");
    


        /// <summary>
        /// Use this whenever player starts game or dies
        /// </summary>
        public static void ResetScore()
        {
            currentScore = 0;
        }

        /// <summary>
        /// See if we're on the highscore list
        /// </summary>
        /// <param name="score"></param>
        public static bool CheckHighScore(int score)
        {

            if(highScores != null)
            {

                if(score != null)
                {
                    if(score > 0)
                    {
                        if(highScores.Count < 10)
                        {
                            return true;
                        }
                        else if (highScores[9].score < score)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                return true;
                
            }
            return true;
            
        }

        /// <summary>
        /// See if we're on the highscore list
        /// </summary>
        public static bool CheckHighScore()
        {

            if (highScores != null)
            {

                if (currentScore != null)
                {
                    if (currentScore > 0)
                    {
                        if (highScores.Count < 10)
                        {
                            return true;
                        }
                        else if (highScores[9].score < currentScore)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                return true;

            }
            return true;

        }



        /// <summary>
        /// Load scores, returns true if files exists, returns false otherwise
        /// </summary>
        /// <returns></returns>
        public static bool LoadScores()
        {

            if (File.Exists(fullPath))
            {
                using (StreamReader file = new StreamReader(fullPath))
                {
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {

                        string[] strScore = ln.Split(colDelim);
                        string name = strScore[0];

                        int scoreNum = 0;
                        if (!int.TryParse(strScore[1], out scoreNum))
                        {
                            continue; //data invalid, go to next line
                        }

                        Score score = new Score(name, scoreNum);

                        highScores.Add(score);

                    }

                    file.Close();

                }

                highScores = highScores.OrderByDescending(o => o.score).ToList();
                return true;

            }



            return false;
            
        }

        /// <summary>
        /// create and save new score
        /// </summary>
        /// <param name="name">player name</param>
        public static void CreateNewScore(string name)
        {

            Score score = new Score(name, currentScore);

            if(highScores != null)
            {
                highScores.Add(score);
            }
            else
            {
                highScores = new List<Score>();
                highScores.Add(score);
                
            }

            SaveScores();

        }

        /// <summary>
        /// Save our new scores. Overwrites any previous scores
        /// </summary>
        /// <param name="scores"></param>
        public static void SaveScores()
        {

            try
            {

                if (highScores == null)
                    return;

                if (highScores.Count == 0)
                    return;

                //order our list one more time for good measure
                highScores.OrderByDescending(o => o.score).ToList();

                TrimScoreList(highScores);

                using (StreamWriter w = new StreamWriter(fullPath, false))
                {

                    foreach (Score highScore in highScores)
                    {
                        string playerName = highScore.name;
                        string strScore = highScore.score.ToString();

                        w.WriteLine(playerName + "," + strScore);
                    }

                    w.Close();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            ClearCurrentScore();
            
        }



        /// <summary>
        /// we keep a limit of 5 scores
        /// </summary>
        /// <param name="scores"></param>
        public static void TrimScoreList(List<Score> scores)
        {

            if(scores != null)
            {
                if(scores.Count > highscoreLimit)
                {
                    //remove other scores until desired limit
                    while(scores.Count > highscoreLimit)
                    {
                        scores.RemoveAt(scores.Count - 1);
                    }
                }
            }

        }

        /// <summary>
        /// add to our score
        /// </summary>
        /// <param name="amt">the points</param>
        public static void AddToCurrentScore(int amt)
        {
            currentScore += amt;
            
        }

        /// <summary>
        /// clears current score
        /// </summary>
        public static void ClearCurrentScore()
        {
            currentScore = 0;   
        }
    
    }
}
