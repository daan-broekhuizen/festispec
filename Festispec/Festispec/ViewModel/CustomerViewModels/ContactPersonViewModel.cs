using Festispec.Model;
using FestiSpec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class ContactPersonViewModel : ViewModelBase
    {
        private Contactpersoon _contactPerson;
        public int Id => _contactPerson.ContactpersoonID;
        public string Email 
        { 
            get => _contactPerson.Email;
            set
            {
                _contactPerson.Email = value;
                RaisePropertyChanged("Email");
            }
        }
        public string FirstName
        {
            get => _contactPerson.Voornaam;
            set
            {
                _contactPerson.Voornaam = value;
                RaisePropertyChanged("FirstName");
            }
        }
        public string Infix
        {
            get => _contactPerson.Tussenvoegsel;
            set
            {
                _contactPerson.Tussenvoegsel = value;
                RaisePropertyChanged("Infix");
            }
        }
        public string LastName
        {
            get => _contactPerson.Achternaam;
            set
            {
                _contactPerson.Achternaam = value;
                RaisePropertyChanged("LastName");
            }
        }
        public string Telephone
        {
            get => _contactPerson.Telefoon;
            set
            {
                _contactPerson.Telefoon = value;
                RaisePropertyChanged("Telephone");
            }
        }
        public string Note
        {
            get => _contactPerson.Notities;
            set
            {
                _contactPerson.Notities = value;
                RaisePropertyChanged("Note");
            }
        }
        public string Role
        {
            get => _contactPerson.Rol;
            set
            {
                _contactPerson.Rol = value;
                RaisePropertyChanged("Role");
            }
        }

        public ContactPersonViewModel(Contactpersoon contact)
        {
            _contactPerson = contact;
        }
        public ContactPersonViewModel()
        {
            _contactPerson = new Contactpersoon();
        }
    }
}
