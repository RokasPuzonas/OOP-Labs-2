using System;
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

        public static void PrintOrders(StreamWriter writer, OrderList orders, string header)
        {
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -59} |", header);
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -15} | {1, -15} | {2, 7} | {3, 13} |", "Pavardė", "Vardas", "Įtaisas", "Įtaiso kiekis");
            writer.WriteLine(new string('-', 63));
            if (orders.Count() > 0)
            {
                foreach (Order o in orders)
                {
                    writer.WriteLine("| {0, -15} | {1, -15} | {2, 7} | {3, 13} |", o.CustomerSurname, o.CustomerName, o.ProductID, o.ProductAmount);
                }
            }
            else
            {
                writer.WriteLine("| {0, -59} |", "Nėra");
            }
            writer.WriteLine(new string('-', 63));
            writer.WriteLine();
        }

        public static void PrintOrdersWithPrices(StreamWriter writer, OrderList orders, Product product, string header)
        {
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -59} |", header);
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -15} | {1, -15} | {2, 13} | {3, 7} |", "Pavardė", "Vardas", "Įtaiso kiekis", "Kaina");
            writer.WriteLine(new string('-', 63));

            OrderList filtered = TaskUtils.FilterByProduct(orders, product.ID);
            filtered = TaskUtils.MergeOrders(filtered);
            filtered.Sort();

            if (filtered.Count() > 0)
            {
                foreach (Order o in filtered)
                {
                    writer.WriteLine("| {0, -15} | {1, -15} | {2, 13} | {3, 7:f2} |", o.CustomerSurname, o.CustomerName, o.ProductAmount, o.ProductAmount*product.Price);
                }
            }
            else
            {
                writer.WriteLine("| {0, -59} |", "Nėra");
            }
            writer.WriteLine(new string('-', 63));
            writer.WriteLine();
        }

        public static void PrintProducts(StreamWriter writer, ProductList products, string header)
        {
            writer.WriteLine(new string('-', 42));
            writer.WriteLine("| {0, -38} |", header);
            writer.WriteLine(new string('-', 42));
            writer.WriteLine("| {0, -5} | {1, -20} | {2, 7} |", "ID", "Vardas", "Kaina");
            writer.WriteLine(new string('-', 42));
            if (products.Count() > 0)
            {
                foreach (Product c in products)
                {
                    writer.WriteLine("| {0, -5} | {1, -20} | {2, 7} |", c.ID, c.Name, c.Price);
                }
            }
            else
            {
                writer.WriteLine("| {0, -38} |", "Nėra");
            }
            writer.WriteLine(new string('-', 42));
            writer.WriteLine();
        }


        public static void PrintMostPopularProducts(StreamWriter writer, OrderList orders, ProductList popularProducts, string header)
        {
            writer.WriteLine(new string('-', 75));
            writer.WriteLine("| {0, -71} |", header);
            writer.WriteLine(new string('-', 75));
            writer.WriteLine("| {0, -5} | {1, -20} | {2, 7} | {3} |", "ID", "Vardas", "Įtaisų kiekis, vnt.", "Įtaisų kaina, eur.");
            writer.WriteLine(new string('-', 75));
            if (popularProducts.Count() > 0)
            {
                foreach (Product p in popularProducts)
                {
                    int sales = TaskUtils.CountProductSales(orders, p.ID);
                    writer.WriteLine("| {0, -5} | {1, -20} | {2, 19} | {3, 18:f2} |", p.ID, p.Name, sales, sales*p.Price);
                }
            }
            else
            {
                writer.WriteLine("| {0, -71} |", "Nėra");
            }
            writer.WriteLine(new string('-', 75));
            writer.WriteLine();
        }
    }
}