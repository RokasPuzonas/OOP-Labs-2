using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LD_24.Code;

namespace LD_24
{
    /// <summary>
    /// Main form
    /// </summary>
    public partial class Forma1 : System.Web.UI.Page
    {
        private const string outputFilename = "App_Data/Rezultatai.txt";

        /// <summary>
        /// Function that runs when the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            FindControl("ResultsDiv").Visible = false;
        }

        /// <summary>
        /// Functions that rusn when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(TextBox1.Text);
            decimal k = decimal.Parse(TextBox2.Text);

            FindControl("ResultsDiv").Visible = true;

            var products = InOutUtils.ReadProducts(FileUpload1.FileContent);
            var orders = InOutUtils.ReadOrders(FileUpload2.FileContent);

            var mostPopularProductIds = TaskUtils.FindMostPopularProducts(orders);
            var mostPopularProducts = TaskUtils.FindByID(products, mostPopularProductIds);
            var filteredProducts = TaskUtils.FilterByQuantitySoldAndPrice(products, orders, n, k);
            var customersWithSingleProduct = TaskUtils.FindCustomerWithSingleProduct(orders);
            customersWithSingleProduct.Sort();

            ShowProducts(Table1, products);
            ShowOrders(Table2, orders);
            ShowMostPopularProducts(Table5, orders, mostPopularProducts);
            ShowOrdersWithPrices(Table3, customersWithSingleProduct, products);
            ShowProducts(Table4, filteredProducts);

            using (StreamWriter writer = new StreamWriter(Server.MapPath(outputFilename)))
            {
                InOutUtils.PrintProducts(writer, products, "Įtaisai");
                InOutUtils.PrintOrders(writer, orders, "Pirkėjai");
                InOutUtils.PrintMostPopularProducts(writer, orders, mostPopularProducts, "Populiariausi įtaisai");
                InOutUtils.PrintOrdersWithPrices(writer, customersWithSingleProduct, products, "Vienos rūšies pirkėjai");
                InOutUtils.PrintProducts(writer, filteredProducts, $"Atrinkti įtaisai (n={n}, k={k:f2})");
            }
        }

    }
}