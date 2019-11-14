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
    public class AddCustomerInfoViewModel : ViewModelBase
    {
        private NavigationService _navigationService;
        public CustomerViewModel Customer { get; set; }
        public ICommand NextPageCommand { get; set; }
        public AddCustomerInfoViewModel(NavigationService service)
        {
            _navigationService = service;

            if (service.Parameter is CustomerViewModel)
                Customer = service.Parameter as CustomerViewModel;
            else
                Customer = new CustomerViewModel();

            NextPageCommand = new RelayCommand(NextPage, CanNavigate);
        }

        private bool CanNavigate()
        {
            return Customer.KvK.Length == 8 &&
                   Customer.Name != null &&
                   Customer.PostalCode != null &&
                   Customer.HouseNumber != null;
        }

        private void NextPage()
        {
            _navigationService.NavigateTo("AddContactInfo", Customer);
        }
    }
}
