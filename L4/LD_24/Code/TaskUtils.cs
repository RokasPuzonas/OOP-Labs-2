using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Utility class for storing unrelated methods
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Find the actors which have the most health by class
        /// </summary>
        /// <param name="actors"></param>
        /// <returns></returns>
        public static Dictionary<string, int> FindMostHealthByClass(List<Actor> actors)
        {
            Dictionary<string, int> mostHealth = new Dictionary<string, int>();
            foreach (var actor in actors)
            {
                if (mostHealth.ContainsKey(actor.Class))
                {
                    mostHealth[actor.Class] = Math.Max(mostHealth[actor.Class], actor.Health);
                } else
                {
                    mostHealth.Add(actor.Class, actor.Health);
                }
            }
            return mostHealth;
        }

        /// <summary>
        /// Find all unique classes from a list of actors
        /// </summary>
        /// <param name="actors"></param>
        /// <returns></returns>
        public static List<string> FindAllClasses(List<Actor> actors)
        {
            List<string> result = new List<string>();
            foreach (var actor in actors)
            {
                if (!result.Contains(actor.Class))
                {
                    result.Add(actor.Class);
                }
            }
            return result;
        }

        /// <summary>
        /// Find all unique races from a list of actors
        /// </summary>
        /// <param name="actors"></param>
        /// <returns></returns>
        public static List<string> FindAllRaces(List<Actor> actors)
        {
            List<string> races = new List<string>();
            foreach (var actor in actors)
            {
                if (!races.Contains(actor.Race))
                {
                    races.Add(actor.Race);
                }
            }
            return races;
        }

        /// <summary>
        /// Finds which races are missing an NPC or Hero
        /// </summary>
        /// <param name="actors"></param>
        /// <returns>A tuple where Item1 is missing Heros, and Item2 is missing NPCs</returns>
        public static Tuple<List<string>, List<string>> FindMissingActors(List<Actor> actors)
        {
            var races = FindAllRaces(actors);
            var missingHeroes = races;
            var missingNPCs = new List<string>(races);
            foreach (var actor in actors)
            {
                if (actor is Hero)
                {
                    missingHeroes.Remove(actor.Race);
                } else if (actor is NPC)
                {
                    missingNPCs.Remove(actor.Race);
                }
            }

            return Tuple.Create(missingHeroes, missingNPCs);
        }

        /// <summary>
        /// Find the actors which have the most health in their respective classes
        /// </summary>
        /// <param name="actors"></param>
        /// <returns></returns>
        public static List<Actor> FilterMostHealthByClass(List<Actor> actors)
        {
            List<Actor> filtered = new List<Actor>();
            var mostHealths = FindMostHealthByClass(actors);
            foreach (var actor in actors)
            {
                if (mostHealths[actor.Class] == actor.Health)
                {
                    filtered.Add(actor);
                }
            }
            return filtered;
        }
    }
}