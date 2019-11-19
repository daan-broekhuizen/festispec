using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using System.Security.Cryptography;
using Festispec.Utility;
using FestiSpec.Domain.Repositories;
using FestiSpec.Domain;
using Festispec.Service;
using Festispec.Model;

namespace Festispec.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private UserRepository _userRepository;
        private NavigationService _navigationService;
        
        private string _errorFeedback; // Deze geeft de gebruiker uiteindelijk feedback daan dus deze mag je ook implementeren :)
        public ICommand LoginCommand { get; set; }

        private string _username;
        public string Username
        {
            get { return this._username; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this._username, value))
                {
                    this._username = value;
                    RaisePropertyChanged("Username");
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return this._password; }
            set
            {
                if (!string.Equals(this._password, value))
                {
                    this._password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        public string ErrorFeedback
        {
            get { return this._errorFeedback; }
            set
            {
                this._errorFeedback = value;
                RaisePropertyChanged("ErrorFeedback");
            }
        }
        
        public LoginViewModel(NavigationService service, UserRepository repo)
        {
            _userRepository = repo;
            _navigationService = service;
            LoginCommand = new RelayCommand(Login);
        }


        private void Login()
        {
            Account currentAccount = new Account()
            {
                Gebruikersnaam = _username,
                Wachtwoord = _password
            };

            Account account = _userRepository.GetAccount(currentAccount);
            if (account != null)
                _navigationService.ApplicationNavigateTo("Main", null);
            else
                ErrorFeedback = "Gebruikersnaam wachtwoord combinatie is ongeldig";
        }

    }
}
