using Festispec.Service;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
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
    public class AddCustomerInfoViewModel : CustomerViewModelBase
    {
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

        private CustomerValidator _customerValidator;
        public AddCustomerInfoViewModel(NavigationService service, CustomerRepository repo) : base(service)
        {
            _customerValidator = new CustomerValidator(repo);
            if (CustomerVM == null) CustomerVM = new CustomerViewModel();
            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            //Validate input and show relevant input errors
            List<ValidationFailure> errors =  _customerValidator.Validate(CustomerVM).Errors.ToList();
            ValidationFailure customerError = errors.Where(e => e.PropertyName.Equals("Name") ||
                                                                e.PropertyName.Equals("KvK")).FirstOrDefault();
            ValidationFailure adresError = errors.Where(e => e.PropertyName.Equals("PostalCode") ||
                                                             e.PropertyName.Equals("HouseNumber") ||
                                                             e.PropertyName.Equals("Streetname") ||
                                                             e.PropertyName.Equals("City")).FirstOrDefault();
            //If succesfull navigate to next page else update error properties
            if (customerError == null && adresError == null)
                _navigationService.NavigateTo("AddContactInfo", CustomerVM);
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
