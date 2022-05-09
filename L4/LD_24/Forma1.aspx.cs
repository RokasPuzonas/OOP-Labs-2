using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LD_24.Code;
using System.Diagnostics;

namespace LD_24
{
    /// <summary>
    /// Main Form
    /// </summary>
    public partial class Forma1 : System.Web.UI.Page
    {
        private List<List<Actor>> actorss = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Generate initial data files if they don't exist
            string[] actorFiles = { "Actors1.txt", "Actors2.txt", "Actors3.txt" };
            foreach (string name in actorFiles)
            {
                string filename = Server.MapPath($"App_Data/{name}");
                if (!File.Exists(filename))
                {
                    InOutUtils.GenerateFakeActors(filename);
                }
            }

            File.Delete(Server.MapPath("App_Data/Rezultatai.txt"));

            // Read actors from folder
            try
            {
                actorss = InOutUtils.ReadActorsDir(Server.MapPath("App_Data"));
            } catch (Exception ex)
            {
                Label5.Text = ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (actorss == null) return;
            var actors = actorss.SelectMany(x => x).ToList();

            int minHeroIntellect = int.Parse(TextBox1.Text);
            int maxNPCAttack = int.Parse(TextBox2.Text);

            var heatlhyActors = TaskUtils.FilterMostHealthByClass(actors);
            var allClasses = TaskUtils.FindAllClasses(actors);
            var missingActors = TaskUtils.FindMissingActors(actors);

            var intellectHeroes = actors
                .Where(a => a is Hero)
                .Where(a => ((Hero)a).Intellect > minHeroIntellect)
                .OrderBy(a => ((Hero)a).Intellect)
                .ToList();
            var strengthNPCs = actors
                .Where(a => a is NPC)
                .Where(a => a.Attack < maxNPCAttack)
                .OrderBy(a => a.Attack)
                .ToList();
            var team = new List<Actor>();
            team.AddRange(intellectHeroes);
            team.AddRange(strengthNPCs);

            using (var writer = new ResultsWriter(Server.MapPath("App_Data/Rezultatai.txt"), ResultsDiv))
            {
                writer.WriteLine("Pradiniai duomenys:");
                InOutUtils.ShowActors(writer, actorss);

                writer.WriteLine("Daugiausiai gyvybės taškų pagal klases:");
                InOutUtils.ShowHealthyActors(writer, heatlhyActors);

                InOutUtils.PrintClassesCSV(Server.MapPath("App_Data/Klasės.csv"), allClasses);
                writer.WriteLine("Visos klasės:");
                writer.WriteList(allClasses);

                InOutUtils.PrintMissingActors(Server.MapPath("App_Data/Trūkstami.csv"), missingActors);
                writer.WriteLine("Trūkstamos herojų rasės:");
                writer.WriteList(missingActors.Item1);
                writer.WriteLine("Trūkstamos NPC rasės:");
                writer.WriteList(missingActors.Item2);

                InOutUtils.PrintTeam(Server.MapPath("App_Data/Riktine.csv"), team);
                writer.WriteLine("Bendra veikėjų rinktinė:");
                InOutUtils.ShowActors(writer, team);
            }
        }
    }
}