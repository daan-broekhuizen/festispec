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
    public class ApplicationViewModel : NavigatableViewModel
    {
        public ICommand ShowLoginCommand { get; set; }
        public ApplicationViewModel(NavigationService service) : base(service)
        {
            ShowLoginCommand = new RelayCommand(ShowLogin);
        }

        private void ShowLogin()
        {
            _navigationService.ApplicationNavigateTo("Login", null);
        }
    }
}
