using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public static class TaskUtils
    {
        public static List<string> FindMostPopularProducts(OrderList orders)
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

            List<string> mostPopularProducts = new List<string>();
            int mostPopularCount = 0;
            foreach (string product in productSales.Keys)
            {
                int count = productSales[product];
                if (count > mostPopularCount)
                {
                    mostPopularCount = count;
                    mostPopularProducts = new List<string> { product };
                } else if (count == mostPopularCount)
                {
                    mostPopularProducts.Add(product);
                }
            }

            return mostPopularProducts;
        }

        public static int CountProductSales(OrderList orders, string product)
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

        public static OrderList FilterByProduct(OrderList orders, string product)
        {
            OrderList filtered = new OrderList();
            foreach (Order order in orders)
            {
                if (order.ProductID == product)
                {
                    filtered.AddToEnd(order);
                }
            }
            return filtered;
        }

        public static OrderList MergeOrders(OrderList orders)
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

            OrderList mergedOrders = new OrderList();
            foreach (var order in ordersByName.Values)
            {
                mergedOrders.AddToEnd(order);
            }
            return mergedOrders;
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

        public static ProductList FindByID(ProductList products, List<string> ids)
        {
            ProductList foundProducts = new ProductList();
            foreach (string id in ids)
            {
                foundProducts.AddToEnd(FindByID(products, id));
            }
            return foundProducts;
        }

        public static ProductList FilterByQuantitySoldAndPrice(ProductList products, OrderList customers, int minSold, decimal maxPrice)
        {
            ProductList filtered = new ProductList();
            foreach (Product product in products)
            {
                if (product.Price < maxPrice)
                {
                    int sold = CountProductSales(customers, product.ID);
                    if (sold >= minSold)
                    {
                        filtered.AddToEnd(product);
                    }
                }
            }
            return filtered;
        }

        public static OrderList FindCustomerWithSingleProduct(OrderList orders)
        {
            Dictionary<Tuple<string, string>, OrderList> ordersByCusomer = new Dictionary<Tuple<string, string>, OrderList>(); 
            foreach (var order in TaskUtils.MergeOrders(orders))
            {
                var key = Tuple.Create(order.CustomerName, order.CustomerSurname);
                if (!ordersByCusomer.ContainsKey(key))
                {
                    ordersByCusomer.Add(key, new OrderList());
                }
                ordersByCusomer[key].AddToEnd(order);
            }

            OrderList finalList = new OrderList();
            foreach (var customerOrders in ordersByCusomer.Values)
            {
                if (customerOrders.Count() == 1)
                {
                    finalList.AddToEnd(customerOrders.First());
                }
            }
            return finalList;
        }

    }
}
