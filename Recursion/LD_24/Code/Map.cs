using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Map
    {
        MapTile[,] data;
        public int Width { get; set; }
        public int Height { get; set; }

        public Map(int width, int height)
        {
            data = new MapTile[width, height];
            Width = width;
            Height = height;
        }

        public void Set(int x, int y, MapTile tile)
        {
            data[x, y] = tile;
        }

        public MapTile Get(int x, int y)
        {
            return data[x, y];
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

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
    }
}