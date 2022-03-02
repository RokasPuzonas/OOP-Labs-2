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
        public void ShowProducts(Table table, ProductList products)
        {
            table.Rows.Clear();

            TableRow header = new TableRow();
            header.Cells.Add(new TableCell { Text = "ID" });
            header.Cells.Add(new TableCell { Text = "Vardas" });
            header.Cells.Add(new TableCell { Text = "Kaina, eur." });
            table.Rows.Add(header);

            foreach (Product product in products)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = product.ID });
                row.Cells.Add(new TableCell { Text = product.Name });
                row.Cells.Add(new TableCell { Text = product.Price.ToString() });
                table.Rows.Add(row);
            }
        }

        public void ShowCustomers(Table table, CustomerList customers)
        {
            table.Rows.Clear();

            TableRow header = new TableRow();
            header.Cells.Add(new TableCell { Text = "Pavardė" });
            header.Cells.Add(new TableCell { Text = "Vardas" });
            header.Cells.Add(new TableCell { Text = "Įtaisas" });
            header.Cells.Add(new TableCell { Text = "Įtaiso kiekis, vnt." });
            table.Rows.Add(header);

            foreach (Customer customer in customers)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = customer.Surname });
                row.Cells.Add(new TableCell { Text = customer.Name });
                row.Cells.Add(new TableCell { Text = customer.ProductID.ToString() });
                row.Cells.Add(new TableCell { Text = customer.ProductAmount.ToString() });
                table.Rows.Add(row);
            }
        }

        public void ShowMostPopularProduct(Label label, CustomerList customers, Product product)
        {
            label.Text = "Populiariausias produktas: ";
            if (product == null)
            {
                label.Text = "Nėra";
                return;
            }

            label.Text += $"{product.Name}<br />";

            int sales = TaskUtils.CountProductSales(customers, product.ID);
            label.Text += $"Pardavimų kiekis: {sales} vnt.<br />";
            label.Text += $"Pardavimų kaina: {sales*product.Price:f2} eur.";
        }

        public void ShowCustomersByProduct(Table table, CustomerList customers, Product product)
        {
            table.Rows.Clear();

            TableRow header = new TableRow();
            header.Cells.Add(new TableCell { Text = "Pavardė" });
            header.Cells.Add(new TableCell { Text = "Vardas" });
            header.Cells.Add(new TableCell { Text = "Įtaisas kiekis, vnt." });
            header.Cells.Add(new TableCell { Text = "Kaina, eur." });
            table.Rows.Add(header);

            foreach (Customer customer in customers)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = customer.Surname });
                row.Cells.Add(new TableCell { Text = customer.Name });
                row.Cells.Add(new TableCell { Text = customer.ProductAmount.ToString() });
                row.Cells.Add(new TableCell { Text = (Math.Round(customer.ProductAmount*product.Price, 2)).ToString() });
                table.Rows.Add(row);
            }
        }
    }
}