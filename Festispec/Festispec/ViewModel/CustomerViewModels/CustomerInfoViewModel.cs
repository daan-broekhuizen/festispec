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
    public class CustomerInfoViewModel : CustomerViewModelBase
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactInfoCommand { get; set; }
        public ICommand ShowContactPeopleCommand { get; set; }
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }

        public CustomerInfoViewModel(NavigationService service, CustomerRepository repo) : base(service, repo)
        {
            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactPeopleCommand = new RelayCommand(ShowContactPeople);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            SaveCustomerCommand = new RelayCommand(SaveCustomer);
        }

        private void SaveCustomer()
        {
            //Get validation errors, exclude kvk error
            ValidationResult result =  new CustomerValidator().Validate(CustomerVM);
            if(result.Errors.Count() == 0)
            {
                //if validated update customer
                _customerRepository.UpdateCustomer(new Klant()
                {
                    KlantID = CustomerVM.Id,
                    Naam = CustomerVM.Name,
                    Email = CustomerVM.Email,
                    Huisnummer = CustomerVM.HouseNumber,
                    KvKNummer = CustomerVM.KvK,
                    Vestigingnummer = CustomerVM.Branchnumber,
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
                foreach(ValidationFailure failure in result.Errors)
                    message += (failure.ErrorMessage + "\n");
                //Use messenger to send error message to view
                //(Hashcode to match view and viewmodel - see code behind)
                Messenger.Default.Send(message, this.GetHashCode());
            }
        }
        private void ShowAddJob() => _navigationService.NavigateTo("AddJob", new JobViewModel() { CustomerID = CustomerVM.Id });
        private void ShowContactPeople() => _navigationService.NavigateTo("ContactPersons", CustomerVM);
        private void ShowCustomerInfo() => _navigationService.NavigateTo("CustomerInfo", CustomerVM);
        private void ShowContactInfo() => _navigationService.NavigateTo("ContactInfo", CustomerVM);
    }
}
