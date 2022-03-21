using System.Collections.Generic;

namespace LD_24.Code
{
    /// <summary>
    /// Class used to store a single map
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Tile map
        /// </summary>
        MapTile[,] data;
        /// <summary>
        /// Map width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Map height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Create an empty map
        /// </summary>
        /// <param name="width">Target width</param>
        /// <param name="height">Target height</param>
        public Map(int width, int height)
        {
            data = new MapTile[width, height];
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Change a single tile in map
        /// </summary>
        /// <param name="x">Target x</param>
        /// <param name="y">Target y</param>
        /// <param name="tile">Target tile</param>
        public void Set(int x, int y, MapTile tile)
        {
            data[x, y] = tile;
        }

        /// <summary>
        /// Retrieve a single tile from map
        /// </summary>
        /// <param name="x">Target x</param>
        /// <param name="y">Target y</param>
        /// <returns>Tile at target position</returns>
        public MapTile Get(int x, int y)
        {
            return data[x, y];
        }

        /// <summary>
        /// Check if a position is whithin the bounds of the map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Find all positions of a certain tile type
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public List<Point> FindAll(MapTile tile)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (data[i, j] == tile)
                    {
                        points.Add(new Point(i, j));
                    }
                }
            }
            return points;
        }

        public override string ToString()
        {
            return string.Format("Map{Width = {0}, Height = {1}}", Width, Height);
        }
    }
}
