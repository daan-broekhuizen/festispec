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
    }
}
