using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class TemplateRepository
    {
        public List<RapportTemplate> GetRapportTemplates()
        {
            List<RapportTemplate> templates = new List<RapportTemplate>();

            using (FestispecContext context = new FestispecContext())
            {
                templates = context.RapportTemplate.ToList();
            }

            return templates;
        }

        public List<Inspectieformulier> GetInspectieformulierTemplates()
        {
            List<Inspectieformulier> templates = new List<Inspectieformulier>();

            using (FestispecContext context = new FestispecContext())
            {
                templates = context.Inspectieformulier.Where(x => x.OpdrachtID == 0).ToList();
            }

            return templates;
        }
    }
}
