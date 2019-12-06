using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Festispec.Model.Repositories
{
    public class PlanningRepository
    {
        public List<Account> GetFreeInspectors(int id)
        {
            using (FestispecContext context = new FestispecContext())
            {
                SqlParameter param = new SqlParameter("@ID", id);

                return context.Database.SqlQuery<Account>("GetInspectors @ID", param).ToList();
            }
        }

        public void Test()
        {
            Inspectieformulier inspec = new Inspectieformulier()
            {
                InspectieformulierID = 2,
                InspectieFormulierTitel = "testje"

                
            };
            using (FestispecContext context = new FestispecContext())
            {
                var target = context.Inspectieformulier.Include("Ingepland").Where(i => i.InspectieformulierID == 2).FirstOrDefault();
                var copy = target;
                var targetAccount = context.Account.Where(i => i.AccountID == 1).FirstOrDefault();
                copy.Ingepland.Add(targetAccount);
                context.Entry(target).CurrentValues.SetValues(copy);


                var formulier = context.Inspectieformulier.Find(1);

                var account = context.Account.Find(3);

                formulier.Ingepland.Add(account);
            }
        }
    }
}
