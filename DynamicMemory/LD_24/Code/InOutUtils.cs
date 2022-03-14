using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public static class InOutUtils
    {
        private static IEnumerable<string> ReadLines(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
        public static ProductList ReadProducts(string filename)
        {
            ProductList products = new ProductList();
            foreach (string line in ReadLines(filename))
            {
                string[] parts = line.Split(',');
                string id = parts[0].Trim();
                string name = parts[1].Trim();
                decimal price = decimal.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
                products.AddToEnd(new Product(id, name, price));
            }
            return products;
        }

        public static OrderList ReadOrders(string filename)
        {
            OrderList orders = new OrderList();
            foreach (string line in ReadLines(filename))
            {
                string[] parts = line.Split(',');
                string customerSurname = parts[0].Trim();
                string customerName = parts[1].Trim();
                string productID = parts[2].Trim();
                int productAmount = int.Parse(parts[3].Trim());
                orders.AddToEnd(new Order(customerSurname, customerName, productID, productAmount));
            }
            return orders;
        }

        private static void PrintTableRow(StreamWriter writer, List<string> cells, List<int> widths)
        {
            for (int i = 0; i < widths.Count; i++)
            {
                if (widths[i] < 0)
                    writer.Write("| {0} ", cells[i].PadRight(-widths[i]));
                else
                    writer.Write("| {0} ", cells[i].PadLeft(widths[i]));
            }
            writer.WriteLine("|");
        }

        private static IEnumerable<Tuple<object, List<String>>> PrintTable(StreamWriter writer, string header, IEnumerable list, params string[] columns)
        {
            // 0. Collect all the rows
            List<List<string>> rows = new List<List<string>>();
            foreach (object item in list)
            {
                List<string> row = new List<string>();
                yield return Tuple.Create(item, row);
                rows.Add(row);
            }

            // 1. Determine the width of each column
            List<int> widths = new List<int>();
            int totalWidth = 3*(columns.Length - 1);
            for (int i = 0; i < columns.Length; i++)
            {
                int width = columns[i].Length;
                foreach (var row in rows)
                {
                    width = Math.Max(row[i].Length, width);
                }
                widths.Add(width);
                totalWidth += width;
            }

            // If the header is longer than the body, make the last column wider.
            // So the table is a nice rectangle when output to the file
            if (header.Length > totalWidth)
            {
                widths[widths.Count - 1] += (header.Length - totalWidth);
                totalWidth = header.Length;
            }

            totalWidth += 2 * 2;

            // 2. Adjust widths to account for aligning
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i][0] == '-')
                {
                    widths[i] = -widths[i];
                    columns[i] = columns[i].Substring(1);
                }
            }

            // 3. Display the table
            writer.WriteLine(new string('-', totalWidth));
            writer.WriteLine("| {0} |", header.PadRight(totalWidth - 4));
            writer.WriteLine(new string('-', totalWidth));
            PrintTableRow(writer, new List<string>(columns), widths);
            writer.WriteLine(new string('-', totalWidth));
            if (rows.Count > 0)
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

        public static void PrintOrders(StreamWriter writer, OrderList orders, string header)
        {
            foreach (var tuple in PrintTable(writer, header, orders, "Pavardė", "Vardas", "-Įtaisas", "-Įtaiso kiekis"))
            {
                Order order = (Order)tuple.Item1;
                List<string> row = tuple.Item2;
                row.Add(order.CustomerSurname);
                row.Add(order.CustomerName);
                row.Add(order.ProductID);
                row.Add(order.ProductAmount.ToString());
            }
        }

        public static void PrintOrdersWithPrices(StreamWriter writer, OrderList orders, ProductList products, string header)
        {
            foreach (var tuple in PrintTable(writer, header, orders, "Pavardė", "Vardas", "-Įtaiso kiekis, vnt.", "-Kaina, eur."))
            {
                Order order = (Order)tuple.Item1;
                List<string> row = tuple.Item2;
                Product product = TaskUtils.FindByID(products, order.ProductID);

                row.Add(order.CustomerSurname);
                row.Add(order.CustomerName);
                row.Add(order.ProductAmount.ToString());
                row.Add(String.Format("{0:f2}", order.ProductAmount * product.Price));
            }
        }

        public static void PrintProducts(StreamWriter writer, ProductList products, string header)
        {
            foreach (var tuple in PrintTable(writer, header, products, "ID", "Vardas", "-Kaina"))
            {
                Product product = (Product)tuple.Item1;
                List<string> row = tuple.Item2;
                row.Add(product.ID);
                row.Add(product.Name);
                row.Add(String.Format("{0:f2}", product.Price));
            }
        }

        public static void PrintMostPopularProducts(StreamWriter writer, OrderList orders, ProductList popularProducts, string header)
        {
            foreach (var tuple in PrintTable(writer, header, popularProducts, "ID", "Vardas", "-Įtaisų kiekis, vnt.", "-Įtaisų kaina, eur."))
            {
                Product product = (Product)tuple.Item1;
                List<string> row = tuple.Item2;
                int sales = TaskUtils.CountProductSales(orders, product.ID);
                row.Add(product.ID);
                row.Add(product.Name);
                row.Add(sales.ToString());
                row.Add(String.Format("{0:f2}", sales * product.Price));
            }
        }
    }
}