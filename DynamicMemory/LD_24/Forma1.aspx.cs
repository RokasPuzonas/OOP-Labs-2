using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LD_24.Code;

namespace LD_24
{
    public partial class Forma1 : System.Web.UI.Page
    {
        private const string targetProductID = "1";
        private const string inputFileA = "App_Data/U24a.txt";
        private const string inputFileB = "App_Data/U24b.txt";
        private const string outputFilename = "App_Data/Rezultatai.txt";

        protected void Page_Load(object sender, EventArgs e)
        {
            FindControl("ResultsDiv").Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(TextBox1.Text);
            decimal k = decimal.Parse(TextBox2.Text);

            FindControl("ResultsDiv").Visible = true;

            ProductList products = InOutUtils.ReadProducts(Server.MapPath(inputFileA));
            OrderList orders = InOutUtils.ReadOrders(Server.MapPath(inputFileB));

            List<string> mostPopularProductIds = TaskUtils.FindMostPopularProducts(orders);
            ProductList mostPopularProducts = TaskUtils.FindByID(products, mostPopularProductIds);
            ProductList filteredProducts = TaskUtils.FilterByQuantitySoldAndPrice(products, orders, n, k);

            ShowProducts(Table1, products);
            ShowOrders(Table2, orders);
            ShowMostPopularProducts(Table5, orders, mostPopularProducts);
            ShowOrdersByProduct(FindControl("OrdersByProductContainer"), orders, products);
            ShowProducts(Table4, filteredProducts);

            using (StreamWriter writer = new StreamWriter(Server.MapPath(outputFilename)))
            {
                InOutUtils.PrintProducts(writer, products, "Įtaisai");
                InOutUtils.PrintOrders(writer, orders, "Pirkėjai");
                InOutUtils.PrintMostPopularProducts(writer, orders, mostPopularProducts, "Populiariausi įtaisai");

                writer.WriteLine("Pirkėjai pagal rūšį:");
                foreach (Product product in products)
                {
                    InOutUtils.PrintOrdersWithPrices(writer, orders, product, product.Name);
                }

                InOutUtils.PrintProducts(writer, filteredProducts, $"Atrinkti įtaisai (n={n}, k={k:f2})");
            }

            /*
            using (StreamWriter writer = new StreamWriter(Server.MapPath(outputFilename)))
            {
                

                writer.Write("Populiariausias produktas: ");
                if (PopularProduct == null)
                {
                    writer.WriteLine("Nėra");
                }
                else
                {
                    writer.WriteLine(PopularProduct.Name);
                    int sales = TaskUtils.CountProductSales(Customers, PopularProduct.ID);
                    writer.WriteLine($"Pardavimų kiekis: {sales} vnt.");
                    writer.WriteLine($"Pardavimų kaina: {sales * PopularProduct.Price:f2} eur.");
                }
                writer.WriteLine();

                InOutUtils.PrintCustomersWithPrices(writer, CustomersByTargetProduct, TargetProduct, $"Pirkėjai pagal rūšį ({TargetProduct.Name})");

                InOutUtils.PrintProducts(writer, filteredProducts, $"Atrinkti įtaisai (n={n}, k={k:f2})");
            }*/
        }
    }
}