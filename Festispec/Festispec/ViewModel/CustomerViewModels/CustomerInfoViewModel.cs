using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Converters;
using Festispec.Validators;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class CustomerInfoViewModel : ViewModelBase
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactInfoCommand { get; set; }
        public ICommand ShowContactPeopleCommand { get; set; }
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public CustomerViewModel CustomerVM { get; set; }

        private NavigationService _navigationService;
        private CustomerValidator _customerValidator;
        private CustomerRepository _customerRepository;

        public CustomerInfoViewModel(NavigationService service, CustomerRepository repo)
        {
            _customerRepository = repo;
            _navigationService = service;
            _customerValidator = new CustomerValidator();

            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;

            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactPeopleCommand = new RelayCommand(ShowContactPeople);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            SaveCustomerCommand = new RelayCommand(SaveCustomer);
        }

        private void SaveCustomer()
        {
            //Get validation errors, exclude kvk error
            ValidationResult result = _customerValidator.Validate(CustomerVM);
            if(result.Errors.Where(e => !(e.PropertyName.Equals("KvK"))).Count() == 0)
            {
                //if validated update customer
                _customerRepository.UpdateCustomer(new Klant()
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
                });

                Messenger.Default.Send("Wijzigingen opgeslagen", this.GetHashCode());

            }
            else
            {
                //Get error messages as string
                string message = "";
                foreach(ValidationFailure failure in result.Errors.Where(e => !(e.PropertyName.Equals("KvK"))))
                    message += (failure.ErrorMessage + "\n");
                //Use messenger to send error message to view
                //(Hashcode to match view and viewmodel - see code behind)
                Messenger.Default.Send(message, this.GetHashCode());
            }
        }
        private void ShowAddJob() { }
        private void ShowContactPeople() => _navigationService.NavigateTo("ContactPersons", CustomerVM);
        private void ShowCustomerInfo() => _navigationService.NavigateTo("CustomerInfo", CustomerVM);
        private void ShowContactInfo() => _navigationService.NavigateTo("ContactInfo", CustomerVM);
    }
}
