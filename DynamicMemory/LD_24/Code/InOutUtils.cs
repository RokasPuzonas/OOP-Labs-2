using System;
using System.Collections.Generic;
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
                decimal price = decimal.Parse(parts[2].Trim());
                products.AddToEnd(new Product(id, name, price));
            }
            return products;
        }

        public static CustomerList ReadCustomers(string filename)
        {
            CustomerList customers = new CustomerList();
            foreach (string line in ReadLines(filename))
            {
                string[] parts = line.Split(',');
                string surname = parts[0].Trim();
                string name = parts[1].Trim();
                string productID = parts[2].Trim();
                int productAmount = int.Parse(parts[3].Trim());
                customers.AddToEnd(new Customer(surname, name, productID, productAmount));
            }
            return customers;
        }

        public static void PrintCustomers(StreamWriter writer, CustomerList customers, string header)
        {
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -59} |", header);
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -15} | {1, -15} | {2, 7} | {3, 13} |", "Pavardė", "Vardas", "Įtaisas", "Įtaiso kiekis");
            writer.WriteLine(new string('-', 63));
            if (customers.Count() > 0)
            {
                foreach (Customer c in customers)
                {
                    writer.WriteLine("| {0, -15} | {1, -15} | {2, 7} | {3, 13} |", c.Surname, c.Name, c.ProductID, c.ProductAmount);
                }
            }
            else
            {
                writer.WriteLine("| {0, -59} |", "Nėra");
            }
            writer.WriteLine(new string('-', 63));
            writer.WriteLine();
        }

        public static void PrintCustomersWithPrices(StreamWriter writer, CustomerList customers, Product product, string header)
        {
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -59} |", header);
            writer.WriteLine(new string('-', 63));
            writer.WriteLine("| {0, -15} | {1, -15} | {2, 13} | {3, 7} |", "Pavardė", "Vardas", "Įtaiso kiekis", "Kaina");
            writer.WriteLine(new string('-', 63));
            if (customers.Count() > 0)
            {
                foreach (Customer c in customers)
                {
                    writer.WriteLine("| {0, -15} | {1, -15} | {2, 13} | {3, 7:f2} |", c.Surname, c.Name, c.ProductAmount, c.ProductAmount *product.Price);
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
    }
}