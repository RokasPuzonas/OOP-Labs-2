using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LD_24.Code;

namespace LD_24
{
    public partial class Forma1 : System.Web.UI.Page
    {
        private IEnumerable<Tuple<Product, TableRow>> ShowProdcutsTable(Table table, ProductList products, params string[] columns)
        {
            table.Rows.Clear();

            TableRow header = new TableRow();
            foreach (string column in columns)
            {
                header.Cells.Add(new TableCell { Text = column });
            }
            table.Rows.Add(header);

            if (products.Count() > 0) {
                foreach (Product product in products)
                {
                    TableRow row = new TableRow();
                    yield return Tuple.Create(product, row);
                    table.Rows.Add(row);
                }
            } else
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = "Nėra", ColumnSpan = columns.Length });
                table.Rows.Add(row);
            }
        }

        private IEnumerable<Tuple<Order, TableRow>> ShowOrdersTable(Table table, string title, OrderList orders, params string[] columns)
        {
            table.Rows.Clear();

            if (title != null)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = title, ColumnSpan = columns.Length });
                table.Rows.Add(row);
            }

            TableRow header = new TableRow();
            foreach (string column in columns)
            {
                header.Cells.Add(new TableCell { Text = column });
            }
            table.Rows.Add(header);

            if (orders.Count() > 0)
            {
                foreach (Order order in orders)
                {
                    TableRow row = new TableRow();
                    yield return Tuple.Create(order, row);
                    table.Rows.Add(row);
                }
            }
            else
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = "Nėra", ColumnSpan = columns.Length });
                table.Rows.Add(row);
            }
        }

        private IEnumerable<Tuple<Order, TableRow>> ShowOrdersTable(Table table, OrderList orders, params string[] columns)
        {
            return ShowOrdersTable(table, null, orders, columns);
        }

        public void ShowProducts(Table table, ProductList products)
        {
            foreach (var tuple in ShowProdcutsTable(table, products, "ID", "Vardas", "Kaina, eur."))
            {
                Product product = tuple.Item1;
                TableRow row = tuple.Item2;
                row.Cells.Add(new TableCell { Text = product.ID });
                row.Cells.Add(new TableCell { Text = product.Name });
                row.Cells.Add(new TableCell { Text = product.Price.ToString() });
            }
        }

        public void ShowOrders(Table table, OrderList orders)
        {
            foreach (var tuple in ShowOrdersTable(table, orders, "Pavardė", "Vardas", "Įtaisas", "Įtaisų kiekis, vnt."))
            {
                Order order = tuple.Item1;
                TableRow row = tuple.Item2;
                row.Cells.Add(new TableCell { Text = order.CustomerSurname });
                row.Cells.Add(new TableCell { Text = order.CustomerName });
                row.Cells.Add(new TableCell { Text = order.ProductID.ToString() });
                row.Cells.Add(new TableCell { Text = order.ProductAmount.ToString() });
            }
        }

        public void ShowMostPopularProducts(Table table, OrderList orders, ProductList popularProducts)
        {
            foreach (var tuple in ShowProdcutsTable(table, popularProducts, "ID", "Vardas", "Įtaisų kiekis, vnt.", "Įtaisų kaina, eur."))
            {
                Product product = tuple.Item1;
                TableRow row = tuple.Item2;
                int sales = TaskUtils.CountProductSales(orders, product.ID);
                row.Cells.Add(new TableCell { Text = product.ID });
                row.Cells.Add(new TableCell { Text = product.Name });
                row.Cells.Add(new TableCell { Text = sales.ToString() });
                row.Cells.Add(new TableCell { Text = String.Format("{0:f2}", sales * product.Price) });
            }
        }

        public void ShowOrdersByProduct(Control container, OrderList orders, ProductList products)
        {
            container.Controls.Clear();
            foreach (Product product in products)
            {
                OrderList filtered = TaskUtils.FilterByProduct(orders, product.ID);
                filtered = TaskUtils.MergeOrders(filtered);
                filtered.Sort();

                Table table = new Table();
                container.Controls.Add(table);
                foreach (var tuple in ShowOrdersTable(table, product.Name, filtered, "Pavardė", "Vardas", "Įtaisų kiekis, vnt.", "Sumokėta, eur."))
                {
                    Order order = tuple.Item1;
                    TableRow row = tuple.Item2;
                    row.Cells.Add(new TableCell { Text = order.CustomerSurname });
                    row.Cells.Add(new TableCell { Text = order.CustomerName });
                    row.Cells.Add(new TableCell { Text = order.ProductAmount.ToString() });
                    row.Cells.Add(new TableCell { Text = String.Format("{0:f2}", order.ProductAmount * product.Price, 2) });
                }
            }
        }
    }
}