using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void RemoveQuestionFromInspectionForm(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Vraag target = context.Vraag.Where(v => v.VraagID == question.VraagID).FirstOrDefault();
                context.Vraag.Remove(target);
                context.SaveChanges();
            }
        }

        public void UpdateInspectieFormulier(Inspectieformulier inspec)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Inspectieformulier target = context.Inspectieformulier.Include("Vraag").SingleOrDefault(i => i.InspectieformulierID == inspec.InspectieformulierID);
                context.Entry(target).CurrentValues.SetValues(inspec);

                context.SaveChanges();
            }
        }

        

        public void AddQuestionToInspectionForm(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Vraag.Add(question);
                context.SaveChanges();
            }
        }

       public void UpdateQuestionOrderInspectionForm(List<Vraag> newOrder)
        {
            using (FestispecContext context = new FestispecContext())
            {
                foreach(Vraag change in newOrder)
                {
                    Vraag target = context.Vraag.Where(i => i.VraagID == change.VraagID).FirstOrDefault();
                    context.Entry(target).CurrentValues.SetValues(change);
                }

                context.SaveChanges();
            }
        }

        public int UpdateQuestion(Vraag question)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Vraag target = context.Vraag.Where(v => v.VraagID == question.VraagID).FirstOrDefault();
                context.Entry(target).CurrentValues.SetValues(question);
                context.SaveChanges();
                return 1;
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

        public void updatePossibleAnswers(List<VraagMogelijkAntwoord> possisbleAnwsers)
        {
            using (FestispecContext context = new FestispecContext())
            {
                int questionID = possisbleAnwsers[0].VraagID;
                List<VraagMogelijkAntwoord> deleteTargets = context.VraagMogelijkAntwoord.Where(i => i.VraagID == questionID).ToList();
                context.VraagMogelijkAntwoord.RemoveRange(deleteTargets);

                context.VraagMogelijkAntwoord.AddRange(possisbleAnwsers);
                context.SaveChanges();
            }
        }
    }
}
