﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Festispec.Model.Repositories
{
    public class QuotationRepository
    {
        public List<Offerte> GetQuotations()
        {
            using(FestispecContext context = new FestispecContext())
            {
                return context.Offerte.Include(o => o.Opdracht.Klant).ToList();
            }
        }

        public List<Offerte> GetLatestQuotationForEachJob(DateTime startDate, DateTime endDate)
        {
            List<Offerte> offertes = new List<Offerte>();

            List<Opdracht> quotations = new List<Opdracht>();
            using(FestispecContext context = new FestispecContext())
            {
                quotations = context.Opdracht.Where(x => x.Status == "Offerte geaccepteerd").ToList();
            }


            foreach (Opdracht job in quotations)
            {
                using (FestispecContext context = new FestispecContext())
                {
                    offertes.Add(context.Offerte.Where(x => x.OpdrachtID == job.OpdrachtID && x.Aanmaakdatum >= startDate && x.Aanmaakdatum <= endDate).OrderByDescending(x => x.Aanmaakdatum).Take(1).FirstOrDefault());
                }
            }

            return offertes;
        }

        public Offerte GetOpdracht(DateTime startDate, DateTime endDate)
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Offerte.Include("Opdracht")
                    .Where(e => e.Aanmaakdatum > startDate && e.Aanmaakdatum < endDate)
                    .OrderByDescending(x => x.Aanmaakdatum)
                    .Take(1).FirstOrDefault();
            }
        }

        public Opdracht GetJob(int jobId)
        {
            using(FestispecContext context = new FestispecContext())
            {
                return context.Opdracht.Find(jobId);
            }
        }
        public void CreateQuotation(Offerte quotation)
        {
            using(FestispecContext context = new FestispecContext())
            {
                Opdracht job = context.Opdracht.Where(j => j.OpdrachtID == quotation.OpdrachtID).FirstOrDefault();
                job.Offerte.Add(quotation);
                context.SaveChanges();
            }
        }
        public void UpdateQuotation(Offerte quotation)
        {
            using(FestispecContext context = new FestispecContext())
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
