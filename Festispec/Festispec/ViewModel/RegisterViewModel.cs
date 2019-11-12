using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class RegisterViewModel
    {
        public bool Resgister()
        {
            //TODO 
            // Hier een nieuw account maken en de waardes toekennen
            Account account = new Account();

            UserRepository repo = new UserRepository();
            foreach (Account c in repo.GetUsers())
                return c.Gebruikersnaam == account.Gebruikersnaam ? false : repo.Register(account);

            return false;
        }
    }
}
