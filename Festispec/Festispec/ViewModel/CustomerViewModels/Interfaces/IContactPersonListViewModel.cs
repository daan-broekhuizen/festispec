using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.CustomerViewModels.Interfaces
{
    public interface IContactPersonListViewModel
    {
        CustomerViewModel CustomerVM { get; set; }
        ObservableCollection<ContactPersonViewModel> Contacts { get; set; }
        ContactPersonViewModel SelectedContact { get; set; }

        ICommand ShowCustomerInfoCommand { get; set; }
        ICommand ShowContactInfoCommand { get; set; }
        ICommand SaveContactPersonCommand { get; set; }
        ICommand CreateContactPersonCommand { get; set; }
    }
}
