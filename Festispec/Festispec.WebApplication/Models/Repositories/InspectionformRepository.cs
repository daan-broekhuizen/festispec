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
                return context.Account.Include(a => a.Ingepland)
                    .FirstOrDefault(a => a.AccountID == userId).Ingepland.ToList();
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
                Vraag question = context.Vraag.Include(q => q.Antwoorden)
                    .FirstOrDefault(q => q.VraagID == answer.VraagID);
                question.Antwoorden.Add(answer);
                context.SaveChanges();
                return answer;
            }
        }

        public Antwoorden UpdateAnswer(Antwoorden answer)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Antwoorden toUpdate = context.Antwoorden.Find(answer.VraagID, answer.InspecteurID, answer.AntwoordNummer);
                context.Entry(toUpdate).CurrentValues.SetValues(answer);
                context.SaveChanges();
                return toUpdate;
            }
        }

    }
}