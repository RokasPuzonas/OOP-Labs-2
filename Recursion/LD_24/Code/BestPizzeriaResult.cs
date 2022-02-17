using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class BestPizzeriaResult
    {
        public Point Pizzeria { get; private set; }
        public Point MeetingSpot { get; private set; }
        public int Cost { get; private set; }

        public BestPizzeriaResult(Point pizzeria, Point meetingSpot, int cost)
        {
            Pizzeria = pizzeria;
            MeetingSpot = meetingSpot;
            Cost = cost;
        }
    }
}