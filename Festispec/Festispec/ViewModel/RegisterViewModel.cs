using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class RegisterViewModel
    {
        private UserRepository _user;

        public ICommand RegisterCommand { get; set; }
        public RegisterViewModel()
        {
            _user = new UserRepository();
        }

        public bool Register()
        {
            //TODO 
            // Hier een nieuw account maken en de waardes toekennen
            // Deze is nog niet af aangezien de values nog nergens zijn 
            Account newAccount = new Account()
            {
                Gebruikersnaam = "", // Hier moet ook nog encryptie worden toegepast
                Wachtwoord = "",
                Rol = ""
            };

            foreach (Account c in _user.GetUsers())
                return c.Gebruikersnaam == newAccount.Gebruikersnaam ? false : _user.Register(newAccount);

            return false;
        }
    }
}
