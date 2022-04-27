using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public abstract class Actor
    {
        public string Race { get; set; }
        public string StartingTown { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Actor(string race, string startingTown, string name, string @class, int health, int mana, int attack, int defense)
        {
            Race = race;
            StartingTown = startingTown;
            Name = name;
            Class = @class;
            Health = health;
            Mana = mana;
            Attack = attack;
            Defense = defense;
        }

        public abstract string ToCSVLine();
    }
}