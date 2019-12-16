using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Festispec.WebApplication.Models.Repositories
{
    public class UserRepository
    {
        private InspectionformRepository _inspectionRepo = new InspectionformRepository();
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

        public List<Inspectieformulier> GetMyAssignments(int InspectorID)
        {
            return _inspectionRepo.GetInspectionforms(InspectorID);
        }
    }
}
