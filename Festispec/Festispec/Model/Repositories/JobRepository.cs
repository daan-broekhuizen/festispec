using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Model;
using System.Data.Entity;

namespace FestiSpec.Domain.Repositories
{
    public class JobRepository
    {
        public List<Opdracht> GetOpdrachten()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.Include(c => c.Klant).Include(c => c.Offerte).ToList();
            }
        }

        public List<Opdracht> GetOpdrachtenWithQuotations()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.Include("Offerte").ToList();
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
                Opdracht toUpdate = context.Opdracht.Where(c => c.OpdrachtID == opdracht.OpdrachtID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(opdracht);
                context.SaveChanges();
            }
        }

        public Opdracht GetSingleJob(int ID)
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht
                    .Include("Klant")
                    .Include("StatusLookup")
                    .Where(c => c.OpdrachtID == ID).FirstOrDefault();
            }

        }
    }
}
