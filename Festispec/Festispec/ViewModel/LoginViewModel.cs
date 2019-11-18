using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.CommandWpf;
using FestiSpec.Domain;
using GalaSoft.MvvmLight;
using System.Security.Cryptography;
using Festispec.Utility;

namespace Festispec.ViewModel
{
    public class LoginViewModel : ViewModelBase 
    {
        private UserRepository _user;
        private Encrypt _encrypt;

        private string _username;
        private string _password;
        private string _errorFeedback; // Deze geeft de gebruiker uiteindelijk feedback daan dus deze mag je ook implementeren :)
        public ICommand LoginCommand { get; set; }
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

        public String ErrorFeedback
        { 
            get { return this._errorFeedback; } 
            set
            {
                this._errorFeedback = value;
                RaisePropertyChanged("ErrorFeedback");
            }
        }


        public LoginViewModel()
        {
            _encrypt = new Encrypt();
            _user = new UserRepository();

            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin() => Username != null && Username.Length > 0 && Password.Length > 0;

        private void Login()
        {
            Account currentAccount = new Account()
            {
                Gebruikersnaam = _username,
                Wachtwoord = _encrypt.GetHashString(_password)
            };

            if (_user.Login(currentAccount))
                _user.LoggedInAccount = currentAccount;
            else
                _errorFeedback = "Foute inlog gegevens";
        }

    }
}
