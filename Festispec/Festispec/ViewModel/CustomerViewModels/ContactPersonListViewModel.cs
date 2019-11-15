using Festispec.Service;
using Festispec.Validators;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class ContactPersonListViewModel : ViewModelBase
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactInfoCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }
        public ICommand CreateContactPersonCommand { get; set; }
        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }
        public CustomerViewModel CustomerVM { get; set; }

        private ContactPersonViewModel _selectedContact;
        public ContactPersonViewModel SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                RaisePropertyChanged("SelectedContact");
            }
        }

        private NavigationService _navigationService;
        private CustomerRepository _customerRepository;

        public ContactPersonListViewModel(NavigationService service, CustomerRepository repo)
        {
            _customerRepository = repo;
            _navigationService = service;
            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;


            CreateContactPersonCommand = new RelayCommand(CreateContactPerson);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson);
            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
        }

        private void CreateContactPerson()
        {
            SelectedContact = new ContactPersonViewModel();
            CustomerVM.Contacts.Add(SelectedContact);
        }

        private void SaveContactPerson()
        {
            if(SelectedContact == null)
            {
                Messenger.Default.Send("Selecteer een contactpersoon", this.GetHashCode());
                return;
            }
            ValidationResult result = new ContactPersonValidator().Validate(SelectedContact);
            if (!result.IsValid)
                Messenger.Default.Send(result.ToString(), this.GetHashCode());
            else
            {
                Contactpersoon newEntity = new Contactpersoon()
                {
                    Voornaam = SelectedContact.Name,
                    Tussenvoegsel = SelectedContact.Name,
                    Achternaam = SelectedContact.Name,
                    Email = SelectedContact.Email,
                    Telefoon = SelectedContact.Telephone,
                    Notities = SelectedContact.Note,
                    KlantID = CustomerVM.KvK,
                    Laatste_weiziging = DateTime.Now
                };

                Klant klant = _customerRepository.GetCustomers().Where(c => c.KvK_nummer == CustomerVM.KvK).FirstOrDefault();
                if (klant != null && klant.Contactpersoon.Where(c => c.Voornaam == newEntity.Voornaam).FirstOrDefault() == null)
                    _customerRepository.CreateContactPerson(newEntity);
                if (klant != null && klant.Contactpersoon.Where(c => c.Voornaam == newEntity.Voornaam).FirstOrDefault() != null)
                    _customerRepository.UpdateContactPerson(newEntity);
                Messenger.Default.Send("Contactpersoon opgeslagen", this.GetHashCode());

            }
        }

        private void ShowContactInfo() => _navigationService.NavigateTo("ContactInfo", CustomerVM);
        private void ShowCustomerInfo() => _navigationService.NavigateTo("CustomerInfo", CustomerVM);
    }
}
