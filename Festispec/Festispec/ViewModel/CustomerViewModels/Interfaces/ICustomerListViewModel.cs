using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public interface ICustomerListViewModel
    {
        ObservableCollection<CustomerViewModel> Customers { get; set; }
        List<CustomerViewModel> FilteredCustomers { get; set; }
        CustomerViewModel SelectedCustomer { get; set; }

        ICommand ShowAddCustomerCommand { get; set; }
    }
}
