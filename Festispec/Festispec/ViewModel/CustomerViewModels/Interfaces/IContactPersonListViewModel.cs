using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.CustomerViewModels.Interfaces
{
    public interface IContactPersonListViewModel
    {
        ObservableCollection<ContactPersonViewModel> Contacts { get; set; }
        CustomerViewModel CustomerVM { get; set; }
        ContactPersonViewModel SelectedContact { get; set; }

        void CreateContactPerson();
        void SaveContactPerson();


    }
}
