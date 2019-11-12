using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel
{
    public class LoginViewModel
    {
        private string userName;
        public ICommand LoginCommand { get; set; }
        public string UserName
        {
            get { return this.userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                    //RaisePropertyChanged(""); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        public string Password
        {
            get { return this.userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                    //RaisePropertyChanged(""); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(login, canLogin);
        }

        private bool canLogin()
        {
            return UserName.Length > 0 && Password.Length > 0;
        }



        private void login()
        {
            UserRepository user = new UserRepository();
            user.Login(new FestiSpec.Domain.Account { Wachtwoord = Password, Gebruikersnaam = UserName});
        }

    }
}
