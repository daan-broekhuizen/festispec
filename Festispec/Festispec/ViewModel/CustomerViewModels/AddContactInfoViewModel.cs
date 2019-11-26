using Festispec.Service;
using Festispec.Validators;
using Festispec.ViewModel.CustomerViewModels;
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
    public class AddContactInfoViewModel : CustomerViewModelBase
    {
        public ICommand NextPageCommand { get; set; }

        //Properties for errors
        #region ErrorMessages
        private string _telephoneError;
        public string TelephoneError
        {
            get => _telephoneError;
            set
            {
                _telephoneError = value;
                RaisePropertyChanged("TelephoneError");
            }
        }

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                RaisePropertyChanged("EmailError");
            }
        } 
        #endregion

        public AddContactInfoViewModel(NavigationService service) : base(service)
        {
            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            //Validate input and display relevant errors
            List<ValidationFailure> errors =  new CustomerValidator().Validate(CustomerVM).Errors.ToList();
            ValidationFailure telephoneError = errors.Where(e => e.PropertyName.Equals("Telephone")).FirstOrDefault();
            ValidationFailure emailError = errors.Where(e => e.PropertyName.Equals("Email")).FirstOrDefault();

            if (telephoneError == null && emailError == null)
                _navigationService.NavigateTo("AddContactPerson", CustomerVM);

            if (telephoneError != null)
                TelephoneError = telephoneError.ErrorMessage;
            else
                TelephoneError = "";

            if (emailError != null)
                EmailError = emailError.ErrorMessage;
            else
                EmailError = "";
        }
    }
}
