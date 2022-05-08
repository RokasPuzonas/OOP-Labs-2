using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Class for a single hero
    /// </summary>
    public class Hero : Actor, IComparable<Hero>, IEquatable<Hero>
    {
        /// <summary>
        /// Power points of hero
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// Agility points of hero
        /// </summary>
        public int Agility { get; set; }
        /// <summary>
        /// Intellect points of hero
        /// </summary>
        public int Intellect { get; set; }
        /// <summary>
        /// Special points of hero
        /// </summary>
        public int Special { get; set; }

        public Hero(string race, string startingTown, string name, string @class, int health, int mana, int attack, int defense, int power, int agility, int intellect, int special) : base(race, startingTown, name, @class, health, mana, attack, defense)
        {
            Power = power;
            Agility = agility;
            Intellect = intellect;
            Special = special;
        }

        /// <summary>
        /// Compare hero to hero by intellect
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Hero other)
        {
            return Intellect.CompareTo(other.Intellect);
        }

        /// <summary>
        /// Check if 2 heros have the same intellect
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Hero other)
        {
            return Intellect.Equals(other.Intellect);
        }

        /// <summary>
        /// Serialize a hero to a CSV line
        /// </summary>
        /// <returns></returns>
        public override string ToCSVLine()
        {
            return string.Join(";", Race, StartingTown, Name, Class, Health, Mana, Attack, Defense, Power, Agility, Intellect, Special);
        }
    }
}