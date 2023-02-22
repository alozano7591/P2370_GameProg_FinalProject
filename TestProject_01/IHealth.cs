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

namespace TestProject_01
{
    /// <summary>
    /// Health interface. Attach this to any class you want killable
    /// </summary>
    public interface IHealth
    {

        public void Damage(int damageAmt);

        public void Damage(int damageAmt, DamageType damageType);

        public void Heal(int healAmt);


    }
}
