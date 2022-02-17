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

        protected void Page_Load(object sender, EventArgs e)
        {
            inputFilename = Server.MapPath(@"App_Data/U3.txt");
            outputFilename = Server.MapPath(@"App_Data/Rezultatai.txt");

            map = Code.InOutUtils.ReadMap(inputFilename);
            ShowMap(Table1, map);
        }
        
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

        private void ShowFriends(BulletedList list, List<Code.Point> friends)
        {
            list.Items.Clear();
            foreach (var friend in friends)
            {
                list.Items.Add(String.Format("X = {0}, Y = {1}", friend.X + 1, friend.Y + 1));
            }
        }

        private void ShowBestPizzeriaResult(Label label, Code.BestPizzeriaResult result)
        {
            if (result == null)
            {
                label.Text = "Neįmanoma";
                return;
            }

            label.Text = String.Format("Susitikimo vieta (X = {0}, Y = {1})", result.MeetingSpot.X + 1, result.MeetingSpot.Y + 1);
            label.Text += "<br />";
            label.Text += String.Format("Picerija (X = {0}, Y = {1})", result.Pizzeria.X + 1, result.Pizzeria.Y + 1);
            label.Text += "<br />";
            label.Text += String.Format("Nueita {0}", result.Cost);
        }

        private void ShowMap(Table table, Code.Map map)
        {
            table.Rows.Clear();
            for (int y = 0; y < map.Height; y++)
            {
                TableRow row = new TableRow();
                for (int x = 0; x < map.Width; x++)
                {
                    TableCell cell = new TableCell();
                    cell.Width = 20;
                    cell.Height = 20;
                    switch (map.Get(x, y))
                    {
                        case Code.MapTile.Empty:
                            cell.Text = ".";
                            break;
                        case Code.MapTile.Pizzeria:
                            cell.Text = "P";
                            break;
                        case Code.MapTile.Friend:
                            cell.Text = "D";
                            break;
                        case Code.MapTile.MeetingSpot:
                            cell.Text = "S";
                            break;
                        case Code.MapTile.Wall:
                            cell.Text = "X";
                            break;
                        default:
                            cell.Text = "?";
                            break;
                    }
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
        }
    }
}