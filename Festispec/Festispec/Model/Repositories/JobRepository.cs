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

        public void CreateJob(Opdracht opdracht)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Opdracht.Add(opdracht);
                context.SaveChanges();
            }
        }

        public void UpdateJob(Opdracht opdracht)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Opdracht toUpdate = context.Opdracht.Where(c => c.OpdrachtNaam == opdracht.OpdrachtNaam).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(opdracht);
                context.SaveChanges();
            }
        }
    }
}
