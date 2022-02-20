using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValidWeb
{
    public partial class Forma1 : System.Web.UI.Page
    {

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Label8.Text = "Dalyvių kiekis: 0";
            for (int i = Table1.Rows.Count-1; i > 0; i--)
            {
                Table1.Rows.RemoveAt(i);
            }
        }
    }
}
