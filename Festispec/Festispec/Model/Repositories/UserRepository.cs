using Festispec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Repositories
{
    public class UserRepository
    {
        public bool Register(Account account)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Account.Add(account);
                return context.SaveChanges() > 1;
            }
        }

        public Account GetAccount(Account account)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Account acc = context.Account.Include("RolType").Where(u => u.Gebruikersnaam == account.Gebruikersnaam
                 && u.Wachtwoord == account.Wachtwoord).FirstOrDefault();
                return acc;
            }
        }

        public void UpdateAccount(Account account)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Account toUpdate = context.Account.Where(c => c.AccountID == account.AccountID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(account);
                context.SaveChanges();
            }
        }

        public List<Rol> GetRols()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Rol.ToList();
            }
        }

        public List<Account> GetUsers()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Account.ToList();
            }
        }
    }
}
