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
                Console.WriteLine(account.Gebruikersnaam + account.Wachtwoord);
                Account acc = context.Account.Where(u => u.Gebruikersnaam == account.Gebruikersnaam
                 && u.Wachtwoord == account.Wachtwoord).FirstOrDefault();
                Console.WriteLine(acc.Email);
                return acc;
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
