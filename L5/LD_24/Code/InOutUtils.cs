using Bogus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Input/Output utilities
    /// </summary>
    public static class InOutUtils
    {
        private static readonly Faker faker = new Faker();

        /// <summary>
        /// Generate some initial data, by some initial parameters
        /// </summary>
        /// <param name="subscriberAmount"></param>
        /// <param name="publicationAmount"></param>
        /// <param name="outputFolder"></param>
        public static void GenerateInitialData(int subscriberAmount, int publicationAmount, string outputFolder)
        {
            var publisherAmount = faker.Random.Number(2, publicationAmount);
            var publishers = new List<string>();
            for (int i = 0; i < publisherAmount; i++)
            {
                publishers.Add(faker.Company.CompanyName());
            }

            var publicationIDs = new List<string>();
            using (var writer = new StreamWriter(Path.Combine(outputFolder, "Publications.txt"), false, Encoding.UTF8))
            {
                for (int i = 0; i < publicationAmount; i++)
                {
                    string ID = faker.Random.AlphaNumeric(3).ToUpper();
                    string title = faker.Commerce.ProductName();
                    string publisher = faker.PickRandom(publishers);
                    decimal price = decimal.Round(faker.Random.Decimal(0.5M, 50.00M), 2);
                    
                    publicationIDs.Add(ID);
                    writer.WriteLine(string.Join(";", ID, title, publisher, price));
                }
            }
            
            for (int i = 0; i < subscriberAmount; i++)
            {
                using (var writer = new StreamWriter(Path.Combine(outputFolder, $"Subscribers{i+1}.txt"), false, Encoding.UTF8))
                {
                    writer.WriteLine(faker.Date.Past().ToShortDateString());
                    for (int j = 0; j < faker.Random.Number(3, 10); j++)
                    {
                        string surname = faker.Name.LastName();
                        string address = faker.Address.StreetAddress();
                        int subscriptionStart = faker.Random.Number(1, 12);
                        int subscriptionLength = faker.Random.Number(1, 13 - subscriptionStart);
                        var subscriptionID = faker.PickRandom(publicationIDs);
                        var subscrionCount = faker.Random.Number(1, 10);

                        writer.WriteLine(string.Join(";", surname, address, subscriptionStart, subscriptionLength, subscriptionID, subscrionCount));
                    }
                }
            }
        }

        /// <summary>
        /// Read all publications from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Publication> ReadPublications(string filename)
        {
            var publications = new List<Publication>();
            foreach (var line in File.ReadLines(filename))
            {
                string[] parts = line.Split(';');
                if (parts.Length != 4)
                {
                    throw new Exception($"Invalid subscriber line: '{line}'");
                }
                string id = parts[0].Trim();
                string title = parts[1].Trim();
                string publisher = parts[2].Trim();
                decimal pricePerMonth = decimal.Parse(parts[3]);
                publications.Add(new Publication(id, title, publisher, pricePerMonth));
            }
            return publications;
        }

        /// <summary>
        /// Read subscribers from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Subscriber> ReadSubscribers(string filename)
        {
            var subscribers = new List<Subscriber>();
            var lines = File.ReadLines(filename);
            DateTime enterDate = DateTime.Parse(lines.First());
            foreach (var line in lines.Skip(1))
            {
                string[] parts = line.Split(';');
                if (parts.Length != 6)
                {
                    throw new Exception($"Invalid subscriber line: '{line}'");
                }
                string surname = parts[0].Trim();
                string address = parts[1].Trim();
                int subscriptionStart = int.Parse(parts[2]);
                int subscriptionLength = int.Parse(parts[3]);
                string subscriptionID = parts[4].Trim();
                int subscrionCount = int.Parse(parts[5]);
                subscribers.Add(new Subscriber(enterDate, surname, address, subscriptionStart, subscriptionLength, subscriptionID, subscrionCount));
            }
            return subscribers;
        }

        /// <summary>
        /// Read all subscribers from a directory
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<List<Subscriber>> ReadSubscribersFromDir(string folder, string pattern = "*.txt")
        {
            if (!Directory.Exists(folder))
            {
                throw new Exception($"Folder '{folder}' not found");
            }
            var subscribers = new List<List<Subscriber>>();
            foreach (var filename in Directory.GetFiles(folder, pattern))
            {
                subscribers.Add(ReadSubscribers(filename));
            }
            return subscribers;
        }

        /// <summary>
        /// Print a table of publications
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="publications"></param>
        public static void PrintPublications(ResultsWriter writer, IEnumerable<Publication> publications)
        {
            foreach (var tuple in writer.WriteTable(publications, "Kodas", "Pavadinimas", "Leidėjas", "Kaina"))
            {
                var publication = tuple.Item1;
                var row = tuple.Item2;
                row.Add(publication.ID);
                row.Add(publication.Title);
                row.Add(publication.Publisher);
                row.Add(publication.PricePerMonth);
            }
        }

        /// <summary>
        /// Print a table of subscribers
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="subscribers"></param>
        public static void PrintSubscribers(ResultsWriter writer, IEnumerable<Subscriber> subscribers)
        {
            foreach (var tuple in writer.WriteTable(subscribers, "Įvedimo data", "Pavardė", "Adresas", "Pradžia", "Ilgis", "Kodas", "Kiekis"))
            {
                var subscriber = tuple.Item1;
                var row = tuple.Item2;
                row.Add(subscriber.EnterDate.ToShortDateString());
                row.Add(subscriber.Surname);
                row.Add(subscriber.Address);
                row.Add(subscriber.SubscriptionStart);
                row.Add(subscriber.SubscriptionLength);
                row.Add(subscriber.SubscriptionID);
                row.Add(subscriber.SubscrionCount);
            }
        }

        /// <summary>
        /// Print a list of table of subscribers
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="manySubscribers"></param>
        public static void PrintManySubscribers(ResultsWriter writer, IEnumerable<IEnumerable<Subscriber>> manySubscribers)
        {
            foreach (var subscribers in manySubscribers)
            {
                PrintSubscribers(writer, subscribers);
            }
        }
    }
}