using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class CustomerListViewModel : ViewModelBase
    {
        public List<Customer> Customers { get; set; }
        public CustomerListViewModel()
        {
            Customers = new List<Customer>
            {
                new Customer { Name = "C1", Adres = "NL"},
                new Customer { Name = "C1", Adres = "BE"}
            };
        }
    }
}
