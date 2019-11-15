using FestiSpec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private Klant _klant;
        public string Name 
        { 
            get => _klant.Naam;
            set
            {
                _klant.Naam = value;
                RaisePropertyChanged("Name");
            }
        }
        public string PostalCode
        {
            get => _klant.Postcode;
            set
            {
                _klant.Postcode = value;
                RaisePropertyChanged("PostalCode");
            }
        }
        public string HouseNumber
        {
            get
            {
                if (_klant.Huisnummer == null) return "";
                return Regex.Match(_klant.Huisnummer, @"\d+").Value;
            }
            set
            {
                _klant.Huisnummer = value + Addition;
                RaisePropertyChanged("HouseNumber");
            }
        }
        public string Addition
        {
            get 
            {
                if (_klant.Huisnummer == null) return "";
                return Regex.Replace(_klant.Huisnummer, @"[^a-zA-Z]+", String.Empty);
            } 
            set
            {
                _klant.Huisnummer = HouseNumber + value;
                RaisePropertyChanged("Addition");
            }
        }
        public string KvK
        {
            get => _klant.KvK_nummer;
            set
            {
                _klant.KvK_nummer = value;
                RaisePropertyChanged("KvK");
            }
        }       
        public string Telephone
        {
            get => _klant.Telefoonnummer;
            set
            {
                _klant.Telefoonnummer = value;
                RaisePropertyChanged("Telephone");
            }
        }
        public string Email
        {
            get => _klant.Email;
            set
            {
                _klant.Email = value;
                RaisePropertyChanged("Email");
            }
        }
        public string Website
        {
            get => _klant.Website;
            set
            {
                _klant.Website = value;
                RaisePropertyChanged("Website");
            }
        }

        private List<ContactPersonViewModel> _contacts;
        public List<ContactPersonViewModel> Contacts 
        { 
            get => _contacts; 
            set
            {
                _contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }

        public CustomerViewModel(Klant klant)
        {
            _klant = klant;
            Contacts = klant.Contactpersoon.Select(c => new ContactPersonViewModel(c)).ToList();
        }
        public CustomerViewModel()
        {
            _klant = new Klant();
            Contacts = new List<ContactPersonViewModel>();
        }
    }
}
