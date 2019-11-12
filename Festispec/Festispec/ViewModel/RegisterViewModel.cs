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
            foreach (var c in repo.GetUsers())
            {
                if (c.Gebruikersnaam == account.Gebruikersnaam)
                    return false;
                else
                    return repo.Register(account);
            }
            return false;
        }
    }
}
