using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Model;

namespace FestiSpec.Domain.Repositories
{
    public class JobRepository
    {
        public List<Opdracht> GetOpdrachten()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.Include("Klant").ToList();
            }
        }
    }
}
