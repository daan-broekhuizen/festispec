using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Festispec.WebApplication.Models.Repositories
{
    public class InspectionformRepository
    {
        public Inspectieformulier GetInspectionform(int id)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Inspectieformulier form = context.Inspectieformulier
                    .Include(i => i.Vraag.Select(q => q.Antwoorden.Select(a => a.Account)))
                    .Include(i => i.Vraag.Select(q => q.VraagMogelijkAntwoord))
                    .Include(i => i.Opdracht.Klant)
                    .FirstOrDefault(i => i.InspectieformulierID == id);
                return form;
            }
        }

        public List<Inspectieformulier> GetInspectionforms(int userId)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Account account = context.Account.Find(userId);
                return context.Inspectieformulier.Include(a => a.Opdracht.Klant).Include(a => a.Ingepland).Include(a => a.Vraag)
                    .Where(a => a.Ingepland.Any(i => i.AccountID == userId)).ToList();
            }
        }

        public List<Inspectieformulier> GetUpcomingForms(int userId)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Account account = context.Account.Find(userId);
                List<Inspectieformulier> list = context.Inspectieformulier.Include(a => a.Opdracht.Klant).Include(a => a.Ingepland).Include(a => a.Vraag)
                .Where(a => a.Ingepland.Any(i => i.AccountID == userId)).ToList();

                return list.Where(e => e.DatumInspectie >= DateTime.Now.Date).OrderBy(e => e.DatumInspectie).ToList();
            }
        }

        public Vraag GetQuestion(int questionId)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                return context.Vraag.Find(questionId);
            }
        }
        public Antwoorden GetAnswer(Antwoorden answer)
        {
            using(FestiSpecContext context = new FestiSpecContext())
            {
                return context.Antwoorden.Include(a => a.Vraag)
                                         .FirstOrDefault(a => a.VraagID == answer.VraagID && 
                                                         a.InspecteurID == answer.InspecteurID &&
                                                         a.AntwoordNummer == answer.AntwoordNummer);
            }
        }

        public Antwoorden CreateAnswer(Antwoorden answer)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.Antwoorden.Add(answer);
                context.SaveChanges();
                return answer;
            }
        }

        public Antwoorden UpdateAnswer(Antwoorden answer)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Antwoorden toUpdate = context.Antwoorden.FirstOrDefault(a => a.VraagID == answer.VraagID &&
                                                         a.InspecteurID == answer.InspecteurID &&
                                                         a.AntwoordNummer == answer.AntwoordNummer);
                context.Entry(toUpdate).CurrentValues.SetValues(answer);
                context.SaveChanges();
                return toUpdate;
            }
        }

    }
}