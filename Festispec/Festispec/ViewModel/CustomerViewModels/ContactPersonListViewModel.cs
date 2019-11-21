using Festispec.Model;
using Festispec.Service;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
using Festispec.ViewModel.CustomerViewModels.Interfaces;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class ContactPersonListViewModel : CustomerViewModelBase, IContactPersonListViewModel
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactInfoCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }
        public ICommand CreateContactPersonCommand { get; set; }

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
        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }

        public ContactPersonListViewModel(NavigationService service, CustomerRepository repo) : base(service, repo)
        {
            CreateContactPersonCommand = new RelayCommand(CreateContactPerson);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson);
            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
        }

        public void CreateContactPerson()
        {
            SelectedContact = new ContactPersonViewModel();
            CustomerVM.Contacts.Add(SelectedContact);
        }
        public void SaveContactPerson()
        {
            if(SelectedContact == null)
            {
                Messenger.Default.Send("Selecteer een contactpersoon", this.GetHashCode());
                return;
            }

            ValidationResult result = new ContactPersonValidator().Validate(SelectedContact);
            if (!result.IsValid)
            {
                Messenger.Default.Send(result.ToString(), this.GetHashCode());
                return;
            }

            Contactpersoon newEntity = new Contactpersoon()
            {
                Voornaam = SelectedContact.Name,
                Tussenvoegsel = SelectedContact.Name,
                Achternaam = SelectedContact.Name,
                Email = SelectedContact.Email,
                Telefoon = SelectedContact.Telephone,
                Notities = SelectedContact.Note,
                KlantID = CustomerVM.KvK,
                LaatsteWijziging = DateTime.Now
            };

            Klant klant = _customerRepository.GetCustomers().Where(c => c.KvKNummer == CustomerVM.KvK).FirstOrDefault();
            if (klant == null) return;
            if (klant.Contactpersoon.Where(c => c.ContactpersoonID == SelectedContact.Id).FirstOrDefault() == null)
                _customerRepository.CreateContactPerson(newEntity);
            else if (klant.Contactpersoon.Where(c => c.ContactpersoonID == SelectedContact.Id).FirstOrDefault() != null)
            {
                newEntity.ContactpersoonID = SelectedContact.Id;
                _customerRepository.UpdateContactPerson(newEntity);
            }
            Messenger.Default.Send("Contactpersoon opgeslagen", this.GetHashCode());
            
        }

        private void ShowContactInfo() => _navigationService.NavigateTo("ContactInfo", CustomerVM);
        private void ShowCustomerInfo() => _navigationService.NavigateTo("CustomerInfo", CustomerVM);
    }
}
