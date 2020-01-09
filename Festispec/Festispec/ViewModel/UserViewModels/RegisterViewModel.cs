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
using Festispec.Utility;
using System.Collections.Generic;

namespace Festispec.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private NavigationService _navigationService;
        private UserRepository _userRepo;
        public AccountViewModel AccountVM { get; set; }

        public ICommand RegisterCommand { get; set; }


        private ObservableDictionary<string, string> _errorMessages;
        public ObservableDictionary<string, string> ErrorMessages
        {
            get => _errorMessages;
            set
            {
                _errorMessages = value;
                RaisePropertyChanged(() => ErrorMessages);
            }
        }

        public RegisterViewModel()
        {
        }

        public RegisterViewModel(NavigationService navigationService, UserRepository userRepo)
        {
            _navigationService = navigationService;
            _userRepo = userRepo;
            AccountVM = new AccountViewModel();
            RegisterCommand = new RelayCommand(Register);
            ErrorMessages = new ObservableDictionary<string, string>()
            {
                ["Username"] = "",
                ["Password"] = "",
                ["HouseNumber"] = "",
                ["StreetName"] = "",
                ["City"] = "",
                ["FirstName"] = "",
                ["LastName"] = "",
                ["Infix"] = ""
            };
        }

        public bool ValidateAccount()
        {
            List<ValidationFailure> errors = new RegisterValidator().Validate(AccountVM).Errors.ToList();

            for (int i = 0; i < ErrorMessages.Count; i++)
            {
                string property = ErrorMessages.ElementAt(i).Key;
                ValidationFailure failure = errors.FirstOrDefault(e => e.PropertyName.Equals(property));
                if (failure != null)
                    ErrorMessages[property] = failure.ErrorMessage;
                else
                    ErrorMessages[property] = "";
            }

            RaisePropertyChanged(() => ErrorMessages);
            return errors.Count == 0;
        }

        public void Register()
        {
            if (ValidateAccount())
            {
                Account newAccount = new Account()
                {
                    Gebruikersnaam = AccountVM.Username,
                    Wachtwoord = AccountVM.Password,
                    Rol = "ng",
                    Voornaam = AccountVM.FirstName,
                    Tussenvoegsel = AccountVM.Infix,
                    Achternaam = AccountVM.LastName,
                    Straatnaam = AccountVM.StreetName,
                    Huisnummer = AccountVM.HouseNumber,
                    Stad = AccountVM.City,
                    LaatsteWijziging = DateTime.Now
                };

                _userRepo.Register(newAccount);
                _navigationService.NavigateTo("UserRights", null);
            }
        }
    }
}
