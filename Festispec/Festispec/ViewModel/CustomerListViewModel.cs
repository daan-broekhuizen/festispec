using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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

        public ICommand SearchCustomer { get; set; }
        public CustomerListViewModel()
        {
            CustomerRepository = new CustomerRepository();
            FilterCustomer = "";
            Customers = CustomerRepository.GetKlanten();

            SearchCustomer = new RelayCommand(FilterCustomers);
        }

        public void FilterCustomers()
        {
            if (SelectedBox != null) {
                switch (SelectedBox.Content)
                {
                    case "A - Z":
                        Customers = CustomerRepository.GetFilteredKlantenASC(FilterCustomer);
                        break;
                    case "Z - A":
                        Customers = CustomerRepository.GetFilteredKlantenDESC(FilterCustomer);
                        break;
                }
            }
            else
            {
                Customers = CustomerRepository.GetFilteredKlanten(FilterCustomer);

            }
        }
    


        public void SortCustomers()
        {
            if(FilterCustomer != null)
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            Customers = CustomerRepository.GetFilteredKlantenASC(FilterCustomer);
                            break;
                        case "Z - A":
                            Customers = CustomerRepository.GetFilteredKlantenDESC(FilterCustomer);
                            break;
                    }
                }
                else
                {
                    Customers = CustomerRepository.GetFilteredKlanten(FilterCustomer);

                }
            }
            else
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            Customers = CustomerRepository.GetKlantenASC(FilterCustomer);
                            break;
                        case "Z - A":
                            Customers = CustomerRepository.GetKlantenDESC(FilterCustomer);
                            break;
                    }
                }
                else
                {
                    Customers = CustomerRepository.GetKlanten();

                }
            }
        }
    }
}
