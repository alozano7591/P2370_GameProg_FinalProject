/* Alfredo Lozano
 * 5397591
 * alozano7591@conestogac.on.ca
 * Final Project: The Charge:
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject_01
{

    
    /// <summary>
    /// Used for shared info
    /// </summary>
    public class Shared
    {
        public static Vector2 midPoint { get; set; }

        public static Vector2 stage;


        public static void SetMidpoint()
        {
            midPoint = new Vector2( (int)Shared.stage.X / 2, (int)Shared.stage.Y / 2);
        }
    }
}
