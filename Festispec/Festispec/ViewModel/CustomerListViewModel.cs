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
        private List<CustomerViewModel> customers;

        public List<CustomerViewModel> Customers 
        { 
            get => customers;
            set
            {
                customers = value;
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
            }
        }
        private CustomerViewModel _selectedCustomer;

        public string FilterCustomer { get; set; }

        private ComboBoxItem _selectedBox;
        public ComboBoxItem SelectedBox
        {
            get
            {
                return _selectedBox;
            }
            set
            {
                _selectedBox = value;
                SortCustomers();
            }

        }

        private CustomerRepository CustomerRepository { get; set; }
        private NavigationService _navigationService;

        public ICommand SearchCustomer { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public ICommand ShowCustomerInfoCommand { get; set; }

        public CustomerListViewModel(NavigationService service)
        {
            _navigationService = service;
            CustomerRepository = new CustomerRepository();
            FilterCustomer = "";
            Customers = CustomerRepository.GetKlanten().Select(c => new CustomerViewModel(c)).ToList();
            FilteredCustomers = Customers;

            SearchCustomer = new RelayCommand(FilterCustomers);
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
            ShowCustomerInfoCommand = new RelayCommand(ShowCustomerInfo);
        }

        private void ShowCustomerInfo()
        {
            if (SelectedCustomer != null)
                _navigationService.NavigateTo("CustomerInfo", SelectedCustomer);
        }

        private void ShowAddCustomer()
        {
            _navigationService.NavigateTo("AddCustomerInfo");
        }

        public void FilterCustomers()
        {
            if (SelectedBox != null)
            {
                switch (SelectedBox.Content)
                {
                    case "A - Z":
                        FilteredCustomers = GetFilteredKlantenASC();
                        break;
                    case "Z - A":
                        FilteredCustomers = GetFilteredKlantenDESC();
                        break;
                }
            }
            else
            {
                FilteredCustomers = GetFilteredKlanten();

            }
        }



        public void SortCustomers()
        {
            if (FilterCustomer != null)
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            FilteredCustomers = GetFilteredKlantenASC();
                            break;
                        case "Z - A":
                            FilteredCustomers = GetFilteredKlantenDESC();
                            break;
                    }
                }
                else
                {
                    FilteredCustomers = GetFilteredKlanten();


                }
            }
            else
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            FilteredCustomers = GetKlantenASC();
                            break;
                        case "Z - A":
                            FilteredCustomers = GetKlantenDESC();
                            break;
                    }
                }
                else
                {
                    FilteredCustomers = Customers;

                }
            }
        }

        public List<CustomerViewModel> GetFilteredKlanten()
        {
            FilteredCustomers = Customers.Where(e => e.Name.Contains(FilterCustomer)).ToList();
            return FilteredCustomers;
        }
        public List<CustomerViewModel> GetFilteredKlantenASC()
        {
            FilteredCustomers = Customers.Where(e => e.Name.Contains(FilterCustomer)).OrderBy(e => e.Name).ToList();
            return FilteredCustomers;
        }
        public List<CustomerViewModel> GetFilteredKlantenDESC()
        {
            FilteredCustomers = Customers.Where(e => e.Name.Contains(FilterCustomer)).OrderByDescending(e => e.Name).ToList();
            return FilteredCustomers;
        }
        public List<CustomerViewModel> GetKlantenASC()
        {
            FilteredCustomers = Customers.OrderBy(e => e.Name).ToList();
            return FilteredCustomers;
        }
        public List<CustomerViewModel> GetKlantenDESC()
        {
            FilteredCustomers = Customers.OrderByDescending(e => e.Name).ToList();
            return FilteredCustomers;
        }
    }
}
