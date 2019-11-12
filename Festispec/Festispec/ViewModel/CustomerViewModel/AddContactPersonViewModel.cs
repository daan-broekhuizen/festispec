using Festispec.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class AddContactPersonViewModel
    {
        public CustomerViewModel CustomerViewModel { get; set; }
        public ContactPersonViewModel ContactPersonViewModel { get; set; }
        public ICommand SaveCustomerCommand { get; set; }
        public ICommand SaveContactPersonCommand { get; set; }

        private NavigationService _navigationService;
        public AddContactPersonViewModel(NavigationService service)
        {
            _navigationService = service;
            ContactPersonViewModel = new ContactPersonViewModel();

            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerViewModel = service.Parameter as CustomerViewModel;

            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            SaveContactPersonCommand = new RelayCommand(SaveContactPerson, CanSaveContactPerson);
        }

        private void SaveCustomer()
        {

        }

        private bool CanSaveCustomer()
        {
            return true;
        }

        private void SaveContactPerson()
        {

        }

        private bool CanSaveContactPerson()
        {
            return true;
        }
    }
}
