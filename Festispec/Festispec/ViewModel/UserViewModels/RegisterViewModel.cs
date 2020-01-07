using Festispec.Model;
using Festispec.Service;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private UserRepository _user;
        private NavigationService navigationService;
        private UserRepository userRepo;
        public AccountViewModel AccountVM { get; set; }

        public ICommand RegisterCommand { get; set; }


        public RegisterViewModel()
        {
            _user = new UserRepository();

            RegisterCommand = new RelayCommand(Register);
        }

        public RegisterViewModel(NavigationService navigationService, UserRepository userRepo)
        {
            this.navigationService = navigationService;
            this.userRepo = userRepo;

        }

        public void Register()
        {
            Account newAccount = new Account()
            {
                Gebruikersnaam = AccountVM.Username,
                Wachtwoord = AccountVM.Password,
                Rol = "in",
                Voornaam = AccountVM.FirstName,
                Tussenvoegsel = AccountVM.Infix,
                Achternaam = AccountVM.LastName,
                Straatnaam = AccountVM.StreetName,
                Huisnummer = AccountVM.HouseNumber,
                Stad = AccountVM.City
            };

            _user.Register(newAccount);

            navigationService.NavigateTo("UserRights", null);

            //foreach (Account c in _user.GetUsers())
            //    return c.Gebruikersnaam == newAccount.Gebruikersnaam ? false : _user.Register(newAccount);

            //return false;
        }
    }
}
