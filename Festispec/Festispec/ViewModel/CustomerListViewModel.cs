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
        public List<CustomerViewModel> Customers { get; set; }
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
        public string SelectedBox { get; set; }

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
            Customers = CustomerRepository.GetFilteredKlanten(FilterCustomer).Select(c => new CustomerViewModel(c)).ToList();
        }

        public void SortCustomers(string sort)
        {
            String _sort = sort;

            if (sort.Equals("A - Z"))
            {
                Customers = null;
            }
        }

    }
}
