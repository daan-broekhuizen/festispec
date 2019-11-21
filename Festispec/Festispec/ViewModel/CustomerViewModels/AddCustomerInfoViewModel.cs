using Festispec.Service;
using Festispec.Validators;
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
    public class AddCustomerInfoViewModel : ViewModelBase
    {
        private NavigationService _navigationService;
        private CustomerValidator _customerValidator;

        public CustomerViewModel Customer { get; set; }
        public ICommand NextPageCommand { get; set; }

        #region ErrorProperties
        private string _customerError;
        public string CustomerError
        {
            get => _customerError;
            set
            {
                _customerError = value;
                RaisePropertyChanged("CustomerError");
            }
        }

        private string _adresError;
        public string AdresError
        {
            get => _adresError;
            set
            {
                _adresError = value;
                RaisePropertyChanged("AdresError");
            }
        } 
        #endregion

        public AddCustomerInfoViewModel(NavigationService service)
        {
            _navigationService = service;
            _customerValidator = new CustomerValidator();

            //If customer is passed through navigation service
            //set customer to the service parameter
            if (service.Parameter is CustomerViewModel)
                Customer = service.Parameter as CustomerViewModel;
            else
                Customer = new CustomerViewModel();

            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            //Validate input and show relevant input errors
            List<ValidationFailure> errors = _customerValidator.Validate(Customer).Errors.ToList();
            ValidationFailure customerError = errors.Where(e => e.PropertyName.Equals("Name") ||
                                                                e.PropertyName.Equals("KvK")).FirstOrDefault();
            ValidationFailure adresError = errors.Where(e => e.PropertyName.Equals("PostalCode") ||
                                                             e.PropertyName.Equals("HouseNumber") ||
                                                             e.PropertyName.Equals("Streetname") ||
                                                             e.PropertyName.Equals("City")).FirstOrDefault();
            //If succesfull navigate to next page else update error properties
            if (customerError == null && adresError == null)
                _navigationService.NavigateTo("AddContactInfo", Customer);
            else
            {
                if (customerError != null)
                    CustomerError = customerError.ErrorMessage;
                else
                    CustomerError = "";
                if (adresError != null)
                    AdresError = adresError.ErrorMessage;
                else
                    AdresError = "";
            }
        }
    }
}
