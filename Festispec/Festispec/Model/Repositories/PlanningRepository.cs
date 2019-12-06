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


                var formulier = context.Inspectieformulier.Find(1);

                var account = context.Account.Find(3);

                formulier.Ingepland.Add(account);
            }
        }
    }
}
