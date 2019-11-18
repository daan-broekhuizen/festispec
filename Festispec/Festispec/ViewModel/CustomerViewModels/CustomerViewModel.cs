using BingMapsRESTToolkit;
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
            get => _postalcode;
            set
            {
                _postalcode = value;
                RaisePropertyChanged("PostalCode");
            }
        }
        public string Streetname
        {
            get => _klant.Straatnaam;
            set
            {
                _klant.Straatnaam = value;
                GetPostalCodeAsync();
                RaisePropertyChanged("PostalCode");
            }
        }
        public string City
        {
            get => _klant.Stad;
            set
            {
                _klant.Stad = value;
                GetPostalCodeAsync();
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
                GetPostalCodeAsync();
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
                GetPostalCodeAsync();
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
        public ImageSource Logo
        {
            get => ImageByteConverter.BytesToImage(_klant.Klant_logo);
            set
            {
                _klant.Klant_logo = ImageByteConverter.PngImageToBytes(value);
                RaisePropertyChanged("Logo");
            }
        }
        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }

        private Klant _klant;
        private string _postalcode;

        public CustomerViewModel(Klant klant)
        {
            _klant = klant;
            Contacts = new ObservableCollection<ContactPersonViewModel>(klant.Contactpersoon.Select(c => new ContactPersonViewModel(c)));
            GetPostalCodeAsync();
        }
        public CustomerViewModel()
        {
            _klant = new Klant();
            Logo = new BitmapImage(new Uri(@"pack://application:,,,/Images/add_customer_logo.png"));
            Contacts = new ObservableCollection<ContactPersonViewModel>();
        }
        private async Task GetPostalCodeAsync()
        {
            string query = $"{Streetname} {HouseNumber}{Addition} {City}";
            Address address = await new LocationService().GetFullAdress(query);
            PostalCode = address.PostalCode;
        }
    }
}
