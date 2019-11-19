using Festispec.Model;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
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

        private string _username;
        private string _password;
        private string _role;
        private string _firstName;
        private string _infix;
        private string _lastName;
        private string _postalCode;
        private int _houseNumber;
        private string _email;
        private long _phoneNumber;
        private DateTime _dateOfCertification;
        private DateTime _endDateOfCertifcation;
        private string _iban;

        public ICommand RegisterCommand { get; set; }

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

        public string Role
        {
            get { return this._role; }
            set
            {
                if (!string.Equals(this._role, value))
                {
                    this._role = value;
                    RaisePropertyChanged("Role");
                }
            }
        }

        public string FirstName
        { 
            get { return this._firstName; }
            set
            {
                if (!string.Equals(this._firstName, value))
                {
                    this._firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        public string Infix
        { 
            get { return this._infix; }
            set
            {
                this._infix = value;
                RaisePropertyChanged("Infix");
            }
        }

        public string LastName
        { 
            get { return this._lastName; }
            set
            {
                this._lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string PostalCode
        {
            get { return this._postalCode; }
            set
            {
                this._postalCode = value;
                RaisePropertyChanged("PostalCode");
            }
        }

        public int HouseNumber
        {
            get { return this._houseNumber; }
            set
            {
                this._houseNumber = value;
                RaisePropertyChanged("HouseNumber");
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                this._email = value;
                RaisePropertyChanged("Email");
            }
        }

        public long PhoneNumber
        {
            get { return this._phoneNumber; }
            set
            {
                this._phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public DateTime DateOfCertification
        {
            get { return this._dateOfCertification; }
            set
            {
                this._dateOfCertification = value;
                RaisePropertyChanged("DateOfCertification");
            }
        }

        public DateTime EndDateOfCertification
        {
            get { return this._endDateOfCertifcation; }
            set
            {
                this._endDateOfCertifcation = value;
                RaisePropertyChanged("EndDateOfCertification");
            }
        }

        public string IBAN
        {
            get { return this._iban; }
            set
            {
                this._iban = value;
                RaisePropertyChanged("IBAN");
            }
        }

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
