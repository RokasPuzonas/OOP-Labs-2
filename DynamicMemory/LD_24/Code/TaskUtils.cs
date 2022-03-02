using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public static class TaskUtils
    {
        public static string FindMostPopularProduct(CustomerList customers)
        {
            Dictionary<string, int> productCounter = new Dictionary<string, int>();
            foreach (Customer customer in customers)
            {
                if (!productCounter.ContainsKey(customer.ProductID))
                {
                    productCounter.Add(customer.ProductID, 1);
                }
                else
                {
                    productCounter[customer.ProductID]++;
                }
            }

            string mostPopularProduct = null;
            int mostPopularCount = 0;
            foreach (string product in productCounter.Keys)
            {
                int count = productCounter[product];
                if (count > mostPopularCount)
                {
                    mostPopularCount = count;
                    mostPopularProduct = product;
                }
            }

            return mostPopularProduct;
        }

        public static int CountProductSales(CustomerList customers, string product)
        {
            int sales = 0;
            foreach (Customer customer in customers)
            {
                if (customer.ProductID == product)
                {
                    sales += customer.ProductAmount;
                }
            }
            return sales;
        }

        public static CustomerList FilterByProduct(CustomerList customers, string product)
        {
            CustomerList filtered = new CustomerList();
            foreach (Customer customer in customers)
            {
                if (customer.ProductID == product)
                {
                    filtered.AddToEnd(customer);
                }
            }
            return filtered;
        }

        public static Product FindByID(ProductList products, string id)
        {
            foreach (Product product in products)
            {
                if (product.ID == id)
                {
                    return product;
                }
            }
            return null;
        }

        public static ProductList FilterByMaxPrice(ProductList products, decimal maxPrice)
        {
            ProductList filtered = new ProductList();
            foreach (Product product in products)
            {
                if (product.Price < maxPrice)
                {
                    filtered.AddToEnd(product);
                }
            }
            return filtered;
        }

        public static ProductList FilterByMinQuantitySold(ProductList products, CustomerList customers, decimal minSold)
        {
            ProductList filtered = new ProductList();
            foreach (Product product in products)
            {
                int sold = CountProductSales(customers, product.ID);
                if (sold >= minSold)
                {
                    filtered.AddToEnd(product);
                }
            }
            return filtered;
        }

    }
}
