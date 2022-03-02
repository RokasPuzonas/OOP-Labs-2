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

        private ProductList Products;
        private CustomerList Customers;

        private Product PopularProduct;
        private Product TargetProduct;
        private CustomerList CustomersByTargetProduct;

        protected void Page_Load(object sender, EventArgs e)
        {
            Products = InOutUtils.ReadProducts(Server.MapPath(inputFileA));
            Customers = InOutUtils.ReadCustomers(Server.MapPath(inputFileB));
            ShowProducts(Table1, Products);
            ShowCustomers(Table2, Customers);

            string popularProductID = TaskUtils.FindMostPopularProduct(Customers);
            PopularProduct = null;
            if (popularProductID != null)
            {
                PopularProduct = TaskUtils.FindByID(Products, popularProductID);
            }

            TargetProduct = TaskUtils.FindByID(Products, targetProductID);
            CustomersByTargetProduct = TaskUtils.FilterByProduct(Customers, targetProductID);
            CustomersByTargetProduct.Sort();

            ShowMostPopularProduct(Label3, Customers, PopularProduct);

            Label4.Text = $"Pirkėjai pagal rūšį ({TargetProduct.Name}):";
            ShowCustomersByProduct(Table3, CustomersByTargetProduct, TargetProduct);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(TextBox1.Text);
            decimal k = decimal.Parse(TextBox2.Text);
            ProductList filteredProducts = TaskUtils.FilterByMinQuantitySold(TaskUtils.FilterByMaxPrice(Products, k), Customers, n);

            ShowProducts(Table4, filteredProducts);

            using (StreamWriter writer = new StreamWriter(Server.MapPath(outputFilename)))
            {
                InOutUtils.PrintProducts(writer, Products, "Įtaisai");
                InOutUtils.PrintCustomers(writer, Customers, "Pirkėjai");

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
            }
        }
    }
}