using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string Addition { get; set; }
        public string KvK { get; set; }
        public string BranchNumber { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        
        public ObservableCollection<ContactPersonViewModel> Contacts { get; set; }
    }
}
