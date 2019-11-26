﻿using System;
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

        private string _password;
        public string Password
        {
            get { return this._password; }
            set
            {
                if (!string.Equals(this._password, value))
                {
                    _password = value;
                    RaisePropertyChanged("Password");
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
            LoginCommand = new RelayCommand(Login);

            if (service.AppSettings.DebugMode)
            {
                _username = service.AppSettings.Account.Username;
                _password = service.AppSettings.Account.Password;

                Login();
            }
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
                _navigationService.ApplicationNavigateTo("Main", new AccountViewModel(account));
            else
                ErrorFeedback = "Gebruikersnaam wachtwoord combinatie is ongeldig";
        }

    }
}