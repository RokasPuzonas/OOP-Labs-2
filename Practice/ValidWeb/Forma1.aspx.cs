using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValidWeb
{
    public partial class Forma1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("-");
                for (int i = 14; i <= 25; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
            }

            TableRow header = new TableRow();

            header.Cells.Add(new TableCell { Text = "<b>Vardas</b>" });
            header.Cells.Add(new TableCell { Text = "<b>Pavardė</b>" });
            header.Cells.Add(new TableCell { Text = "<b>Mokykla</b>" });
            header.Cells.Add(new TableCell { Text = "<b>Amžius</b>" });
            header.Cells.Add(new TableCell { Text = "<b>Programavimo kalba</b>" });

            Table1.Rows.Add(header);

            if (Session["users"] != null)
            {
                string[] users = ((string)Session["users"]).Split(';');
                foreach (string user in users)
                {
                    string[] parts = user.Split('|');
                    AddUserToTable(parts[0], parts[1], parts[2], parts[3], parts[4]);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text;
            if (Regex.IsMatch(name, @"[^a-zA-Z]")) { return; }

            string surname = TextBox2.Text;
            if (Regex.IsMatch(surname, @"[^a-zA-Z]")) { return; }

            string school = TextBox3.Text;
            string age = DropDownList1.Text;
            string language = CheckBoxList1.SelectedValue;
            
            string user = String.Join("|", name, surname, school, age, language);
            if (Session["users"] == null) {
                Session["users"] = user;
            } else {
                Session["users"] += ";" + user;
            }
            AddUserToTable(name, surname, school, age, language);
        }

        void AddUserToTable(string name, string surname, string school, string age, string language)
        {
            TableRow row = new TableRow();

            row.Cells.Add(new TableCell { Text = name });
            row.Cells.Add(new TableCell { Text = surname });
            row.Cells.Add(new TableCell { Text = school });
            row.Cells.Add(new TableCell { Text = age });
            row.Cells.Add(new TableCell { Text = language });

            Table1.Rows.Add(row);
        }

        
    }
}
