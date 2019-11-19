using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class ApplicationViewModel
    {
        public ICommand ShowLoginCommand { get; set; }
        private NavigationService _navigationService;
        public ApplicationViewModel(NavigationService service)
        {
            _navigationService = service;
            ShowLoginCommand = new RelayCommand(ShowLogin);
        }

        private void ShowLogin()
        {
            _navigationService.NavigateApp("Login", null);
        }
    }
}
