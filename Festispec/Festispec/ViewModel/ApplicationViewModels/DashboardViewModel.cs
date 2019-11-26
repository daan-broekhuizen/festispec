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
    public class DashboardViewModel : NavigatableViewModel
    {
        public ICommand ShowCustomersCommand { get; set; }
        public ICommand ShowJobsCommand { get; set; }
        public ICommand ShowQuotationsCommand { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public ICommand ShowAddQuotationCommand { get; set; }
        public ICommand ShowPlanningCommand { get; set; }
        public ICommand ShowScheduleCommand { get; set; }
        public ICommand ShowMessagesCommand { get; set; }

        public DashboardViewModel(NavigationService service) : base(service)
        {
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
            ShowCustomersCommand = new RelayCommand(ShowCustomers);
        }

        private void ShowAddCustomer() => _navigationService.NavigateTo("AddCustomerInfo");
        private void ShowCustomers() => _navigationService.NavigateTo("Customers");
    }
}
