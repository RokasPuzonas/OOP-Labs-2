using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using Bogus;
using System.Text;

namespace LD_24.Code
{
    /// <summary>
    /// Utility class for reading/writing to files
    /// </summary>
    public static class InOutUtils
    {
        private static readonly List<string> Races = new List<string> { "Human", "Orc", "Elf", "Dwarf", "Fairy", "Halfling" };
        private static readonly List<string> Classess = new List<string> { "Warrior", "Hunter", "Archer", "Mage", "Necromancer" };
        private static readonly Faker faker = new Faker();

        /// <summary>
        /// Read line by lines from a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <returns>Lines</returns>
        public static IEnumerable<string> ReadLines(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // This check is for ignoring empty lines
                    if (line.Length > 0)
                    {
                        yield return line;
                    }
                }
            }
        }

        /// <summary>
        /// Read actors from a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <returns>A list of actors</returns>
        /// <exception cref="Exception">Throws if a given line in a file is incorrect</exception>
        public static List<Actor> ReadActors(string filename)
        {
            var actors = new List<Actor>();
            var lines = ReadLines(filename);
            var race = lines.First().Trim();
            var startingTown = lines.Skip(1).First().Trim();
            foreach (var line in lines.Skip(2))
            {
                string[] parts = line.Split(';');
                if (parts.Length != 7 && parts.Length != 10)
                {
                    throw new Exception($"Invalid number of values given: '{line}'");
                }

                string name = parts[0].Trim();
                string @class = parts[1].Trim();
                int health = int.Parse(parts[2]);
                int mana = int.Parse(parts[3]);
                int attack = int.Parse(parts[4]);
                int defense = int.Parse(parts[5]);
                if (parts.Length == 7)
                {
                    string guild = parts[6].Trim();
                    actors.Add(new NPC(race, startingTown, name, @class, health, mana, attack, defense, guild));
                }
                else if (parts.Length == 10)
                {
                    int power = int.Parse(parts[6]);
                    int agility = int.Parse(parts[7]);
                    int intellect = int.Parse(parts[8]);
                    int special = int.Parse(parts[9]);
                    actors.Add(new Hero(race, startingTown, name, @class, health, mana, attack, defense, power, agility, intellect, special));
                }
            }
            return actors;
        }

        /// <summary>
        /// Generate file with actors
        /// </summary>
        /// <param name="filename">Target file</param>
        public static void GenerateFakeActors(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine(faker.PickRandom(Races));
                writer.WriteLine(faker.Address.City());
                int count = faker.Random.Number(1, 5);
                for (int i = 0; i < count; i++)
                {
                    string name = faker.Name.FirstName();
                    string @class = faker.PickRandom(Classess);
                    int health = faker.Random.Number(1, 25);
                    int mana = faker.Random.Number(1, 25);
                    int attack = faker.Random.Number(1, 25);
                    int defense = faker.Random.Number(1, 25);
                    writer.Write($"{name};{@class};{health};{mana};{attack};{defense};");
                    if (faker.Random.Number(100) < 60)
                    {
                        string guild = faker.Company.CompanyName();
                        writer.WriteLine($"{guild}");
                    } else
                    {
                        int power = faker.Random.Number(1, 25);
                        int agility = faker.Random.Number(1, 25);
                        int intellect = faker.Random.Number(1, 25);
                        int special = faker.Random.Number(1, 25);
                        writer.WriteLine($"{power};{agility};{intellect};{special}");
                    }
                }
            }
        }

        /// <summary>
        /// Read all files from a directory that have actors
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Actor> ReadActorsDir(string directory, string pattern = "*.txt")
        {
            if (!Directory.Exists(directory))
            {
                throw new Exception(string.Format("Directory '{0}' not found", directory));
            }
            var merged = new List<Actor>();
            foreach (var filename in Directory.GetFiles(directory, pattern))
            {
                merged.AddRange(ReadActors(filename));
            }
            return merged;
        }

        /// <summary>
        /// Writes a list of classes to a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <param name="classes">Target classes</param>
        public static void PrintClassesCSV(string filename, List<string> classes)
        {
            using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                foreach (var @class in classes)
                {
                    writer.WriteLine(@class);
                }
            }
        }

        /// <summary>
        /// Prints missing actors to a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <param name="missingActors">Missing actor race names</param>
        public static void PrintMissingActors(string filename, Tuple<List<string>, List<string>> missingActors)
        {
            using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                writer.WriteLine(string.Join(";", missingActors.Item1));
                writer.WriteLine(string.Join(";", missingActors.Item2));
            }
        }

        /// <summary>
        /// Prints a team of actors to a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <param name="team">Target actors</param>
        public static void PrintTeam(string filename, List<Actor> team)
        {
            using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                foreach (var actor in team)
                {
                    writer.WriteLine(actor.ToCSVLine());
                }
            }
        }
    }
}