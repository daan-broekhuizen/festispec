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
    }
}
