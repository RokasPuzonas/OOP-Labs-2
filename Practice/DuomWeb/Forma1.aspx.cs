using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuomWeb
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow row = new TableRow();

            TableCell title = new TableCell();
            title.Text = "<b>Pavadinimas</b>";
            row.Cells.Add(title);

            TableCell price = new TableCell();
            price.Text = "<b>Kaina, Eur</b>";
            row.Cells.Add(price);

            Table1.Rows.Add(row);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] allLines = File.ReadAllLines(Server.MapPath("App_Data/Prekes.txt"));
            foreach (string line in allLines)
            {
                string[] parts = line.Split(' ');
                TableRow row = new TableRow();

                TableCell title = new TableCell();
                title.Text = parts[0];
                row.Cells.Add(title);

                TableCell price = new TableCell();
                price.Text = parts[1];
                row.Cells.Add(price);

                Table1.Rows.Add(row);
            }
        }
    }
}