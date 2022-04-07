using System;
using System.IO;

namespace K2
{
    public interface IBetween<T>
    {
        /// <summary>
        /// Indicates whether the value of the certain property of the current instance is in
        /// [<paramref name="from"/>, <paramref name="to"/>] range including range marginal values.
        /// <paramref name="from"/> should always precede <paramref name="to"/> in default sort order.
        /// </summary>
        /// <param name="from">The starting value of the range</param>
        /// <param name="to">The ending value of the range</param>
        /// <returns>true if the value of the current object property is in range; otherwise,
        /// false.</returns>
        bool MutuallyInclusiveBetween(T from, T to);

        /// <summary>
        /// Indicates whether the value of the certain property of the current instance is in
        /// [<paramref name="from"/>, <paramref name="to"/>] range excluding range marginal values.
        /// <paramref name="from"/> should always precede <paramref name="to"/> in default sort order.
        /// </summary>
        /// <param name="from">The starting value of the range</param>
        /// <param name="to">The ending value of the range</param>
        /// <returns>true if the value of the current object property is in range; otherwise,
        /// false.</returns>
        bool MutuallyExclusiveBetween(T from, T to);
    }

    /// <summary>
    /// Provides generic container where the data are stored in the linked list.
    /// </summary>
    /// <typeparam name="T">The type of the data to be stored in the list. Data
    /// class should implement some interfaces.</typeparam>
    public class LinkList<T>
        where T: IComparable<T>
    {
        class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        /// <summary>
        /// All the time should point to the first element of the list.
        /// </summary>
        private Node begin;

        /// <summary>
        /// All the time should point to the last element of the list.
        /// </summary>
        private Node end;

        /// <summary>
        /// Shouldn't be used in any other methods except Begin(), Next(), Exist() and Get().
        /// </summary>
        private Node current;

        /// <summary>
        /// Initializes a new instance of the LinkList class with empty list.
        /// </summary>
        public LinkList()
        {
            begin = current = end = null;
        }

        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the first element of the list.
        /// </summary>
        public void Begin()
        {
            current = begin;
        }
        
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the next element of the list.
        /// </summary>
        public void Next()
        {
            current = current.Next;
        }

        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to check whether the internal pointer points to the element of the list.
        /// </summary>
        /// <returns>true, if the internal pointer points to some element of the list; otherwise,
        /// false.</returns>
        public bool Exist()
        {
            return current != null;
        }

        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to get the value stored in the node pointed by the internal pointer.
        /// </summary>
        /// <returns>the value of the element that is pointed by the internal pointer.</returns>
        public T Get()
        {
            return current.Data;
        }

        /// <summary>
        /// Method appends new node to the end of the list and saves in it <paramref name="data"/>
        /// passed by the parameter.
        /// </summary>
        /// <param name="data">The data to be stored in the list.</param>
        public void Add(T data)
        {
            var node = new Node(data, null);
            if (begin == null)
            {
                begin = node;
                end = node;
            } else
            {
                end.Next = node;
                end = node;
            }
        }

        /// <summary>
        /// Method sorts data in the list. The data object class should implement IComparable
        /// interface though defining sort order.
        /// </summary>
        public void Sort()
        {
            for (Node nodeA = begin; nodeA != null; nodeA = nodeA.Next)
            {
                if (nodeA.Next == null) continue;

                for (Node nodeB = nodeA.Next; nodeB != null; nodeB = nodeB.Next)
                {
                    if (nodeA.Data.CompareTo(nodeB.Data) < 0)
                    {
                        T tmp = nodeA.Data;
                        nodeA.Data = nodeB.Data;
                        nodeB.Data = tmp;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Provides properties and interface implementations for the storing and manipulating of cars data.
    /// </summary>
    public class Car : IComparable<Car>, IBetween<double>, IBetween<string>
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }

        public Car(string manufacturer, string model, double price)
        {
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same
        /// position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.The
        /// return value has these meanings: -1 when this instance precedes other in the sort order;
        /// 0 when this instance occurs in the same position in the sort order as other;
        /// 1 when this instance follows other in the sort order.</returns>
        public int CompareTo(Car other)
        {
            if (Price < other.Price)
            {
                return -1;
            } else if (Price == other.Price)
            {
                return -Model.CompareTo(other.Model);
            } else
            {
                return 1;
            }
        }

        public bool MutuallyInclusiveBetween(double from, double to)
        {
            return from <= Price && Price <= to;
        }

        public bool MutuallyExclusiveBetween(double from, double to)
        {
            return from < Price && Price < to;
        }

        public bool MutuallyInclusiveBetween(string from, string to)
        {
            return Manufacturer.CompareTo(from) <= 0 && Manufacturer.CompareTo(to) >= 0;
        }

        public bool MutuallyExclusiveBetween(string from, string to)
        {
            return Manufacturer.CompareTo(from) < 0 && Manufacturer.CompareTo(to) > 0;
        }
    }

    public static class InOut
    {
        /// <summary>
        /// Creates the list containing data read from the text file.
        /// </summary>
        /// <param name="fileName">The name of the text file</param>
        /// <returns>List with data from file</returns>
        public static LinkList<Car> ReadFromFile(string fileName)
        {
            var cars = new LinkList<Car>();
            foreach (var line in File.ReadAllLines(fileName))
            {
                string[] parts = line.Split(';');
                string manufacturer = parts[0].Trim();
                string model = parts[1].Trim();
                double price = double.Parse(parts[2].Trim());
                cars.Add(new Car(manufacturer, model, price));
            }
            return cars;
        }

        /// <summary>
        /// Appends the table, built from data contained in the list and preceded by the header,
        /// to the end of the text file.
        /// </summary>
        /// <param name="fileName">The name of the text file</param>
        /// <param name="header">The header of the table</param>
        /// <param name="list">The list from which the table to be formed</param>
        public static void PrintToFile(string fileName, string header, LinkList<Car> list)
        {
            using (var writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(new string('-', 53));
                writer.WriteLine("| {0, -49} |", header);
                writer.WriteLine(new string('-', 53));

                // Klausimas: Ar GRIEŽTAI negalima kurtis savo pagalbinių funkcijų?
                //  Nes šitoje situacijoje žemiau labai praverstų tiesiog susikurti `IsEmpty` metodą.
                //  Kartu prie to pačio, man labai nepatinka `Begin`, `Exist` ir `Next`, tai koks `IEnumerable`
                // manau yra žymiai geresnis sprendimas, kad kaip sukti ciklą per listą.
                list.Begin();
                if (list.Exist())
                {
                    writer.WriteLine("| {0,-20} | {1,-15} | {2,8} |", "Gamintojas", "Modelis", "Kaina");
                    writer.WriteLine(new string('-', 53));
                    for (list.Begin(); list.Exist(); list.Next())
                    {
                        var car = list.Get();
                        writer.WriteLine("| {0,-20} | {1,-15} | {2,8:f2} |", car.Manufacturer, car.Model, car.Price);
                    }
                } else
                {
                    writer.WriteLine("| {0, -49} |", "Nėra");
                }
                writer.WriteLine(new string('-', 53));
                writer.WriteLine();
            }
        }
    }

    public static class Task
    {
        /// <summary>
        /// The method finds the biggest price value in the given list.
        /// </summary>
        /// <param name="list">The data list to be searched.</param>
        /// <returns>The biggest price value.</returns>
        public static double MaxPrice(LinkList<Car> list)
        {
            double maxPrice = -1;
            for (list.Begin(); list.Exist(); list.Next())
            {
                var car = list.Get();
                maxPrice = Math.Max(maxPrice, car.Price);
            }
            return maxPrice;
        }

        /// <summary>
        /// Filters data from the source list that meets filtering criteria and writes them
        /// into the new list.
        /// </summary>
        /// <typeparam name="TData">The type of the data objects stored in the list</typeparam>
        /// <typeparam name="TCriteria">The type of criteria</typeparam>
        /// <param name="source">The source list from which the result would be created</param>
        /// <param name="from">Lower bound of the interval</param>
        /// <param name="to">Upper bound of the interval</param>
        /// <returns>The list that contains filtered data</returns>
        public static LinkList<TData> Filter<TData, TCriteria>(LinkList<TData> source, TCriteria from, TCriteria to)
                where TData : IComparable<TData>, IBetween<TCriteria>
        {
            var filtered = new LinkList<TData>();
            for (source.Begin(); source.Exist(); source.Next())
            {
                var item = source.Get();
                if (item.MutuallyInclusiveBetween(from, to))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }

    class Program
    {
        // Klausimas: Ar reikia kurti testus `LinkList` klasei?
        static void Main()
        {
            string inputFilename = "Duomenys.txt";
            string outputFilename = "Rezultatai.txt";
            File.Delete(outputFilename);

            Console.Write("Įveskite pirmą gamintoją: ");
            string manufacturer1 = Console.ReadLine();
            Console.Write("Įveskite antrą gamintoją: ");
            string manufacturer2 = Console.ReadLine();

            var cars1 = InOut.ReadFromFile(inputFilename);
            InOut.PrintToFile(outputFilename, "Pradinės mašinos", cars1);

            var cars2 = Task.Filter(cars1, manufacturer1, manufacturer1);
            var cars3 = Task.Filter(cars1, manufacturer2, manufacturer2);
            InOut.PrintToFile(outputFilename, string.Format("Mašinos pagal gamintoją '{0}'", manufacturer1), cars2);
            InOut.PrintToFile(outputFilename, string.Format("Mašinos pagal gamintoją '{0}'", manufacturer2), cars3);

            var maxPrice = Task.MaxPrice(cars1);
            var cars4 = Task.Filter(cars2, maxPrice * 0.75, maxPrice);
            var cars5 = Task.Filter(cars3, maxPrice * 0.75, maxPrice);
            cars4.Sort();
            cars5.Sort();
            InOut.PrintToFile(outputFilename, string.Format("Brangios mašinos pagal gamintoją '{0}'", manufacturer1), cars4);
            InOut.PrintToFile(outputFilename, string.Format("Brangios mašinos pagal gamintoją '{0}'", manufacturer2), cars5);

            // Klausimas: Ar galima įdėti išdemą į failą maine?
            //  Taip, žinau per LD darbus negalima tai daryti. Bet K2 apraše
            // parašyta, kad negalima pridėti naujų metodų ar keisti egzistuojančių
            // kamienus.
            //  Bet reikalaujama išvesti surastą didžiausią kainą į failo galą
            // su paaiškinamu tekstu.
            // Paradoksas ¯\_(ツ)_/¯
            using (var writer = new StreamWriter(outputFilename, true))
            {
                writer.WriteLine("Didžiausia mašinos kaina: {0:f2}", maxPrice);
            }

            // Klausimas: Kas sugalvojo šitą užduotį?!?!?!
            //   Kodėl tą inteface reikia panaudoti 2 kartus ant `Car`
            // klasės, kad būtų galima patikrinti skirtingus laukus?! Atrodo, kad specialiai
            // sąlyga buvo 'priplakta' su daug nereikalingų dalykų.
            //  Dar reikėjo panaudoti `Task.Filter` du kartus su skirtingais interface kad tiktais
            // filtruoti paprastą dalyką ir galų gale gavosi, kad reikia aplamai turėti daugiau kodo
            // kad pasiekti tą patį rezultatą. Ir dar lieka `MutuallyExclusiveBetween` metodai nepanaudoti.
            //
            // Ką galiu pasakyti tai, kad man labai nepatiko šita užduotis.
        }
    }
}
