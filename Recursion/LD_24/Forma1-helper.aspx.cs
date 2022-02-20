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
        /// <summary>
        /// Put target friends into a bulleted list element
        /// </summary>
        /// <param name="list">Target list</param>
        /// <param name="friends">Target friends</param>
        private void ShowFriends(BulletedList list, List<Code.Point> friends)
        {
            list.Items.Clear();
            foreach (var friend in friends)
            {
                list.Items.Add(String.Format("X = {0}, Y = {1}", friend.X + 1, friend.Y + 1));
            }
        }

        /// <summary>
        /// Put best pizzeria result into a label
        /// </summary>
        /// <param name="label">Target label</param>
        /// <param name="result">Target result</param>
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

        /// <summary>
        /// Put a maps tiles into a table
        /// </summary>
        /// <param name="table">Target table</param>
        /// <param name="map">Target map</param>
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
