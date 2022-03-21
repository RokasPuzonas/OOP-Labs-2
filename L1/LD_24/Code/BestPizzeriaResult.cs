using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Used for keeping the result of path finding
    /// </summary>
    public class BestPizzeriaResult
    {
        /// <summary>
        /// Pizzeria to which every friend will go
        /// </summary>
        public Point Pizzeria { get; private set; }
        /// <summary>
        /// Meeting spot for friends
        /// </summary>
        public Point MeetingSpot { get; private set; }
        /// <summary>
        /// The turn number of steps that will be taken between all friends
        /// </summary>
        public int Cost { get; private set; }

        public BestPizzeriaResult(Point pizzeria, Point meetingSpot, int cost)
        {
            Pizzeria = pizzeria;
            MeetingSpot = meetingSpot;
            Cost = cost;
        }

        public override string ToString()
        {
            return String.Format("BestPizzeriaResult{Pizzeria = {0}, MeetingSpot = {1}, Cost = {2}}", Pizzeria, MeetingSpot, Cost);
        }
    }
}
