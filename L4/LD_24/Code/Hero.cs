using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Hero : Actor, IComparable<Hero>, IEquatable<Hero>
    {
        public int Power { get; set; }
        public int Agility { get; set; }
        public int Intellect { get; set; }
        public int Special { get; set; }

        public Hero(string race, string startingTown, string name, string @class, int health, int mana, int attack, int defense, int power, int agility, int intellect, int special) : base(race, startingTown, name, @class, health, mana, attack, defense)
        {
            Power = power;
            Agility = agility;
            Intellect = intellect;
            Special = special;
        }

        public int CompareTo(Hero other)
        {
            return Intellect.CompareTo(other.Intellect);
        }

        public bool Equals(Hero other)
        {
            return Intellect.Equals(other.Intellect);
        }

        public override string ToCSVLine()
        {
            return string.Join(";", Race, StartingTown, Name, Class, Health, Mana, Attack, Defense, Power, Agility, Intellect, Special);
        }
    }
}