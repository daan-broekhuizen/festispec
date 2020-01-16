using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Service;
using Festispec.ViewModel.CustomerViewModels;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class CustomerListViewModel : CustomerViewModelBase, ICustomerListViewModel
    {
        #region BindingProperties
        public ObservableCollection<CustomerViewModel> Customers { get; set; }
        private List<CustomerViewModel> _filteredcustomers;
        public List<CustomerViewModel> FilteredCustomers
        {
            get => _filteredcustomers;
            set
            {
                _filteredcustomers = value;
                RaisePropertyChanged("FilteredCustomers");
            }
        }

        private CustomerViewModel _selectedCustomer;
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
        #endregion

        public ICommand ShowAddCustomerCommand { get; set; }
        public ICommand SortChangedCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public CustomerListViewModel(NavigationService service, CustomerRepository repo) : base(service, repo)
        {
            Customers =  new ObservableCollection<CustomerViewModel> (_customerRepository.GetCustomers().Select(c => new CustomerViewModel(c)).ToList());
            FilteredCustomers = Customers.ToList();


            SearchButtonClickCommand = new RelayCommand<string>(FilterCustomers);
            SearchTextChangedCommand = new RelayCommand<string>(FilterCustomers);
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
            SortChangedCommand = new RelayCommand<int>(ChangeSort);

        }

        private void ShowCustomerInfo()
        {
            if (SelectedCustomer != null)
                _navigationService.NavigateTo("CustomerInfo", SelectedCustomer);
        }
        private void ShowAddCustomer() => _navigationService.NavigateTo("AddCustomerInfo", new CustomerViewModel());
        private void FilterCustomers(string searchText)
        {
            FilteredCustomers = Customers.Where(e => e.Name.ToLower().Contains(searchText) || e.KvK.Contains(searchText)).ToList();
        }
        private void ChangeSort(int sortMode)
        {
            
            switch (sortMode)
            {
                case 0:
                    FilteredCustomers = FilteredCustomers.OrderBy(e => e.Name).ToList();
                    break;
                case 1:
                    FilteredCustomers = FilteredCustomers.OrderByDescending(e => e.Name).ToList();
                    break;
                case 2:
                    FilteredCustomers = FilteredCustomers.OrderBy(e => e.KvK).ToList();
                    break;
                case 3:
                    FilteredCustomers = FilteredCustomers.OrderByDescending(e => e.KvK).ToList();
                    break;
            }
            
        }
    }
}
