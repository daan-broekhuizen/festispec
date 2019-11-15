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
        private string _customerError;
        private string _adresError;

        public CustomerViewModel Customer { get; set; }
        public ICommand NextPageCommand { get; set; }
        public string CustomerError 
        {
            get => _customerError;
            set 
            {
                _customerError = value;
                RaisePropertyChanged("CustomerError");
            } 
        }
        public string AdresError 
        {
            get => _adresError;
            set
            {
                _adresError = value;
                RaisePropertyChanged("AdresError");
            }
        }

        public AddCustomerInfoViewModel(NavigationService service)
        {
            _navigationService = service;
            _customerValidator = new CustomerValidator();

            if (service.Parameter is CustomerViewModel)
                Customer = service.Parameter as CustomerViewModel;
            else
                Customer = new CustomerViewModel();

            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            List<ValidationFailure> errors = _customerValidator.Validate(Customer).Errors.ToList();
            ValidationFailure customerError = errors.Where(e => e.PropertyName.Equals("Name") ||
                                                                e.PropertyName.Equals("KvK")).FirstOrDefault();
            ValidationFailure adresError = errors.Where(e => e.PropertyName.Equals("PostalCode") ||
                                                             e.PropertyName.Equals("HouseNumber")).FirstOrDefault();

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
