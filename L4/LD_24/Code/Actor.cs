using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Abstract actor class
    /// </summary>
    public abstract class Actor
    {
        /// <summary>
        /// Race of actor
        /// </summary>
        public string Race { get; set; }
        /// <summary>
        /// Starting town of actor
        /// </summary>
        public string StartingTown { get; set; }
        /// <summary>
        /// Name of actor
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Class of actor
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// Health points of actor
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Mana points of actor
        /// </summary>
        public int Mana { get; set; }
        /// <summary>
        /// Attack points of actor
        /// </summary>
        public int Attack { get; set; }
        /// <summary>
        /// Defense points of actor
        /// </summary>
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

        /// <summary>
        /// Serialize an actor into a valid CSV line
        /// </summary>
        /// <returns>A string representing the whole actor</returns>
        public abstract string ToCSVLine();
    }
}