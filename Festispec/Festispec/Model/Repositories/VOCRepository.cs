using Festispec.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class VOCRepository : IVOCRepository
    {
        public List<InspectieformulierVragenlijstCombinatie> GetVragenlijstCombinaties(int InspectieformulierID)
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.InspectieformulierVragenlijstCombinatie.Where(v => v.InspectieformulierID == InspectieformulierID).ToList();
            }
        }
        public void AddVraagToInspectieFormulier(InspectieformulierVragenlijstCombinatie voc)
        {
            using(FestispecContext context = new FestispecContext())
            {
                context.InspectieformulierVragenlijstCombinatie.Add(voc);
                context.SaveChanges();
            }
        }

        public void RemoveVraagFromInspectieFormulier(InspectieformulierVragenlijstCombinatie voc)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.InspectieformulierVragenlijstCombinatie.Remove(voc);
                context.SaveChanges();
            }
        }
    }
}
