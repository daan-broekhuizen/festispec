using Festispec.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.IO;
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
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/bin/opdrachten.json";
            if (File.Exists(path))
                this.ShowOfflineJobListCommand = new RelayCommand(ShowOfflineJobList);
            else
                this.ShowOfflineJobListCommand = null;
        }

        private void ShowOfflineJobList() => _navigationService.OfflineNavigation("OfflineJobList", null);
    }
}
