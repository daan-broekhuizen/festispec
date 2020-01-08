using Festispec.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.OfflineViewModels
{
    public class OfflineLoginViewModel : NavigatableViewModel
    {
        public ICommand ShowOfflineJobListCommand { get; set; }
        public OfflineLoginViewModel(NavigationService service) : base(service)
        {
            this.ShowOfflineJobListCommand = new RelayCommand(ShowOfflineJobList);
        }

        private void ShowOfflineJobList() => _navigationService.ApplicationNavigateTo("OfflineJobList", null);
    }
}
