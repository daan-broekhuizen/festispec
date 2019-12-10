using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class RapportageRepository
    {
        public void CreateTemplate(RapportTemplate template)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.RapportTemplate.Add(template);
                context.SaveChanges();
            }
        }

        public void UpdateTemplate(RapportTemplate template)
        {
            using(FestispecContext context = new FestispecContext())
            {
                context.RapportTemplate.Where(x => x.TemplateID == template.TemplateID).FirstOrDefault().TemplateText = template.TemplateText;
                context.SaveChanges();
            }
        }

        public RapportTemplate GetTemplate(int id)
        {
            RapportTemplate template = null;

            using (FestispecContext context = new FestispecContext())
            {
                template = context.RapportTemplate.Where(x => x.TemplateID == id).FirstOrDefault();
            }

            return template;
        }

        public void UpdateRapportage(int jobID, string report)
        {
            using(FestispecContext context = new FestispecContext())
            {
                context.Opdracht.Where(x => x.OpdrachtID == jobID).FirstOrDefault().Rapportage = report;
                context.SaveChanges();
            }
        }

        public Opdracht GetOpdracht(int jobID)
        {
            Opdracht opdracht = null;
            
            using(FestispecContext context = new FestispecContext())
            {
                opdracht = context.Opdracht
                    .Include(o => o.Inspectieformulier.Select(i => i.Vraag.Select(v => v.Antwoorden)))
                    .Where(o => o.OpdrachtID == jobID)
                    .FirstOrDefault();
            }

            return opdracht;
        }

        public VraagType GetQuestionType(string id)
        {
            VraagType type = null;

            using(FestispecContext context = new FestispecContext())
            {
                type = context.VraagType.Where(x => x.Afkorting == id).FirstOrDefault();
            }

            return type;
        }

        public List<ChartData> GetChartData(int questionID)
        {
            List<ChartData> chartData = new List<ChartData>();

            using(FestispecContext context = new FestispecContext())
            {
                chartData = context.Database.SqlQuery<ChartData>("exec sp_GetChartData @VraagID ", new SqlParameter("VraagID", questionID)).ToList();
            }

            return chartData;
        }

        public List<Account> GetInspectorsWithFilledAnswers()
        {
            List<Account> accounts = new List<Account>();

            using (FestispecContext context = new FestispecContext())
            {
                accounts = context.Antwoorden.Select(x => x.Account).Distinct().ToList();
            }

            return accounts;
        }

        public List<Vraag> GetQuestionsFromInspector(int inspectorID, int jobID)
        {
            List<Vraag> filteredQuestions = new List<Vraag>();

            List<Vraag> unfilteredQuestions = new List<Vraag>();

            using(FestispecContext context = new FestispecContext())
            {
                unfilteredQuestions = context.Antwoorden.Where(x => x.InspecteurID == inspectorID).Select(x => x.Vraag).Include(x => x.Antwoorden).ToList();
            }

            foreach (Vraag vraag in unfilteredQuestions)
            {
                if (filteredQuestions.Count(x => x.VraagID == vraag.VraagID) == 0)
                {
                    using(FestispecContext context = new FestispecContext())
                    {
                        if (context.Inspectieformulier.Where(x => x.InspectieformulierID == vraag.InspectieFormulierID && x.OpdrachtID == jobID).Count() > 0)
                            filteredQuestions.Add(vraag);
                    }
                }
            }

            return filteredQuestions;
        }
    }
}
