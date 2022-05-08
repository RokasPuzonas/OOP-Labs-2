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
        /// <summary>
        /// Show a table to the Web UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T, TableCellCollection>> ShowTable<T>(Table table, IEnumerable<T> list, params string[] columns)
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
                yield return Tuple.Create(item, row.Cells);
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

        /// <summary>
        /// Show a list of actors in a table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="actors"></param>
        public static void ShowActors(Table table, IEnumerable<Actor> actors)
        {
            foreach (var tuple in ShowTable(table, actors, "Rasė", "Miestas", "Vardas", "Klasė", "Gyvybė", "Mana", "Žala", "Šarvai"))
            {
                var actor = tuple.Item1;
                var cells = tuple.Item2;
                cells.Add(new TableCell { Text = actor.Race });
                cells.Add(new TableCell { Text = actor.StartingTown });
                cells.Add(new TableCell { Text = actor.Name });
                cells.Add(new TableCell { Text = actor.Class });
                cells.Add(new TableCell { Text = actor.Health.ToString() });
                cells.Add(new TableCell { Text = actor.Mana.ToString() });
                cells.Add(new TableCell { Text = actor.Attack.ToString() });
                cells.Add(new TableCell { Text = actor.Defense.ToString() });
            }
        }

        /// <summary>
        /// Show a list of healthy actors to a table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="actors"></param>
        public static void ShowHealthyActors(Table table, IEnumerable<Actor> actors)
        {
            foreach (var tuple in ShowTable(table, actors, "Vardas", "Rasė", "Klasė", "Gyvybė"))
            {
                var actor = tuple.Item1;
                var cells = tuple.Item2;
                cells.Add(new TableCell { Text = actor.Name });
                cells.Add(new TableCell { Text = actor.Race });
                cells.Add(new TableCell { Text = actor.Class });
                cells.Add(new TableCell { Text = actor.Health.ToString() });
            }
        }
    }
}