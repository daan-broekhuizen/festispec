using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Service;

namespace Festispec.ViewModel.OfflineViewModel
{
    public class OfflineDashboardViewModel : NavigatableViewModel
    {
        public ICommand ShowJobsCommand { get; set; }
        public OfflineDashboardViewModel(NavigationService service) : base(service)
        {

        }
    }
}
