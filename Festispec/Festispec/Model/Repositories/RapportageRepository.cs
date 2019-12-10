using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}
