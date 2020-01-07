using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Converters;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class AddContactPersonViewModel : CustomerViewModelBase, IAddContactPersonViewModel
    {
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

        private string _firstNameError;
        public string FirstNameError
        {
            get => _firstNameError;
            set
            {
                _firstNameError = value;
                RaisePropertyChanged("FirstNameError");
            }
        }

        private string _lastNameError;
        public string LastNameError
        {
            get => _lastNameError;
            set
            {
                _lastNameError = value;
                RaisePropertyChanged("LastNameError");
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
            SaveContactPersonCommand = new RelayCommand(AddContactPerson);
        }

        private void SaveCustomer()
        {
            //Create Customer && add to db
            Klant klant = new Klant()
            {
                Naam = CustomerVM.Name,
                Email = CustomerVM.Email,
                Vestigingnummer = CustomerVM.Branchnumber,
                Huisnummer = CustomerVM.HouseNumber,
                KvKNummer = CustomerVM.KvK,
                Straatnaam = CustomerVM.Streetname,
                Stad = CustomerVM.City,
                Website = CustomerVM.Website,
                LaatsteWijziging = DateTime.Now,
                Telefoonnummer = CustomerVM.Telephone,
                KlantLogo = ImageByteConverter.PngImageToBytes(CustomerVM.Logo)
            };

            klant = _customerRepository.CreateCustomer(klant);
            CustomerVM.SetCustomer(klant);
            SaveContacts();

            //Notify & Navigate to customers
            Messenger.Default.Send("Klantgegevens opgeslagen", this.GetHashCode());
            _navigationService.NavigateTo("Customers");
        }
        private bool CanSaveCustomer() => new CustomerValidator().Validate(CustomerVM).IsValid;
        private void AddContactPerson()
        {
            //Validate & get relevant errors
            List<ValidationFailure> errors = new ContactPersonValidator().Validate(ContactPersonViewModel).Errors.ToList();
            ValidationFailure telephoneError = errors.FirstOrDefault(e => e.PropertyName.Equals("Telephone"));
            ValidationFailure emailError = errors.FirstOrDefault(e => e.PropertyName.Equals("Email"));
            ValidationFailure firstNameError = errors.FirstOrDefault(e => e.PropertyName.Equals("FirstName"));
            ValidationFailure lastNameError = errors.FirstOrDefault(e => e.PropertyName.Equals("LastName"));

            if (errors.Count == 0)
            {
                //Add contact to customervm and create new contact
                CustomerVM.Contacts.Add(ContactPersonViewModel);
                Messenger.Default.Send("Contactpersoon opgeslagen", this.GetHashCode());
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

            if (firstNameError != null)
                FirstNameError = firstNameError.ErrorMessage;
            else
                FirstNameError = "";

            if (lastNameError != null)
                LastNameError = lastNameError.ErrorMessage;
            else
                LastNameError = "";
        }
        private void SaveContacts()
        {
            //Save contacts
            CustomerVM.Contacts.ToList().ForEach(c =>
            {
                Contactpersoon contact = new Contactpersoon()
                {
                    Voornaam = c.FirstName,
                    Tussenvoegsel = c.Infix,
                    Achternaam = c.LastName,
                    Email = c.Email,
                    Telefoon = c.Telephone,
                    Notities = c.Note,
                    Rol = c.Role,
                    KlantID = CustomerVM.Id,
                    LaatsteWijziging = DateTime.Now
                };

                contact = _customerRepository.CreateContactPerson(contact);
                c.SetContactPerson(contact);
            });
        }
    }
}
