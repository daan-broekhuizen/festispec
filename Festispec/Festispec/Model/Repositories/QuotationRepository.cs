using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class QuotationRepository
    {
        public List<Offerte> GetQuotations()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Offerte.Include("Opdracht.Klant").ToList();
            }
        }
        public Opdracht GetJob(int jobId)
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.Find(jobId);
            }
        }
        public void CreateQuotation(Offerte quotation)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Opdracht job = context.Opdracht.Where(j => j.OpdrachtID == quotation.OpdrachtID).FirstOrDefault();
                job.Offerte.Add(quotation);
                context.SaveChanges();
            }
        }
        public void UpdateQuotation(Offerte quotation)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Offerte toUpdate = context.Offerte.Where(q => q.OfferteID == quotation.OfferteID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(quotation);
                context.SaveChanges();
            }
        }
        public void UpdateDecision(int quotationId, string decision)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Offerte toUpdate = context.Offerte.Where(q => q.OfferteID == quotationId).FirstOrDefault();
                toUpdate.KlantbeslissingReden = decision;
                context.SaveChanges();
            }
        }
        public void UpdateJobStatus(int jobId, string status)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Opdracht toUpdate = context.Opdracht.Where(j => j.OpdrachtID == jobId).FirstOrDefault();
                toUpdate.Status = context.Status.First(s => s.Betekenis == status).Betekenis;
                context.SaveChanges();
            }
        }
    }
}