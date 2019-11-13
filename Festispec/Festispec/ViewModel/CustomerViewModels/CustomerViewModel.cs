using FestiSpec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        

        public string Addition
        {
            get => _klant.Naam;
            set
            {
                _klant.Naam = value;
                RaisePropertyChanged("Addition");
            }
        }

        public string KvK
        {
            get
            {
                if (_klant.KvK_nummer != 0)
                    return Convert.ToString(_klant.KvK_nummer);
                else
                    return "";
            }
            set
            {
                _klant.KvK_nummer = Convert.ToInt32(value);
                RaisePropertyChanged("KvK");
            }
        }

        public string HouseNumber
        {
            get
            {
                if (_klant.Huisnummer != 0)
                    return Convert.ToString(_klant.Huisnummer);
                else
                    return "";
            }
            set
            {
                _klant.Huisnummer = Convert.ToInt32(value);
                RaisePropertyChanged("HouseNumber");
            }
        }

        /*
        public string BranchNumber
        {
            get
            {
                if (_klant.KvK_nummer != 0)
                    return Convert.ToString(_klant.KvK_nummer);
                else
                    return "";
            }
            set
            {
                _klant.KvK_nummer = Convert.ToInt32(value);
                RaisePropertyChanged("BranchNumber");
            }
        }

        


        public string Telephone
        {
            get
            {
                if (_klant.Huisnummer != 0)
                    return Convert.ToString(_klant.Huisnummer);
                else
                    return "";
            }
            set
            {
                _klant.KvK_nummer = Convert.ToInt32(value);
                RaisePropertyChanged("Telephone");
            }
        }
        */

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

        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }

        public CustomerViewModel(Klant klant)
        {
            _klant = klant;
        }

        public CustomerViewModel()
        {
            _klant = new Klant();
        }
    }
}
