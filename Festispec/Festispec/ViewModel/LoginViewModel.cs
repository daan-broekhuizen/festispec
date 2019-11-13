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
    public class LoginViewModel  : ViewModelBase 
    {
        private UserRepository _user;
        private Encrypt _encrypt;

        private string _userName;
        private string _password;
        private string _errorFeedback; // Deze geeft de gebruiker uiteindelijk feedback daan dus deze mag je ook implementeren :)
        public ICommand LoginCommand { get; set; }
        public string UserName
        {
            get { return this._userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this._userName, value))
                {
                    this._userName = value;
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


        public LoginViewModel()
        {
            _encrypt = new Encrypt();
            _user = new UserRepository();

            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin() => UserName.Length > 0 && Password.Length > 0;



        private void Login()
        {
            Account currentAccount = new Account()
            {
                Gebruikersnaam = _userName,
                Wachtwoord = _encrypt.GetHashString(_password)
            };

            if (_user.Login(currentAccount))
                _user.LoggedInAccount = currentAccount;
            else
                _errorFeedback = "Foute inlog gegevens";
        }

    }
}
