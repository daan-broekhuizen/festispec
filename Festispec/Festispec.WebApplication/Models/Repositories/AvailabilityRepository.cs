using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.Repositories
{
    public class AvailabilityRepository
    {
        public List<BeschikbaarheidInspecteurs> GetAvailability(int id)
        {
            using(FestiSpecContext context = new FestiSpecContext())
            {
                return context.BeschikbaarheidInspecteurs.Where(c => c.MedewerkerID == id).ToList();
            }
        }

        public void CreateAvailability(BeschikbaarheidInspecteurs beschikbaarheid)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.BeschikbaarheidInspecteurs.Add(beschikbaarheid);
                context.SaveChanges();
            }
        }

        public void DeleteAvailability(BeschikbaarheidInspecteurs beschikbaarheid)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.BeschikbaarheidInspecteurs.Remove(beschikbaarheid);
                context.SaveChanges();
            }
        }

    }
}