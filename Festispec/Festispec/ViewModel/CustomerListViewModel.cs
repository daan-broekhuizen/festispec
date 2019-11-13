using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Service;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class CustomerListViewModel : ViewModelBase
    {
        private List<Klant> _customers;

        public List<Klant> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        public string FilterCustomer { get; set; }
        public string SelectedBox { get; set; }

        private CustomerRepository CustomerRepository { get; set; }
        private NavigationService _navigationService;

        public ICommand SearchCustomer { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public CustomerListViewModel(NavigationService service)
        {
            _navigationService = service;
            CustomerRepository = new CustomerRepository();
            FilterCustomer = "";
            Customers = CustomerRepository.GetKlanten();

            SearchCustomer = new RelayCommand(FilterCustomers);
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
        }

        private void ShowAddCustomer()
        {
            _navigationService.NavigateTo("AddCustomerInfo");
        }

        public void FilterCustomers()
        {
            Customers = CustomerRepository.GetFilteredKlanten(FilterCustomer);
        }

        public void SortCustomers(string sort)
        {
            String _sort = sort;

            if(sort.Equals("A - Z"))
            {
                Customers = null;
            }
        }

    }
}
