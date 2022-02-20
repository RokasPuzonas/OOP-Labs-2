using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LD_24
{

    public partial class Forma1 : System.Web.UI.Page
    {
        private string inputFilename;
        private string outputFilename;
        private Code.Map map;

        /// <summary>
        /// Load initial map and show it in the interface
        /// </summary>
        /// <param name="sender">Sender element</param>
        /// <param name="e">Event</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            inputFilename = Server.MapPath(@"App_Data/U3.txt");
            outputFilename = Server.MapPath(@"App_Data/Rezultatai.txt");

            map = Code.InOutUtils.ReadMap(inputFilename);
            ShowMap(Table1, map);
        }

        /// <summary>
        /// Find which is the best pizzeria, show it in the interface and output the result
        /// to a file
        /// </summary>
        /// <param name="sender">Sender element</param>
        /// <param name="e">Event</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Code.Point> friends = map.FindAll(Code.MapTile.Friend);
            Code.BestPizzeriaResult result = Code.TaskUtils.FindBestPizzeria(map);

            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            BulletedList1.Visible = true;
            ShowFriends(BulletedList1, friends);
            ShowBestPizzeriaResult(Label3, result);

            using (StreamWriter writer = new StreamWriter(outputFilename))
            {
                Code.InOutUtils.WriteMap(writer, map);
                writer.Write('\n');
                Code.InOutUtils.WriteFriendPositions(writer, friends);
                Code.InOutUtils.WriteBestPizzeriaResult(writer, result);
            }
        }
    }
}
