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

        public void AddToPlanning(int accountId, int formulierId)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Inspectieformulier formulier = context.Inspectieformulier.Find(formulierId);
                Account account = context.Account.Find(accountId);

                formulier.Ingepland.Add(account);
                context.SaveChanges();
            }
        }
    }
}
