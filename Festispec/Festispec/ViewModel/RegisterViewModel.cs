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
        public bool Register()
        {
            //TODO 
            // Hier een nieuw account maken en de waardes toekennen
            // Deze is nog niet af aangezien ik nu nog niet verder kan
            Account newAccount = new Account()
            {
                Gebruikersnaam = "",
                Wachtwoord = "",
                Rol = ""
            };

            UserRepository repo = new UserRepository();
            foreach (Account c in repo.GetUsers())
                return c.Gebruikersnaam == newAccount.Gebruikersnaam ? false : repo.Register(newAccount);

            return false;
        }
    }
}
