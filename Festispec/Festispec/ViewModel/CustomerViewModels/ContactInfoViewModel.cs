using Festispec.Service;
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
    public class ContactInfoViewModel : ViewModelBase
    {
        public ICommand ShowCustomerInfoCommand { get; set; }
        public ICommand ShowContactPeopleCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public CustomerViewModel CustomerVM { get; set; }

        private NavigationService _navigationService;

        public ContactInfoViewModel(NavigationService service)
        {
            _navigationService = service;
            //get customer from navigationservice
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;

            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
            ShowContactPeopleCommand = new RelayCommand(ShowContactPeople);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
        }

        private void ShowAddJob()
        {
            throw new NotImplementedException();
        }

        private void ShowContactPeople()
        {
            _navigationService.NavigateTo("ContactPeople", CustomerVM);
        }

        private void ShowCustomerInfo()
        {
            _navigationService.NavigateTo("CustomerInfo", CustomerVM);

        }
    }
}
