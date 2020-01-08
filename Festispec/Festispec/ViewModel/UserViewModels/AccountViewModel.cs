using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private Account _account;

        public AccountViewModel(Account account)
        {
            _account = account;
        }

        public AccountViewModel()
        {
            _account = new Account();
        }

        public int Id => _account.AccountID;

        public string Username
        {
            get => _account.Gebruikersnaam;
            set
            {
                _account.Gebruikersnaam = value;
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get => _account.Wachtwoord;
            set
            {
                _account.Wachtwoord = value;
                RaisePropertyChanged("Password");
            }
        }

        public string Role
        {
            get => _account.Rol;
            set
            {
                _account.Rol = value;
                RaisePropertyChanged("Role");
            }
        }

        public string FirstName
        {
            get => _account.Gebruikersnaam;
            set
            {
                _account.Voornaam = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string Infix
        {
            get => _account.Tussenvoegsel;
            set
            {
                _account.Tussenvoegsel = value;
                RaisePropertyChanged("Infix");
            }
        }

        public string LastName
        {
            get => _account.Achternaam;
            set
            {
                _account.Achternaam = value;
                RaisePropertyChanged("LastName");
            }
        }

        //TODO implement (see customerviewmodel + locationservice)
        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                RaisePropertyChanged("PostalCode");
            }
        }

        public string HouseNumber
        {
            get => _account.Huisnummer;
            set
            {
                _account.Huisnummer = value;
                RaisePropertyChanged("HouseNumber");
            }
        }

        public string Email
        {
            get => _account.Email;
            set
            {
                _account.Email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string PhoneNumber
        {
            get => _account.Telefoonnummer;
            set
            {
                _account.Telefoonnummer = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public DateTime? DateOfCertification
        {
            get => _account.DatumCertificering;
            set
            {
                _account.DatumCertificering = value;
                RaisePropertyChanged("DateOfCertification");
            }
        }

        public DateTime? EndDateOfCertification
        {
            get => _account.EinddatumCertificering;
            set
            {
                _account.EinddatumCertificering = value;
                RaisePropertyChanged("EndDateOfCertification");
            }
        }

        public string IBAN
        {
            get => _account.IBAN;
            set
            {
                _account.IBAN = value;
                RaisePropertyChanged("IBAN");
            }
        }
    }
}
