using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.CommandWpf;
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

           // RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister()
        {
            throw new NotImplementedException();
        }

        public bool Register()
        {
            //TODO 
            // Hier een nieuw account maken en de waardes toekennen
            // Deze is nog niet af aangezien de values nog nergens zijn 
            Account newAccount = new Account()
            {
                Gebruikersnaam = "", 
                Wachtwoord = "", // Encryptie toepassen
                Rol = ""
            };

            foreach (Account c in _user.GetUsers())
                return c.Gebruikersnaam == newAccount.Gebruikersnaam ? false : _user.Register(newAccount);

            return false;
        }
    }
}
