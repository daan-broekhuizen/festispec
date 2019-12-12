using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using Festispec.ViewModel.CustomerViewModels;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
namespace Festispec.ViewModel
{
    public class UserRightsViewModel : ViewModelBase
    {
        public ObservableCollection<AccountViewModel> Accounts;
        private List<AccountViewModel> filteredAccounts;

        public List<AccountViewModel> FilteredAccounts
        {
            get => filteredAccounts;
            set
            {
                filteredAccounts = value;
                RaisePropertyChanged("FilteredAccounts");
            }
        }

        private AccountViewModel selectedAccount;
        public AccountViewModel SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                RaisePropertyChanged("SelectedAccount");
                //ShowCustomerInfo();
            }
        }
    }
}
