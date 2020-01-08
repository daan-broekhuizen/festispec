using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Converters;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
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
    public class AddContactPersonViewModel : CustomerViewModelBase, IAddContactPersonViewModel
    {
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }

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

        public AddContactPersonViewModel(NavigationService service, CustomerRepository repo) : base(service, repo)
        {
            //Create empty contact
            ContactPersonViewModel = new ContactPersonViewModel();
            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson);
            PreviousPageCommand = new RelayCommand(PreviousPage);
        }

        private void PreviousPage() => _navigationService.NavigateTo("AddContactInfo", CustomerVM);
        private void SaveCustomer()
        {
            //Create Customer && add to db
            Klant klant = new Klant()
            {
                Naam = CustomerVM.Name,
                Email = CustomerVM.Email,
                Huisnummer = CustomerVM.HouseNumber,
                KvKNummer = CustomerVM.KvK,
                Straatnaam = CustomerVM.Streetname,
                Stad = CustomerVM.City,
                Website = CustomerVM.Website,
                LaatsteWijziging = DateTime.Now,
                Telefoonnummer = CustomerVM.Telephone,
                KlantLogo = ImageByteConverter.PngImageToBytes(CustomerVM.Logo)
            };
            _customerRepository.CreateCustomer(klant);

            //Create Contacts
            CustomerVM.Contacts.ToList().ForEach(c =>
            _customerRepository.CreateContactPerson(new Contactpersoon()
            {
                Voornaam = c.Name,
                Tussenvoegsel = c.Name,
                Achternaam = c.Name,
                Email = c.Email,
                Telefoon = c.Telephone,
                Notities = c.Note,
                KlantID = klant.KlantID,
                LaatsteWijziging = DateTime.Now
            }));

            //Navigate to created CustomerInfo
            _navigationService.NavigateTo("CustomerInfo", CustomerVM);
        }
        private bool CanSaveCustomer() => new CustomerValidator(_customerRepository).Validate(CustomerVM).IsValid;
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
                CustomerVM.Contacts.Add(ContactPersonViewModel);
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
