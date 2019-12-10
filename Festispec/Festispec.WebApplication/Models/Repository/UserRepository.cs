using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.Repository
{
    public class UserRepository
    {
        public bool Register(Account account)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.Account.Add(account);
                return context.SaveChanges() > 1;
            }
        }

        public Account GetAccount(Account account)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                Account acc = context.Account.Where(u => u.Gebruikersnaam == account.Gebruikersnaam
                 && u.Wachtwoord == account.Wachtwoord).FirstOrDefault();
                return acc;
            }
        }

        public List<Account> GetUsers()
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                return context.Account.ToList();
            }
        }

        //public List<Inspectieformulier> GetMyAssignments(int InspectorID)
        //{
        //    using (FestiSpecContext context = new FestiSpecContext())
        //    {
        //        Account a = context.Account.Where(i => i.AccountID == InspectorID).FirstOrDefault();
        //        List<Inspectieformulier> assigments = context.Inspectieformulier.Include("Account").Where(i => i.Account.Contains(a) && i.Datum_inspectie >= DateTime.Now.Date).ToList();
        //        return assigments;
        //    }
        //}
    }
}
