using Festispec.Service;
using Festispec.Utility.Converters;
using Festispec.Validators;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
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
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }

        //ContactPersonVM and ErrorProperties
        #region BindingProperties
        private ContactPersonViewModel _contactPersonViewModel;
        public ContactPersonViewModel ContactPersonViewModel
        {
            get => _contactPersonViewModel;
            set
            {
                _contactPersonViewModel = value;
                RaisePropertyChanged("ContactPersonViewModel");
            }
        }

        private string _nameError;
        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                RaisePropertyChanged("NameError");
            }
        }

        private string _telephoneError;
        public string TelephoneError
        {
            get => _telephoneError;
            set
            {
                _telephoneError = value;
                RaisePropertyChanged("TelephoneError");
            }
        }

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                RaisePropertyChanged("EmailError");
            }
        } 
        #endregion

        private NavigationService _navigationService;
        private CustomerRepository _customerRepo;

        public AddContactPersonViewModel(NavigationService service, CustomerRepository repo)
        {
            _customerRepo = repo;
            _navigationService = service;

            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerViewModel = service.Parameter as CustomerViewModel;

            //Create empty contact
            ContactPersonViewModel = new ContactPersonViewModel();

            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson);
        }

        private void SaveCustomer()
        {
            //Create Customer && add to db
            Klant klant = new Klant()
            {
                Naam = CustomerViewModel.Name,
                Email = CustomerViewModel.Email,
                Huisnummer = CustomerViewModel.HouseNumber,
                KvK_nummer = CustomerViewModel.KvK,
                Straatnaam = CustomerViewModel.Streetname,
                Stad = CustomerViewModel.City,
                Website = CustomerViewModel.Website,
                Laatste_weiziging = DateTime.Now,
                Telefoonnummer = CustomerViewModel.Telephone,
                Klant_logo = ImageByteConverter.ImageToBytes(CustomerViewModel.Logo)
            };
            _customerRepo.CreateCustomer(klant);

            //Create Contacts
            CustomerViewModel.Contacts.ToList().ForEach(c =>
            _customerRepo.CreateContactPerson(new Contactpersoon()
            {
                Voornaam = c.Name,
                Tussenvoegsel = c.Name,
                Achternaam = c.Name,
                Email = c.Email,
                Telefoon = c.Telephone,
                Notities = c.Note,
                KlantID = klant.KvK_nummer,
                Laatste_weiziging = DateTime.Now
            }));

            //Navigate to created CustomerInfo
            _navigationService.NavigateTo("CustomerInfo", CustomerViewModel);
        }

        private bool CanSaveCustomer() => new CustomerValidator().Validate(CustomerViewModel).IsValid;

        private void SaveContactPerson()
        {
            //Validate & get relevant errors
            List<ValidationFailure> errors = new ContactPersonValidator().Validate(ContactPersonViewModel).Errors.ToList();
            ValidationFailure telephoneError = errors.Where(e => e.PropertyName.Equals("Telephone")).FirstOrDefault();
            ValidationFailure emailError = errors.Where(e => e.PropertyName.Equals("Email")).FirstOrDefault();
            ValidationFailure nameError = errors.Where(e => e.PropertyName.Equals("Name")).FirstOrDefault();


            if (errors.Count == 0)
            {
                //Add contact to customervm and create new contact
                CustomerViewModel.Contacts.Add(ContactPersonViewModel);
                ContactPersonViewModel = new ContactPersonViewModel();
            }

            // update error message properties
            if (telephoneError != null)
                TelephoneError = telephoneError.ErrorMessage;
            else
                TelephoneError = "";

            if (emailError != null)
                EmailError = emailError.ErrorMessage;
            else
                EmailError = "";

            if (nameError != null)
                NameError = nameError.ErrorMessage;
            else
                NameError = "";
        }

    }
}
