using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.Repositories
{
    public class AvailabilityRepository
    {
        public List<Beschikbaarheid_inspecteurs> GetAvailability(int id)
        {
            using(FestiSpecContext context = new FestiSpecContext())
            {
                return context.Beschikbaarheid_inspecteurs.Where(c => c.MedewerkerID == id).ToList();
            }
        }

        public void CreateAvailability(Beschikbaarheid_inspecteurs beschikbaarheid)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.Beschikbaarheid_inspecteurs.Add(beschikbaarheid);
                context.SaveChanges();
            }
        }

        public void DeleteAvailability(Beschikbaarheid_inspecteurs beschikbaarheid)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.Beschikbaarheid_inspecteurs.Remove(beschikbaarheid);
                context.SaveChanges();
            }
        }

    }
}