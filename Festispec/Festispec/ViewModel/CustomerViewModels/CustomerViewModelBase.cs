using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Service;
using FestiSpec.Domain.Repositories;

namespace Festispec.ViewModel.CustomerViewModels
{
    public abstract class CustomerViewModelBase : NavigatableViewModel
    {
        public CustomerViewModel CustomerVM { get; set; }
        protected CustomerRepository _customerRepository;

        public CustomerViewModelBase(NavigationService service, CustomerRepository repo) : base(service)
        {
            _customerRepository = repo;
            //Get customer from navigation service
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;
        }

        public CustomerViewModelBase(NavigationService service) : base(service)
        {
            //Get customer from navigation service
            if (service.Parameter is CustomerViewModel)
                CustomerVM = service.Parameter as CustomerViewModel;
        }
    }
}
