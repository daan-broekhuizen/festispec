using BingMapsRESTToolkit;
using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Converters;
using FestiSpec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private Klant _customer;
        public string Name
        {
            get => _customer.Naam;
            set
            {
                _customer.Naam = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Streetname
        {
            get => _customer.Straatnaam;
            set
            {
                _customer.Straatnaam = value;
                GetPostalCodeAsync();
                RaisePropertyChanged("Streetname");
            }
        }
        public string City
        {
            get => _customer.Stad;
            set
            {
                _customer.Stad = value;
                GetPostalCodeAsync();
                RaisePropertyChanged("City");
            }
        }
        public string HouseNumber
        {
            get
            {
                if (_customer.Huisnummer == null) return "";
                return Regex.Match(_customer.Huisnummer, @"\d+").Value;
            }
            set
            {
                _customer.Huisnummer = value + Addition;
                GetPostalCodeAsync();
                RaisePropertyChanged("HouseNumber");
            }
        }
        public string Addition
        {
            get
            {
                if (_customer.Huisnummer == null) return "";
                return Regex.Replace(_customer.Huisnummer, @"[^a-zA-Z]+", String.Empty);
            }
            set
            {
                _customer.Huisnummer = HouseNumber + value;
                GetPostalCodeAsync();
                RaisePropertyChanged("Addition");
            }
        }
        public string KvK
        {
            get => _customer.KvKNummer;
            set
            {
                _customer.KvKNummer = value;
                RaisePropertyChanged("KvK");
            }
        }
        public string Branchnumber
        {
            get => _customer.Vestigingnummer;
            set 
            {
                _customer.Vestigingnummer = value;
                RaisePropertyChanged("Branchnumber");
            } 
        }
        public int Id
        {
            get => _customer.KlantID;
        }
        public string Telephone
        {
            get => _customer.Telefoonnummer;
            set
            {
                _customer.Telefoonnummer = value;
                RaisePropertyChanged("Telephone");
            }
        }
        public string Email
        {
            get => _customer.Email;
            set
            {
                _customer.Email = value;
                RaisePropertyChanged("Email");
            }
        }
        public string Website
        {
            get => _customer.Website;
            set
            {
                _customer.Website = value;
                RaisePropertyChanged("Website");
            }
        }
        public ImageSource Logo
        {
            get
            {
                ImageSource image = ImageByteConverter.BytesToImage(_customer.KlantLogo);
                if (image != null)
                    return image;
                else
                    return new BitmapImage(new Uri(@"pack://application:,,,/Images/add_customer_logo.png"));
            }
            set
            {
                byte[] image = ImageByteConverter.PngImageToBytes(value);
                if (image != null)
                    _customer.KlantLogo = image;
                RaisePropertyChanged("Logo");

            }
        }
        private string _postalcode;
        public string PostalCode
        {
            get => _postalcode;
            set
            {
                _postalcode = value;
                RaisePropertyChanged("PostalCode");
            }
        }


        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }
        public CustomerViewModel(Klant klant)
        {
            _customer = klant;
            Contacts = new ObservableCollection<ContactPersonViewModel>(klant.Contactpersoon.Select(c => new ContactPersonViewModel(c)));
            GetPostalCodeAsync();
        }
        public CustomerViewModel()
        {
            _customer = new Klant();
            Contacts = new ObservableCollection<ContactPersonViewModel>();
        }
        private async Task GetPostalCodeAsync()
        {
            string query = $"{Streetname} {HouseNumber}{Addition} {City}";
            Address address = await new LocationService().GetFullAdress(query);
            PostalCode = address.PostalCode;

        }
        public void SetCustomer(Klant customer) => _customer = customer;
    }
}
