using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public interface ICustomerListViewModel
    {
        List<CustomerViewModel> Customers { get; set; }
        List<CustomerViewModel> FilteredCustomers { get; set; }
        CustomerViewModel SelectedCustomer { get; set; }
        string FilterCustomer { get; set; }
        ComboBoxItem SelectedBox { get; set; }

        ICommand SearchCustomer { get; set; }
        ICommand ShowAddCustomerCommand { get; set; }
    }
}
