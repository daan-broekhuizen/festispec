using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.CustomerViewModels
{
    public interface IAddContactPersonViewModel
    {
        CustomerViewModel CustomerViewModel { get; set; }
        ContactPersonViewModel ContactPersonViewModel { get; set; }
        string NameError { get; set; }
        string TelephoneError { get; set; }
        string EmailError { get; set; }
        void SaveCustomer();
        bool CanSaveCustomer();
        void SaveContactPerson();
    }
}
