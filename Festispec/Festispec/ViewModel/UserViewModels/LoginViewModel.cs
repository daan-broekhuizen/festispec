using Festispec.Model;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class LoginViewModel : NavigatableViewModel
    {       
        public ICommand LoginCommand { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (!string.Equals(this._username, value))
                {
                    _username = value;
                    RaisePropertyChanged("Username");
                }
            }
        }

        private string _errorFeedback;
        public string ErrorFeedback
        {
            get { return _errorFeedback; }
            set
            {
                _errorFeedback = value;
                RaisePropertyChanged("ErrorFeedback");
            }
        }

        private UserRepository _userRepository;

        public LoginViewModel(NavigationService service, UserRepository repo) : base(service)
        {
            _userRepository = repo;
            LoginCommand = new RelayCommand<PasswordBox>(Login);

            if (service.AppSettings.DebugMode)
            {
                _username = service.AppSettings.Account.Username;

                Login(service.AppSettings.Account.Password);
            }
        }

        private void Login(string password)
        {
            Account currentAccount = new Account()
            {
                Gebruikersnaam = _username,
                Wachtwoord = password
            };

            Account account = _userRepository.GetAccount(currentAccount);
            if (account != null && CanAccess(account))
                _navigationService.ApplicationNavigateTo("Main", new AccountViewModel(account));
            else if (account == null)
                ErrorFeedback = "Onbekend account, heeft u uw gebruikersnaam correct geschreven?";
            else if (!CanAccess(account))
                ErrorFeedback = "Account heeft niet de juiste rechten";
            else
                ErrorFeedback = "Gebruikersnaam wachtwoord combinatie is ongeldig";
        }

        private void Login(PasswordBox password)
        {
            Login(password.Password);
        }

        private bool CanAccess(Account account)
        {
            List<string> validRoles = new List<string> { "ad", "ma", "om", "sm" };

            return validRoles.Contains(account.RolType.Afkorting);
        }
    }
}
