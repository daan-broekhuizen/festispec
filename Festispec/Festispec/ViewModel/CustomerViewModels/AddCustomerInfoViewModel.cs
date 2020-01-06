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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        private CustomerValidator _customerValidator;
        public AddCustomerInfoViewModel(NavigationService service, CustomerRepository repo) : base(service)
        {
            if (CustomerVM == null) 
                CustomerVM = new CustomerViewModel();
            _customerValidator = new CustomerValidator();

            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            //Validate input and show relevant input errors
            List<ValidationFailure> errors = _customerValidator.Validate(CustomerVM).Errors
                .Where(c => c.PropertyName != "Telephone" && 
                            c.PropertyName != "Email" && 
                            c.PropertyName != "Website").ToList();

            //If succesfull navigate to next page else update error properties
            if (errors.Count != 0)
                ErrorMessage = errors[0].ErrorMessage;
            else
                _navigationService.NavigateTo("AddContactInfo", CustomerVM);
        }
    }
}
