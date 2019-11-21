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
    public class CustomerListViewModel : ViewModelBase, ICustomerListViewModel
    {
        #region BindingProperties
        private List<CustomerViewModel> _customers;
        public List<CustomerViewModel> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        private List<CustomerViewModel> filteredcustomers;

        public List<CustomerViewModel> FilteredCustomers
        {
            get => filteredcustomers;
            set
            {
                filteredcustomers = value;
                RaisePropertyChanged("FilteredCustomers");
            }
        }
        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
                ShowCustomerInfo();
            }
        }
        private CustomerViewModel _selectedCustomer;

        public string FilterCustomer { get; set; }

        private ComboBoxItem _selectedBox;
        public ComboBoxItem SelectedBox
        {
            get => _selectedBox;
            set
            {
                _selectedBox = value;
                SortCustomers();
            }
        } 
        #endregion

        private CustomerRepository _customerRepository;
        private NavigationService _navigationService;

        public ICommand SearchCustomer { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public CustomerListViewModel(NavigationService service, CustomerRepository repo)
        {
            _navigationService = service;
            _customerRepository = repo;

            Customers = _customerRepository.GetCustomers().Select(c => new CustomerViewModel(c)).ToList();
            FilteredCustomers = Customers;
            FilterCustomer = "";

            SearchCustomer = new RelayCommand(FilterCustomers);
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
        }

        private void ShowCustomerInfo()
        {
            if (SelectedCustomer != null)
                _navigationService.NavigateTo("CustomerInfo", SelectedCustomer);
        }

        private void ShowAddCustomer() => _navigationService.NavigateTo("AddCustomerInfo", new CustomerViewModel());

        public void FilterCustomers()
        {
            FilteredCustomers = Customers.Where(e => e.Name.Contains(FilterCustomer)).ToList();
            SortCustomers();
        }

        public void SortCustomers()
        {
            if (SelectedBox != null)
            {
                switch (SelectedBox.Content)
                {
                    case "Naam oplopend":
                        FilteredCustomers = FilteredCustomers.OrderBy(e => e.Name).ToList();
                        break;
                    case "Naam aflopend":
                        FilteredCustomers = FilteredCustomers.OrderByDescending(e => e.Name).ToList();
                        break;
                    case "KvK oplopend":
                        FilteredCustomers = FilteredCustomers.OrderBy(e => e.KvK).ToList();
                        break;
                    case "KvK aflopend":
                        FilteredCustomers = FilteredCustomers.OrderByDescending(e => e.KvK).ToList();
                        break;
                }
            }
        }
    }
}
