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
        public Account LoggedInAccount { get; set; }

        public bool Register(Account account)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Account.Add(account);
                return context.SaveChanges() > 1;
            }
        }

        public bool Login(Account account)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Account acc = context.Account.FirstOrDefault(u => u.Gebruikersnaam == account.Gebruikersnaam
                 && u.Wachtwoord == account.Wachtwoord);

                if (acc != null) // Ingelogd
                    return true;
                else    // Fout
                    return false;
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
