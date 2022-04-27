using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public static class TaskUtils
    {
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

        public static List<Hero> FilterHeroesByIntellect(List<Actor> actors, int minIntellect)
        {
            List<Hero> filtered = new List<Hero>();
            foreach (var actor in actors)
            {
                if (actor is Hero && (actor as Hero).Intellect > minIntellect)
                {
                    filtered.Add(actor as Hero);
                }
            }
            return filtered;
        }

        public static List<NPC> FilterNPCsByAttack(List<Actor> actors, int maxAttack)
        {
            List<NPC> filtered = new List<NPC>();
            foreach (var actor in actors)
            {
                if (actor is NPC && actor.Attack < maxAttack)
                {
                    filtered.Add(actor as NPC);
                }
            }
            return filtered;
        }
    }
}