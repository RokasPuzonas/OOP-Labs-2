using System;
using System.Collections.Generic;
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
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<List<Subscriber>> Subscribers;
        List<Publication> Publications;

        protected void Page_Load(object sender, EventArgs e)
        {
            //InOutUtils.GenerateInitialData(3, 5, Server.MapPath("App_Data"));

            try
            {
                Subscribers = InOutUtils.ReadSubscribersFromDir(Server.MapPath("App_Data"), "Subscribers*.txt");
                Publications = InOutUtils.ReadPublications(Server.MapPath("App_Data/Publications.txt"));
            } catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Publications == null || Subscribers == null) return;

            int month = int.Parse(TextBox1.Text);

            var subscribers = Subscribers.SelectMany(x => x).ToList();
            var incomes = subscribers
                .Where(s => s.SubscriptionStart <= month && month < s.SubscriptionStart + s.SubscriptionLength)
                .Join(Publications, s => s.SubscriptionID, p => p.ID, (s, p) => new {
                    p.Publisher,
                    Income = p.PricePerMonth * s.SubscriptionLength
                }).GroupBy(x => x.Publisher)
                .Select(x => new {
                    Publisher = x.Key,
                    Income = x.Sum(s => s.Income)
                }).ToDictionary(x => x.Publisher, x => x.Income);

            using (var writer = new ResultsWriter(Server.MapPath("App_Data/Rezultatai.txt"), ResultsDiv))
            {
                writer.WriteLine("Prenumeratoriai:");
                InOutUtils.PrintManySubscribers(writer, Subscribers);

                writer.WriteLine("Leidiniai:");
                InOutUtils.PrintPublications(writer, Publications);

                writer.WriteLine("Pajamos:");
                foreach (var tuple in writer.WriteTable(incomes, "Leidėjas", "Pajamos"))
                {
                    var publisher = tuple.Item1.Key;
                    var income = tuple.Item1.Value;
                    var row = tuple.Item2;
                    row.Add(publisher);
                    row.Add(Math.Round(income, 2));
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Publications == null || Subscribers == null) return;

            var subscribers = Subscribers.SelectMany(x => x).ToList();
            var incomes = subscribers
                .Join(Publications, s => s.SubscriptionID, p => p.ID, (s, p) => new {
                    p.Publisher,
                    Publication = p.ID,
                    Income = p.PricePerMonth * s.SubscriptionLength
                }).GroupBy(x => x.Publisher)
                .Select(x => new {
                    Publisher = x.Key,
                    Income = x.Sum(s => s.Income),
                    Publications = x.GroupBy(p => p.Publication).Select(b => new {
                        ID = b.Key,
                        Income = b.Sum(c => c.Income)
                    }).ToDictionary(b => b.ID, b => b.Income)
                }).OrderBy(x => x.Income)
                .ThenBy(x => x.Publisher)
                .Select(x => Tuple.Create(x.Publisher, x.Income, x.Publications))
                .ToList();

            using (var writer = new ResultsWriter(Server.MapPath("App_Data/Rezultatai.txt"), ResultsDiv))
            {
                writer.WriteLine("Prenumeratoriai:");
                InOutUtils.PrintManySubscribers(writer, Subscribers);

                writer.WriteLine("Leidiniai:");
                InOutUtils.PrintPublications(writer, Publications);

                writer.WriteLine("Pajamos:");
                foreach (var tuple in writer.WriteTable(incomes, "Leidėjas", "Pajamos", "Pajamos pagal leidinius"))
                {
                    var publisher = tuple.Item1.Item1;
                    var income = tuple.Item1.Item2;
                    var publications = tuple.Item1.Item3;
                    var row = tuple.Item2;
                    row.Add(publisher);
                    row.Add(Math.Round(income, 2));
                    row.Add(string.Join(" / ", publications.Select(p => string.Format("{0}: {1:0.00}", p.Key, p.Value))));
                }
            }
        }
    }
}