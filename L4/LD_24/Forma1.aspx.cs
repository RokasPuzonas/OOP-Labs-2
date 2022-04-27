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
    public partial class Forma1 : System.Web.UI.Page
    {
        private List<Actor> actors = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] actorFiles = { "Actors1.txt", "Actors2.txt", "Actors3.txt" };
            foreach (string name in actorFiles)
            {
                string filename = Server.MapPath($"App_Data/{name}");
                if (!File.Exists(filename))
                {
                    InOutUtils.GenerateFakeActors(filename);
                }
            }

            try
            {
                actors = InOutUtils.ReadActorsDir(Server.MapPath("App_Data"));
            } catch (Exception ex)
            {
                Debug.WriteLine("Oops an error occured:");
                Debug.WriteLine(ex);
                throw ex;
            }

            Label1.Visible = false;
            Label2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Visible = true;
            Label2.Visible = true;

            int minHeroIntellect = int.Parse(TextBox1.Text);
            int maxNPCAttack = int.Parse(TextBox2.Text);

            ShowActors(Table1, actors);

            var heatlhyActors = TaskUtils.FilterMostHealthByClass(actors);
            ShowHealthyActors(Table2, heatlhyActors);

            var allClasses = TaskUtils.FindAllClasses(actors);
            InOutUtils.PrintClassesCSV(Server.MapPath("App_Data/Klasės.csv"), allClasses);

            var missingActors = TaskUtils.FindMissingActors(actors);
            InOutUtils.PrintMissingActors(Server.MapPath("App_Data/Trūkstami.csv"), missingActors);

            var intellectHeroes = TaskUtils.FilterHeroesByIntellect(actors, minHeroIntellect);
            var strengthNPCs = TaskUtils.FilterNPCsByAttack(actors, maxNPCAttack);
            intellectHeroes.Sort();
            strengthNPCs.Sort();
            var team = new List<Actor>();
            team.AddRange(intellectHeroes);
            team.AddRange(strengthNPCs);
            InOutUtils.PrintTeam(Server.MapPath("App_Data/Riktine.csv"), team);
        }
    }
}