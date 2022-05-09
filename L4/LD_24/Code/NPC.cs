using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Class for storing a single NPC
    /// </summary>
    public class NPC : Actor
    {
        /// <summary>
        /// blah blah blah blah
        /// </summary>
        public string Guild { get; set; }

        public NPC(string race, string startingTown, string name, string @class, int health, int mana, int attack, int defense, string guild) : base(race, startingTown, name, @class, health, mana, attack, defense)
        {
            Guild = guild;
        }

        /// <summary>
        /// blah blah?
        /// </summary>
        /// <returns></returns>
        public override string ToCSVLine()
        {
            return string.Join(";", Race, StartingTown, Name, Class, Health, Mana, Attack, Defense, Guild);
        }
    }
}