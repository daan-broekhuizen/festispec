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
    public class ContactPersonListViewModel
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactInfoCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public CustomerViewModel CustomerVM { get; set; }

        private NavigationService _navigationService;

        public ContactPersonListViewModel(NavigationService service)
        {
            _navigationService = service;
            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;

            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactInfoCommand = new RelayCommand(ShowContactInfo);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
        }

        private void ShowAddJob()
        {
            throw new NotImplementedException();
        }

        private void ShowContactInfo()
        {
            _navigationService.NavigateTo("ContactInfo", CustomerVM);
        }

        private void ShowCustomerInfo()
        {
            _navigationService.NavigateTo("CustomerInfo", CustomerVM);
        }
    }
}
