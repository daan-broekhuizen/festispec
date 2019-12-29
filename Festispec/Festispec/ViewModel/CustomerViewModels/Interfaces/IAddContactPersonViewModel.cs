using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.CustomerViewModels
{
    public interface IAddContactPersonViewModel
    {
        CustomerViewModel CustomerVM { get; set; }
        ContactPersonViewModel ContactPersonViewModel { get; set; }

        ICommand SaveCustomerCommand { get; set; }
        ICommand SaveContactPersonCommand { get; set; }

        string FirstNameError { get; set; }
        string LastNameError { get; set; }

        string TelephoneError { get; set; }
        string EmailError { get; set; }
    }
}
