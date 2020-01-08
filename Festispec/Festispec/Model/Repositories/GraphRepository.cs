using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class GraphRepository
    {
        public List<Offerte> GetOffertes()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Offerte.ToList();
            }
        }

        public List<Opdracht> GetJobs()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.ToList();
            }
        }

        public int GetOffertesRejected()
        {
            int amountOfOffertes = GetOffertes().Count();
            int amountOfJobs = GetOffertes().Select(e => e.OpdrachtID).Distinct().Count();
            return amountOfOffertes - amountOfJobs;

        }
    }
}
