using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class InspectionFormRepository : IInspectionFormRepository
    {
        

        public int CreateInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Inspectieformulier.Add(inspec);
                context.SaveChanges();
                return context.Inspectieformulier.Max(i => i.InspectieformulierID);
            }
        }

        public void DeleteInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Inspectieformulier.Remove(inspec);
                context.SaveChanges();
            }
        }

        public List<Inspectieformulier> GetInspectieformulier(int OpdrachtID)
        {
            using(FestispecContext context = new FestispecContext())
            {
                return context.Inspectieformulier.Include("InspectieformulierVragenlijstCombinatie").Include("Vraag").Include("VraagMogelijkAntwoord").Where(i => i.OpdrachtID == OpdrachtID).ToList();
            }
        }

        public void RemoveQuestionFromInspectionForm(InspectieformulierVragenlijstCombinatie voc)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.InspectieformulierVragenlijstCombinatie.Remove(voc);
                context.SaveChanges();
            }
        }

        public void UpdateInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Inspectieformulier Target = context.Inspectieformulier.Include("InspectieFormulierVragenLijstCombinatie").Single(i => i.InspectieformulierID == inspec.InspectieformulierID);
                context.Entry(Target).CurrentValues.SetValues(inspec);

                context.SaveChanges();
            }
        }

        

        public void AddQuestionToInspectionForm(InspectieformulierVragenlijstCombinatie voc)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.InspectieformulierVragenlijstCombinatie.Add(voc);
                context.SaveChanges();
            }
        }

        public void UpdateQuestionOrderInspectionForm(List<InspectieformulierVragenlijstCombinatie> newOrder)
        {
            using (FestispecContext context = new FestispecContext())
            {
                foreach(InspectieformulierVragenlijstCombinatie change in newOrder)
                {
                    InspectieformulierVragenlijstCombinatie target = context.InspectieformulierVragenlijstCombinatie.Where(i => i.InspectieformulierID == change.InspectieformulierID && i.VraagID == change.VraagID).FirstOrDefault();
                    context.Entry(target).CurrentValues.SetValues(change);
                }

                context.SaveChanges();
            }
        }

        public void UpdateQuestion(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Vraag target = context.Vraag.Where(v => v.VraagID == question.VraagID).FirstOrDefault();
                context.Entry(target).CurrentValues.SetValues(question);
            }
        }

        public void DeleteQuestion(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Vraag.Remove(question);
                context.SaveChanges();
            }
        }

        public int AddQuestion(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Vraag.Add(question);
                context.SaveChanges();
                return context.Vraag.Max(q => q.VraagID);
            }
        }

        public void DeletePossibleAnwser(VraagMogelijkAntwoord pos)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.VraagMogelijkAntwoord.Remove(pos);
                context.SaveChanges();
            }
        }

        public void AddPossibleAnwser(VraagMogelijkAntwoord pos)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.VraagMogelijkAntwoord.Add(pos);
                context.SaveChanges();
            }
        }

        public void updatePossibleAnswer(VraagMogelijkAntwoord pos)
        {
            using (FestispecContext context = new FestispecContext())
            {
                VraagMogelijkAntwoord target = context.VraagMogelijkAntwoord.Where(v => v.VraagID == pos.VraagID).Where(v => v.AntwoordNummer == pos.AntwoordNummer).FirstOrDefault();
                context.Entry(target).CurrentValues.SetValues(pos);
            }
        }
    }
}
