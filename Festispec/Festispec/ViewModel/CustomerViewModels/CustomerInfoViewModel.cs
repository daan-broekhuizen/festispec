using Festispec.Service;
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

        public CustomerInfoViewModel(NavigationService service)
        {
            _customerRepository = new CustomerRepository();
            _customerValidator = new CustomerValidator();
            _navigationService = service;
            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;

            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactPeopleCommand = new RelayCommand(ShowContactPeople);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
        }

        private bool CanSaveCustomer() 
        {
            List<ValidationFailure> errors = _customerValidator.Validate(CustomerVM).Errors.ToList();
            return errors.Where(e => !(e.PropertyName.Equals("KvK"))).Count() == 0;
        }

        private void SaveCustomer()
        {
            _customerRepository.UpdateCustomer(new Klant()
            {
                Naam = CustomerVM.Name,
                Email = CustomerVM.Email,
                Huisnummer = CustomerVM.HouseNumber,
                KvK_nummer = CustomerVM.KvK,
                Postcode = CustomerVM.PostalCode,
                Website = CustomerVM.Website,
                Laatste_weiziging = DateTime.Now,
                Telefoonnummer = CustomerVM.Telephone
            });
        }

        private void ShowAddJob() { }

        private void ShowContactPeople()
        {
            _navigationService.NavigateTo("ContactPersons", CustomerVM);
        }
        private void ShowCustomerInfo()
        {
            _navigationService.NavigateTo("CustomerInfo", CustomerVM);
        }
        private void ShowContactInfo()
        {
            _navigationService.NavigateTo("ContactInfo", CustomerVM);
        }
    }
}
