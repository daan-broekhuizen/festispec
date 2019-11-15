using Festispec.Service;
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
    public class AddContactPersonViewModel : ViewModelBase
    {
        public CustomerViewModel CustomerViewModel { get; set; }
        public ContactPersonViewModel ContactPersonViewModel
        {
            get => _contactPersonViewModel;
            set
            {
                _contactPersonViewModel = value;
                RaisePropertyChanged("ContactPersonViewModel");
            } 
        }
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }

        private NavigationService _navigationService;
        private CustomerRepository _customerRepo;
        private ContactPersonViewModel _contactPersonViewModel;

        public AddContactPersonViewModel(NavigationService service)
        {
            _navigationService = service;
            _customerRepo = new CustomerRepository();
            ContactPersonViewModel = new ContactPersonViewModel();

            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerViewModel = service.Parameter as CustomerViewModel;

            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson, CanSaveContactPerson);
        }

        private void SaveCustomer()
        {
            Klant klant = new Klant()
            {
                Naam = CustomerViewModel.Name,
                Email = CustomerViewModel.Email,
                Huisnummer = CustomerViewModel.HouseNumber,
                KvK_nummer = Convert.ToInt32(CustomerViewModel.KvK),
                Postcode = CustomerViewModel.PostalCode,
                Website = CustomerViewModel.Website,
                Laatste_weiziging = DateTime.Now
            };
            //Create Customer
            _customerRepo.CreateCustomer(klant);
            Klant entity = _customerRepo.GetCustomers().Where(c => c.KvK_nummer == klant.KvK_nummer).FirstOrDefault();
            //Add ContactPersons
            CustomerViewModel.Contacts.ForEach(c =>
            _customerRepo.CreateContactPerson(new Contactpersoon()
            {
                Voornaam = c.Name,
                Tussenvoegsel = c.Name,
                Achternaam = c.Name,
                Email = c.Email,
                Telefoon = Convert.ToInt32(c.Telephone),
                Notities = c.Note,
                Klant = entity,
                Laatste_weiziging = DateTime.Now

            }));

            //Navigate to CustomerInfo
            _navigationService.NavigateTo("CustomerInfo", CustomerViewModel);
        }

        private bool CanSaveCustomer()
        {
            return CustomerViewModel.Name != null &&
                   CustomerViewModel.KvK.Length == 8 &&
                   CustomerViewModel.PostalCode.Length == 6 &&
                   CustomerViewModel.HouseNumber != null &&
                   CustomerViewModel.Email != null;
        }

        private void SaveContactPerson()
        {
            CustomerViewModel.Contacts.Add(ContactPersonViewModel);
            ContactPersonViewModel = new ContactPersonViewModel();
        }

        private bool CanSaveContactPerson()
        {
            return ContactPersonViewModel.Email != null &&
                   ContactPersonViewModel.Name != null &&
                   ContactPersonViewModel.Telephone != null;
        }
    }
}
