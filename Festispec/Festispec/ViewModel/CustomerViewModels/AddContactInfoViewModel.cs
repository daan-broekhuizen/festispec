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
    public class AddContactInfoViewModel : ViewModelBase
    {
        public CustomerViewModel CustomerViewModel { get; set; }
        public ICommand NextPageCommand { get; set; }
        private NavigationService _navigationService;

        public AddContactInfoViewModel(NavigationService service)
        {
            _navigationService = service;
            if (service.Parameter is CustomerViewModel)
                CustomerViewModel = service.Parameter as CustomerViewModel;
            NextPageCommand = new RelayCommand(NextPage, CanNavigate);
        }

        private bool CanNavigate()
        {
            return CustomerViewModel.Email != null;
        }

        private void NextPage()
        {
            _navigationService.NavigateTo("AddContactPerson", CustomerViewModel);
        }
    }
}
