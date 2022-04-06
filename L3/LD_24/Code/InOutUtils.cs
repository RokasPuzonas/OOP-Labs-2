using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Class used for reading and writing to files
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Read lines from a stream
        /// </summary>
        /// <param name="filename">Target filename</param>
        /// <returns>IEnumerable of all the lines</returns>
        private static IEnumerable<string> ReadLines(StreamReader stream)
        {
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Read products from a stream reader
        /// </summary>
        /// <param name="reader">Target stream reader</param>
        /// <returns>A list of products</returns>
        public static LinkedList<Product> ReadProducts(StreamReader reader)
        {
            LinkedList<Product> products = new LinkedList<Product>();
            foreach (string line in ReadLines(reader))
            {
                string[] parts = line.Split(',');
                string id = parts[0].Trim();
                string name = parts[1].Trim();
                decimal price = decimal.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
                products.Add(new Product(id, name, price));
            }
            return products;
        }

        /// <summary>
        /// Read products from a stream
        /// </summary>
        /// <param name="stream">Target stream</param>
        /// <returns>A list of products</returns>
        public static LinkedList<Product> ReadProducts(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return ReadProducts(reader);
            }
        }

        /// <summary>
        /// Read products from a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <returns>A list of products</returns>
        public static LinkedList<Product> ReadProducts(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                return ReadProducts(reader);
            }
        }

        /// <summary>
        /// Read orders from a file
        /// </summary>
        /// <param name="filename">Target file</param>
        /// <returns>A list of orders</returns>
        public static LinkedList<Order> ReadOrders(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                return ReadOrders(reader);
            }
        }

        /// <summary>
        /// Read orders from a stream
        /// </summary>
        /// <param name="stream">Target stream</param>
        /// <returns>A list of orders</returns>
        public static LinkedList<Order> ReadOrders(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return ReadOrders(reader);
            }
        }

        /// <summary>
        /// Read orders from a stream reader
        /// </summary>
        /// <param name="reader">Target stream reader</param>
        /// <returns>A list of orders</returns>
        public static LinkedList<Order> ReadOrders(StreamReader reader)
        {
            LinkedList<Order> orders = new LinkedList<Order>();
            foreach (string line in ReadLines(reader))
            {
                string[] parts = line.Split(',');
                string customerSurname = parts[0].Trim();
                string customerName = parts[1].Trim();
                string productID = parts[2].Trim();
                int productAmount = int.Parse(parts[3].Trim());
                orders.Add(new Order(customerSurname, customerName, productID, productAmount));
            }
            return orders;
        }

        /// <summary>
        /// Print a single table row to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="cells">Cell data</param>
        /// <param name="widths">Cell widths</param>
        private static void PrintTableRow(StreamWriter writer, LinkedList<string> cells, LinkedList<int> widths)
        {
            foreach (var tuple in cells.Zip(widths, (a, b) => Tuple.Create(a, b)))
            {
                if (tuple.Item1[0] == '-')
                    writer.Write("| {0} ", tuple.Item1.Substring(1).PadRight(tuple.Item2));
                else
                    writer.Write("| {0} ", tuple.Item1.PadLeft(tuple.Item2));
            }
            writer.WriteLine("|");
        }

        private static LinkedList<int> FindTableWidths(LinkedList<LinkedList<string>> rows, string header, string[] columns)
        {
            var allWidths = new Dictionary<int, LinkedList<int>>();
            
            int o = 0;
            foreach (var column in columns)
            {
                allWidths.Add(o, new LinkedList<int> { column.Length });
                o++;
            }

            foreach (var row in rows)
            {
                int p = 0;
                foreach (var cell in row)
                {
                    allWidths[p].Add(cell.Length);
                    p++;
                }
            }

            var widths = new LinkedList<int>();
            int totalWidth = 3 * (columns.Length - 1);
            foreach (var columnWidths in allWidths.Values)
            {
                int width = columnWidths.Max();
                totalWidth += width;
                widths.Add(width);
            }

            // If the header is longer than the body, make the last column wider.
            // So the table is a nice rectangle when output to the file
            if (header.Length > totalWidth)
            {
                // Make the last column a bit wider so everything lines up
                widths.Add(widths.RemoveLast() + header.Length - totalWidth);
            }

            return widths;
        }

        /// <summary>
        /// Print a table to a file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="header">Header above table</param>
        /// <param name="list">Target list</param>
        /// <param name="columns">Column names</param>
        /// <returns>A IEnumerable for inserting values for each row</returns>
        private static IEnumerable<Tuple<T, LinkedList<string>>> PrintTable<T>(StreamWriter writer, string header, IEnumerable<T> list, params string[] columns)
        {
            // 1. Collect all the rows
            LinkedList<LinkedList<string>> rows = new LinkedList<LinkedList<string>>();
            foreach (T item in list)
            {
                var row = new LinkedList<string>();
                yield return Tuple.Create(item, row);
                rows.Add(row);
            }

            // 2. Determine the width of each column
            var widths = FindTableWidths(rows, header, columns);
            int totalWidth = 3 * (columns.Length - 1) + 2 * 2 + widths.Sum();

            // 3. Display the table
            writer.WriteLine(new string('-', totalWidth));
            writer.WriteLine("| {0} |", header.PadRight(totalWidth - 4));
            writer.WriteLine(new string('-', totalWidth));
            PrintTableRow(writer, new LinkedList<string>(columns), widths);
            writer.WriteLine(new string('-', totalWidth));
            if (!rows.IsEmpty())
            {
                foreach (var row in rows)
                {
                    PrintTableRow(writer, row, widths);
                }
            } else
            {
                writer.WriteLine("| {0} |", "Nėra".PadRight(totalWidth - 4));
            }
            writer.WriteLine(new string('-', totalWidth));

            writer.WriteLine();
        }

        /// <summary>
        /// Print orders table to file 
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="orders">List of orders</param>
        /// <param name="header">Header above table</param>
        public static void PrintOrders(StreamWriter writer, IEnumerable<Order> orders, string header)
        {
            foreach (var tuple in PrintTable(writer, header, orders, "Pavardė", "Vardas", "-Įtaisas", "-Įtaiso kiekis"))
            {
                Order order = tuple.Item1;
                var row = tuple.Item2;
                row.Add(order.CustomerSurname);
                row.Add(order.CustomerName);
                row.Add(order.ProductID);
                row.Add(order.ProductAmount.ToString());
            }
        }

        /// <summary>
        /// Print orders with prices table to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="orders">List of orders</param>
        /// <param name="products">List of products</param>
        /// <param name="header">Header above table</param>
        public static void PrintOrdersWithPrices(StreamWriter writer, IEnumerable<Order> orders, IEnumerable<Product> products, string header)
        {
            foreach (var tuple in PrintTable(writer, header, orders, "Pavardė", "Vardas", "-Įtaiso kiekis, vnt.", "-Kaina, eur."))
            {
                Order order = tuple.Item1;
                var row = tuple.Item2;
                Product product = TaskUtils.FindByID(products, order.ProductID);

                row.Add(order.CustomerSurname);
                row.Add(order.CustomerName);
                row.Add(order.ProductAmount.ToString());
                row.Add(string.Format("{0:f2}", order.ProductAmount * product.Price));
            }
        }

        /// <summary>
        /// Print a products table to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="products">List of products</param>
        /// <param name="header">Header above table</param>
        public static void PrintProducts(StreamWriter writer, IEnumerable<Product> products, string header)
        {
            foreach (var tuple in PrintTable(writer, header, products, "ID", "Vardas", "-Kaina"))
            {
                Product product = tuple.Item1;
                var row = tuple.Item2;
                row.Add(product.ID);
                row.Add(product.Name);
                row.Add(string.Format("{0:f2}", product.Price));
            }
        }

        /// <summary>
        /// Print a table of most popular products to file
        /// </summary>
        /// <param name="writer">Target file</param>
        /// <param name="orders">List of orders</param>
        /// <param name="popularProducts">List of most popular products</param>
        /// <param name="header">Header above table</param>
        public static void PrintMostPopularProducts(StreamWriter writer, IEnumerable<Order> orders, IEnumerable<Product> popularProducts, string header)
        {
            foreach (var tuple in PrintTable(writer, header, popularProducts, "ID", "Vardas", "-Įtaisų kiekis, vnt.", "-Įtaisų kaina, eur."))
            {
                Product product = tuple.Item1;
                var row = tuple.Item2;
                int sales = TaskUtils.CountProductSales(orders, product.ID);
                row.Add(product.ID);
                row.Add(product.Name);
                row.Add(sales.ToString());
                row.Add(string.Format("{0:f2}", sales * product.Price));
            }
        }
    }
}