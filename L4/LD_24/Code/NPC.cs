using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class NPC : Actor, IComparable<NPC>, IEquatable<NPC>
    {
        public string Guild { get; set; }

        public NPC(string race, string startingTown, string name, string @class, int health, int mana, int attack, int defense, string guild) : base(race, startingTown, name, @class, health, mana, attack, defense)
        {
            Guild = guild;
        }

        public int CompareTo(NPC other)
        {
            return Attack.CompareTo(other.Attack);
        }

        public bool Equals(NPC other)
        {
            return Attack.Equals(other.Attack);
        }

        public override string ToCSVLine()
        {
            return string.Join(";", Race, StartingTown, Name, Class, Health, Mana, Attack, Defense, Guild);
        }
    }
}