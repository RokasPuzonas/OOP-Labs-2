using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LD_24.Code;

namespace LD_24
{
    public partial class Forma1 : System.Web.UI.Page
    {
        public static IEnumerable<Tuple<T, TableRow>> ShowTable<T>(Table table, IEnumerable<T> list, params string[] columns)
        {
            TableHeaderRow header = new TableHeaderRow();
            foreach (string column in columns)
            {
                header.Cells.Add(new TableHeaderCell { Text = column });
            }
            table.Rows.Add(header);

            bool noRows = true;
            foreach (T item in list)
            {
                TableRow row = new TableRow();
                yield return Tuple.Create(item, row);
                table.Rows.Add(row);
                noRows = false;
            }

            if (noRows)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = "Nėra", ColumnSpan = columns.Length });
                table.Rows.Add(row);
            }
        }

        public static void ShowActors(Table table, IEnumerable<Actor> actors)
        {
            foreach (var tuple in ShowTable(table, actors, "Rasė", "Miestas", "Vardas", "Klasė", "Gyvybė", "Mana", "Žala", "Šarvai"))
            {
                Actor actor = tuple.Item1;
                TableRow row = tuple.Item2;
                row.Cells.Add(new TableCell { Text = actor.Race });
                row.Cells.Add(new TableCell { Text = actor.StartingTown });
                row.Cells.Add(new TableCell { Text = actor.Name });
                row.Cells.Add(new TableCell { Text = actor.Class });
                row.Cells.Add(new TableCell { Text = actor.Health.ToString() });
                row.Cells.Add(new TableCell { Text = actor.Mana.ToString() });
                row.Cells.Add(new TableCell { Text = actor.Attack.ToString() });
                row.Cells.Add(new TableCell { Text = actor.Defense.ToString() });
            }
        }

        public static void ShowHealthyActors(Table table, IEnumerable<Actor> actors)
        {
            foreach (var tuple in ShowTable(table, actors, "Vardas", "Rasė", "Klasė", "Gyvybė"))
            {
                Actor actor = tuple.Item1;
                TableRow row = tuple.Item2;
                row.Cells.Add(new TableCell { Text = actor.Name });
                row.Cells.Add(new TableCell { Text = actor.Race });
                row.Cells.Add(new TableCell { Text = actor.Class });
                row.Cells.Add(new TableCell { Text = actor.Health.ToString() });
            }
        }
    }
}