using Festispec.ViewModel.TemplateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class TemplateRepository
    {
        public List<TemplateViewModel> GetRapportTemplates()
        {
            List<TemplateViewModel> templates = new List<TemplateViewModel>();

            using (FestispecContext context = new FestispecContext())
            {
                foreach(RapportTemplate template in context.RapportTemplate)
                {
                    templates.Add(new TemplateViewModel(template));
                }
            }

            return templates;
        }

        public RapportTemplate GetRapportTemplate(int templateID)
        {
            RapportTemplate template = null;

            using(FestispecContext context = new FestispecContext())
            {
                template = context.RapportTemplate.Where(x => x.TemplateID == templateID).FirstOrDefault();
            }

            return template;
        }

        public List<TemplateViewModel> GetInspectieformulierTemplates()
        {
            List<TemplateViewModel> templates = new List<TemplateViewModel>();

            using (FestispecContext context = new FestispecContext())
            {
                foreach (Inspectieformulier template in context.Inspectieformulier.Where(x => x.OpdrachtID == null))
                {
                    templates.Add(new TemplateViewModel(template));
                }
            }

            return templates;
        }

        public Inspectieformulier GetInspectionFormTemplate(int templateID)
        {
            Inspectieformulier template = null;

            using (FestispecContext context = new FestispecContext())
            {
                template = context.Inspectieformulier.Include("Vraag").Include("Vraag.VraagMogelijkAntwoord").Where(x => x.InspectieformulierID == templateID).FirstOrDefault();
            }

            return template;
        }

    }
}
