using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Various functions for operations on data
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Finds the most popular products by the number of sales
        /// </summary>
        /// <param name="orders">List of orders</param>
        /// <returns>List of products ids</returns>
        public static LinkedList<string> FindMostPopularProducts(IEnumerable<Order> orders)
        {
            Dictionary<string, int> productSales = new Dictionary<string, int>();
            foreach (Order order in orders)
            {
                if (!productSales.ContainsKey(order.ProductID))
                {
                    productSales.Add(order.ProductID, order.ProductAmount);
                }
                else
                {
                    productSales[order.ProductID] += order.ProductAmount;
                }
            }

            LinkedList<string> mostPopularProducts = new LinkedList<string>();
            int mostPopularCount = 0;
            foreach (string product in productSales.Keys)
            {
                int count = productSales[product];
                if (count > mostPopularCount)
                {
                    mostPopularCount = count;
                    mostPopularProducts = new LinkedList<string> { product };
                } else if (count == mostPopularCount)
                {
                    mostPopularProducts.Add(product);
                }
            }

            return mostPopularProducts;
        }

        /// <summary>
        /// Counts the number of sales of a certain product
        /// </summary>
        /// <param name="orders">List of products</param>
        /// <param name="product">Target product id</param>
        /// <returns>Sales</returns>
        public static int CountProductSales(IEnumerable<Order> orders, string product)
        {
            int sales = 0;
            foreach (Order order in orders)
            {
                if (order.ProductID == product)
                {
                    sales += order.ProductAmount;
                }
            }
            return sales;
        }

        /// <summary>
        /// Merge orders which have the same customer name, surname and product id into a single order.
        /// </summary>
        /// <param name="orders">A list of orders</param>
        /// <returns>A list of orders where same orders have been merged</returns>
        public static LinkedList<Order> MergeOrders(IEnumerable<Order> orders)
        {
            Dictionary<Tuple<string, string, string>, Order> ordersByName = new Dictionary<Tuple<string, string, string>, Order>();
            foreach (var order in orders)
            {
                var key = Tuple.Create(order.CustomerSurname, order.CustomerName, order.ProductID);
                if (ordersByName.ContainsKey(key))
                {
                    ordersByName[key].ProductAmount += order.ProductAmount;
                } else
                {
                    ordersByName.Add(key, new Order(order.CustomerSurname, order.CustomerName, order.ProductID, order.ProductAmount));
                }
            }

            LinkedList<Order> mergedOrders = new LinkedList<Order>();
            foreach (var order in ordersByName.Values)
            {
                mergedOrders.Add(order);
            }
            return mergedOrders;
        }

        /// <summary>
        /// Finds a product by it's id
        /// </summary>
        /// <param name="products">List of products</param>
        /// <param name="id">Target product id</param>
        /// <returns>The product</returns>
        public static Product FindByID(IEnumerable<Product> products, string id)
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

        /// <summary>
        /// Find all products by their ids
        /// </summary>
        /// <param name="products">List of products</param>
        /// <param name="ids">List of product ids</param>
        /// <returns>List of products</returns>
        public static LinkedList<Product> FindByID(IEnumerable<Product> products, LinkedList<string> ids)
        {
            LinkedList<Product> foundProducts = new LinkedList<Product>();
            foreach (string id in ids)
            {
                foundProducts.Add(FindByID(products, id));
            }
            return foundProducts;
        }

        /// <summary>
        /// Filter a list of products by sales and price.
        /// </summary>
        /// <param name="products">List of products</param>
        /// <param name="orders">List of orders</param>
        /// <param name="minSold">Minimmum sales amount</param>
        /// <param name="maxPrice">Max product price</param>
        /// <returns>A list of filtered products</returns>
        public static LinkedList<Product> FilterByQuantitySoldAndPrice(IEnumerable<Product> products, IEnumerable<Order> orders, int minSold, decimal maxPrice)
        {
            LinkedList<Product> filtered = new LinkedList<Product>();
            foreach (Product product in products)
            {
                if (product.Price < maxPrice)
                {
                    int sold = CountProductSales(orders, product.ID);
                    if (sold >= minSold)
                    {
                        filtered.Add(product);
                    }
                }
            }
            return filtered;
        }

        /// <summary>
        /// Find all customer which bought only 1 type of product
        /// </summary>
        /// <param name="orders">List of orders</param>
        /// <returns>A list of filtered orders</returns>
        public static LinkedList<Order> FindCustomerWithSingleProduct(IEnumerable<Order> orders)
        {
            var ordersByCusomer = new Dictionary<Tuple<string, string>, LinkedList<Order>>(); 
            foreach (var order in MergeOrders(orders))
            {
                var key = Tuple.Create(order.CustomerName, order.CustomerSurname);
                if (!ordersByCusomer.ContainsKey(key))
                {
                    ordersByCusomer.Add(key, new LinkedList<Order>());
                }
                ordersByCusomer[key].Add(order);
            }

            LinkedList<Order> finalList = new LinkedList<Order>();
            foreach (var customerOrders in ordersByCusomer.Values)
            {
                if (customerOrders.Count() == 1)
                {
                    finalList.Add(customerOrders.First());
                }
            }
            return finalList;
        }
    }
}
