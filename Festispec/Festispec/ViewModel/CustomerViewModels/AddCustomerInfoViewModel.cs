using Festispec.Service;
using Festispec.Utility;
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

        private ObservableDictionary<string, string> _errorMessages;
        public ObservableDictionary<string, string> ErrorMessages
        {
            get => _errorMessages;
            set
            {
                _errorMessages = value;
                RaisePropertyChanged(() => ErrorMessages);
            }
        }

        private CustomerValidator _customerValidator;
        public AddCustomerInfoViewModel(NavigationService service, CustomerRepository repo) : base(service)
        {
            if (CustomerVM == null) 
                CustomerVM = new CustomerViewModel();
            _customerValidator = new CustomerValidator();

            NextPageCommand = new RelayCommand(NextPage);
            //Init error messages
            ErrorMessages = new ObservableDictionary<string, string>()
            {
                ["User"] = "",
                ["KvK"] = "",
                ["Branchnumber"] = "",
                ["Streetname"] = "",
                ["HouseNumber"] = "",
                ["City"] = "",
                ["PostalCode"] = ""
            };

        }

        private bool CanNavigate()
        {
            //Validate input and show relevant input errors
            List<ValidationFailure> errors = _customerValidator.Validate(CustomerVM).Errors
                .Where(c => c.PropertyName != "Telephone" &&
                            c.PropertyName != "Email" &&
                            c.PropertyName != "Website").ToList();

            for (int i = 0; i < ErrorMessages.Count; i++)
            {
                string property = ErrorMessages.ElementAt(i).Key;
                ValidationFailure failure = errors.FirstOrDefault(e => e.PropertyName.Equals(property));
                if (failure != null)
                    ErrorMessages[property] = failure.ErrorMessage;
                else
                    ErrorMessages[property] = "";
            }

            RaisePropertyChanged(() => ErrorMessages);
            return errors.Count == 0;
        }

        private void NextPage()
        {
            //If valid navigate to next page else update error properties
            if (CanNavigate())
                _navigationService.NavigateTo("AddContactInfo", CustomerVM);
        }
    }
}
