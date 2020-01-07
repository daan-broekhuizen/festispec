using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Validators;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using FluentValidation;
using System;
using FluentValidation.Results;
using System.Linq;

namespace Festispec.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private UserRepository _user;
        private NavigationService navigationService;
        private UserRepository userRepo;
        public AccountViewModel AccountVM { get; set; }

        public ICommand RegisterCommand { get; set; }


        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public RegisterViewModel()
        {

        }

        public RegisterViewModel(NavigationService navigationService, UserRepository userRepo)
        {
            this.navigationService = navigationService;
            this.userRepo = userRepo;
            _user = new UserRepository();
            AccountVM = new AccountViewModel();
            RegisterCommand = new RelayCommand(Register);
        }

        public void Register()
        {


            ValidationResult result = new RegisterValidator().Validate(AccountVM);
            if (result.IsValid)
            {
                ErrorMessage = "";

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
            }
            else
            {
                ValidationFailure userError = result.Errors.Where(e => e.PropertyName == "Username").FirstOrDefault();
                if (userError != null)
                    ErrorMessage = userError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure passwordError = result.Errors.Where(e => e.PropertyName == "Password").FirstOrDefault();
                if (passwordError != null)
                    ErrorMessage = passwordError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure firstnameError = result.Errors.Where(e => e.PropertyName == "FirstName").FirstOrDefault();
                if (firstnameError != null)
                    ErrorMessage = firstnameError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure infixError = result.Errors.Where(e => e.PropertyName == "Infix").FirstOrDefault();
                if (infixError != null)
                    ErrorMessage = infixError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure lastNameError = result.Errors.Where(e => e.PropertyName == "LastName").FirstOrDefault();
                if (lastNameError != null)
                    ErrorMessage = lastNameError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure streetError = result.Errors.Where(e => e.PropertyName == "StreetName").FirstOrDefault();
                if (streetError != null)
                    ErrorMessage = streetError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure housnumberError = result.Errors.Where(e => e.PropertyName == "HouseNumber").FirstOrDefault();
                if (housnumberError != null)
                    ErrorMessage = housnumberError.ToString();
                else
                    ErrorMessage = "";

                ValidationFailure cityError = result.Errors.Where(e => e.PropertyName == "City").FirstOrDefault();
                if (cityError != null)
                    ErrorMessage = cityError.ToString();
                else
                    ErrorMessage = "";
            }
        }
    }
}
