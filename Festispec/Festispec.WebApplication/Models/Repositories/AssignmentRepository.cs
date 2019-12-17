using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.Repository
{
    public class AssignmentRepository
    {
        public List<Inspectieformulier> GetCustomerLogo(int InspectorID)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Account a = context.Account
                    .Where(i => i.AccountID == InspectorID)
                    .FirstOrDefault();

                List<Inspectieformulier> assignments = context.Inspectieformulier
                    .Include("Account")
                    .Where(i => i.Account.Equals(a) && i.Datum_inspectie >= DateTime.Now.Date)
                    .ToList();

                return assignments;
            }
        }
    }
}