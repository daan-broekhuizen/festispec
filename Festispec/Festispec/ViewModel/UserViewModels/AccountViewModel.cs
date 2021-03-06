﻿using BingMapsRESTToolkit;
using Festispec.Model;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private Account _account;
        private UserRepository _userRepository;

        public AccountViewModel(Account account)
        {
            _account = account;
            Rollen = new ObservableCollection<string>();
            _userRepository = new UserRepository();
            Rollen.Add("Admin");
            Rollen.Add("Inspecteur");
            Rollen.Add("Management");
            Rollen.Add("Operationeelmedewerker");
            Rollen.Add("Salesmedewerker");
            GetPostalCodeAsync();
            Rollen.Add("Nieuwe Gebruiker");
        }

        public AccountViewModel()
        {
            _account = new Account();
            _userRepository = new UserRepository();
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
            get => _account.Voornaam;
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
                GetPostalCodeAsync();
                RaisePropertyChanged("HouseNumber");
            }
        }

        public string StreetName
        {
            get => _account.Straatnaam;
            set
            {
                _account.Straatnaam = value;
                GetPostalCodeAsync();
                RaisePropertyChanged("StreetName");
            }
        }

        public string City
        {
            get => _account.Stad;
            set
            {
                _account.Stad = value;
                GetPostalCodeAsync();
                RaisePropertyChanged("City");
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

        private async Task GetPostalCodeAsync()
        {
            string street = StreetName.Remove(StreetName.Length - 1, 1);
            string query = $"{street} {HouseNumber} {City}";
            Address address = await new LocationService().GetFullAdress(query);
            if (address.AddressLine.ToLower().Contains(StreetName.ToLower()))
                PostalCode = address.PostalCode;
            else
                PostalCode = "";

        }

        public ObservableCollection<string> Rollen { get; set; }


        public string RoleComplete
        {
            get
            {
                switch(Role)
                {
                    case "ad":
                        return "Admin";
                    case "in":
                        return "Inspecteur";
                    case "ma":
                        return "Management";
                    case "om":
                        return "Operationeelmedewerker";
                    case "sm":
                        return "Salesmedewerker";
                    case "ng":
                            return "Nieuwe Gebruiker";
                }

                return Role;
            }

            set
            {
                switch (value)
                {
                    case "Admin":
                        Role = "ad";
                        _userRepository.UpdateAccount(_account);
                        break;
                    case "Inspecteur":
                        Role = "in";
                        _userRepository.UpdateAccount(_account);
                        break;
                    case "Management":
                        Role = "ma";
                        _userRepository.UpdateAccount(_account);
                        break;
                    case "Operationeelmedewerker":
                        Role = "om";
                        _userRepository.UpdateAccount(_account);
                        break;
                    case "Salesmedewerker":
                        Role = "sm";
                        _userRepository.UpdateAccount(_account);
                        break;
                    case "Nieuwe Gebruiker":
                        Role = "ng";
                        _userRepository.UpdateAccount(_account);
                        break;
                }
            }
        }
    }
}
