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

            NextPageCommand = new RelayCommand(NextPage);
        }

        private void NextPage()
        {
            _navigationService.NavigateTo("AddContactInfo", Customer);
        }
    }
}
