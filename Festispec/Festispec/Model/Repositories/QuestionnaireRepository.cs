using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        public List<Inspectieformulier> GetInspectieformulier(int opdrachtID)
        {
            using(FestispecContext context = new FestispecContext())
            {
                return context.Inspectieformulier.Where(i => i.OpdrachtID == opdrachtID).ToList();
            }
        }

        public void UpdateInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Inspectieformulier target = context.Inspectieformulier.Where(i => i.InspectieformulierID == inspec.InspectieformulierID).FirstOrDefault();
                context.Entry(target).CurrentValues.SetValues(inspec);
                context.SaveChanges();
            }
        }
        public void CreateInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Inspectieformulier.Add(inspec);
                context.SaveChanges();
            }
        }

        public void DeleteInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Inspectieformulier target = context.Inspectieformulier.Where(i => i.InspectieformulierID == inspec.InspectieformulierID).FirstOrDefault();
                context.Inspectieformulier.Remove(target);
                context.SaveChanges();
            }
        }
    }
}
