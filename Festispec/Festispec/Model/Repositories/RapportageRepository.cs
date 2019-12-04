using Festispec.ViewModel;
using System;
using System.Collections.Generic;
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

        public void UpdateRapportage(JobViewModel job)
        {
            using(FestispecContext context = new FestispecContext())
            {
                context.Opdracht.Where(x => x.OpdrachtID == job.JobID).FirstOrDefault().Rapportage = job.Report;
                context.SaveChanges();
            }
        }

        public Opdracht GetOpdracht(JobViewModel job)
        {
            Opdracht opdracht = null;
            
            using(FestispecContext context = new FestispecContext())
            {
                opdracht = context.Opdracht.Where(x => x.OpdrachtID == job.JobID).FirstOrDefault();
            }

            return opdracht;
        }
    }
}
