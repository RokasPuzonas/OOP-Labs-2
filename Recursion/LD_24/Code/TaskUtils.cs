using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    
    public static class TaskUtils
    {
        public static BestPizzeriaResult FindBestPizzeria(Map map)
        {
            int lowestCost = int.MaxValue;
            Point bestMeetingSpot = null;
            Point bestPizzeria = null;

            List<Point> friends = map.FindAll(MapTile.Friend);
            List<Point> pizzerias = map.FindAll(MapTile.Pizzeria);
            List<Point> meetingSpots = map.FindAll(MapTile.MeetingSpot);

            foreach (var meetingSpot in meetingSpots)
            {
                foreach (var pizzeria in pizzerias)
                {
                    int totalCost = 0;

                    int toPizzeriaCostPerFriend = FindBestPath(map, meetingSpot, pizzeria);
                    if (toPizzeriaCostPerFriend < 0) { break; }
                    totalCost += toPizzeriaCostPerFriend * friends.Count;

                    bool failed = false;
                    foreach (var friend in friends)
                    {
                        int toMeetingCost = FindBestPath(map, friend, meetingSpot);
                        if (toMeetingCost < 0) { failed = true; break; }
                        totalCost += toMeetingCost;

                        int toHomeCost = FindBestPath(map, pizzeria, friend);
                        if (toHomeCost < 0) { failed = true; break; }
                        totalCost += toHomeCost;
                    }
                    if (failed) { break; }

                    if (totalCost < lowestCost)
                    {
                        lowestCost = totalCost;
                        bestMeetingSpot = meetingSpot;
                        bestPizzeria = pizzeria;
                    }
                }
            }

            if (bestMeetingSpot != null)
            {
                return new BestPizzeriaResult(bestPizzeria, bestMeetingSpot, lowestCost);
            }
            else
            {
                return null;
            }
        }

        public static int FindBestPath(Map map, Point from, Point to)
        {
            return FindBestPath(map, from, to, new Stack<Point>());
        }

        private static int FindBestPath(Map map, Point from, Point to, Stack<Point> exploredPoints)
        {
            if (from.Equals(to)) { return 0; }

            int minCost = -1;
            exploredPoints.Push(from);
            foreach (var neighbour in GetNeighbours(map, from.X, from.Y))
            {
                if (!exploredPoints.Contains(neighbour))
                {
                    int cost = FindBestPath(map, neighbour, to, exploredPoints);
                    if (cost >= 0)
                    {
                        if (minCost < 0)
                        {
                            minCost = cost + 1;
                        } else
                        {
                            minCost = Math.Min(minCost, cost + 1);
                        }
                    }
                }
            }
            exploredPoints.Pop();
            return minCost;
        }

        private static IEnumerable<Point> GetNeighbours(Map map, int x, int y)
        {
            if (map.IsInBounds(x + 1, y) && map.Get(x + 1, y) != MapTile.Wall)
            {
                yield return new Point(x + 1, y);
            }
            if (map.IsInBounds(x, y + 1) && map.Get(x, y + 1) != MapTile.Wall)
            {
                yield return new Point(x, y + 1);
            }
            if (map.IsInBounds(x - 1, y) && map.Get(x - 1, y) != MapTile.Wall)
            {
                yield return new Point(x - 1, y);
            }
            if (map.IsInBounds(x, y - 1) && map.Get(x, y - 1) != MapTile.Wall)
            {
                yield return new Point(x, y - 1);
            }
        }
    }
}
