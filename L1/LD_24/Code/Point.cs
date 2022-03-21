using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// A simple point class for storing x and y together.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// The x component
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The y component
        /// </summary>
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Check if 2 points are the same
        /// </summary>
        /// <param name="obj">Other point</param>
        /// <returns>Are the x and y the same</returns>
        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return String.Format("Point({0}, {1})", X, Y);
        }
    }
}
