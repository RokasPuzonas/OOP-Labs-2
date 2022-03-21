using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Functions related to working with files
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Read a map from a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <returns>A map loaded from the file</returns>
        /// <exception cref="Exception">If there was an invalid tile</exception>
        public static Map ReadMap(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            string[] height_width = lines[0].Split(' ');
            int height = int.Parse(height_width[0]);
            int width = int.Parse(height_width[1]);

            Map map = new Map(width, height);
            for (int i = 1; i < height+1; i++)
            {
                int x = 0;
                foreach (char c in lines[i])
                {
                    MapTile tile;
                    if (c == '.') {
                        tile = MapTile.Empty;
                    } else if (c == 'X')  {
                        tile = MapTile.Wall;
                    } else if (c == 'P') {
                        tile = MapTile.Pizzeria;
                    } else if (c == 'D') {
                        tile = MapTile.Friend;
                    } else if (c == 'S') {
                        tile = MapTile.MeetingSpot;
                    } else {
                        throw new Exception($"Invalid tile '{c}'");
                    }
                    map.Set(x, i - 1, tile);
                    x++;
                }
            }
            return map;
        }

        /// <summary>
        /// Write a map to a file
        /// </summary>
        /// <param name="writer">Target file writer</param>
        /// <param name="map">Target map</param>
        public static void WriteMap(StreamWriter writer, Map map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    switch (map.Get(x, y))
                    {
                        case MapTile.Empty:
                            writer.Write('.');
                            break;
                        case MapTile.Pizzeria:
                            writer.Write('P');
                            break;
                        case MapTile.Friend:
                            writer.Write('D');
                            break;
                        case MapTile.MeetingSpot:
                            writer.Write('S');
                            break;
                        case MapTile.Wall:
                            writer.Write('X');
                            break;
                        default:
                            writer.Write('?');
                            break;
                    }
                }
                writer.Write('\n');
            }
        }

        /// <summary>
        /// Write out best pizzeria result to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="result">Target result</param>
        public static void WriteBestPizzeriaResult(StreamWriter writer, BestPizzeriaResult result)
        {
            if (result == null)
            {
                writer.WriteLine("Neįmanoma");
            }
            else
            {
                writer.WriteLine("Susitikimo vieta {0} {1}", result.MeetingSpot.X + 1, result.MeetingSpot.Y + 1);
                writer.WriteLine("Picerija {0} {1}", result.Pizzeria.X + 1, result.Pizzeria.Y + 1);
                writer.WriteLine("Nueita {0}", result.Cost);
            }
        }

        /// <summary>
        /// Write out friend positions to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="friends">Target friends list</param>
        public static void WriteFriendPositions(StreamWriter writer, List<Point> friends)
        {
            foreach (var friend in friends)
            {
                writer.WriteLine("{0} {1}", friend.X + 1, friend.Y + 1);
            }
        }
    }
}
