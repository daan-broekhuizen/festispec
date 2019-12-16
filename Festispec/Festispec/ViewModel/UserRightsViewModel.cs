using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class UserRightsViewModel : ViewModelBase
    {
        public ObservableCollection<AccountViewModel> Accounts;
        public ObservableCollection<RollenViewModel> Roles { get; set; }
        public ObservableCollection<string> RolItems { get; set; }
        private List<AccountViewModel> filteredAccounts;
        private List<RollenViewModel> currentRollen;
        public List<string> RolsString;


        public UserRightsViewModel()
        { }

        public List<AccountViewModel> FilteredAccounts
        {
            get => filteredAccounts;
            set
            {
                filteredAccounts = value;
                RaisePropertyChanged("FilteredAccounts");
            }
        }

        public List<RollenViewModel> CurrentRoles
        {
            get => currentRollen;
            set
            {
                currentRollen = value;
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

        public ICommand SortChangedCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public UserRightsViewModel(NavigationService service, UserRepository repo) 
        {
            Accounts = new ObservableCollection<AccountViewModel>(repo.GetUsers().Select(c => new AccountViewModel(c)).ToList());
            Roles = new ObservableCollection<RollenViewModel>(repo.GetRols().Select(c => new RollenViewModel(c)).ToList());

            FilteredAccounts = Accounts.ToList();
            CurrentRoles = Roles.ToList();


            SearchButtonClickCommand = new RelayCommand<string>(FilterAccounts);
            SearchTextChangedCommand = new RelayCommand<string>(FilterAccounts);
            SortChangedCommand = new RelayCommand<int>(ChangeSort);

        }

        private void FilterAccounts(string searchText) => FilteredAccounts = Accounts.Where(e => e.Username.ToLower().Contains(searchText)).ToList();

        private void ChangeSort(int sortMode)
        {
            switch (sortMode)
            {
                case 0:
                    FilteredAccounts = FilteredAccounts.OrderBy(e => e.Username).ToList();
                    break;
                case 1:
                    FilteredAccounts = FilteredAccounts.OrderByDescending(e => e.Username).ToList();
                    break;
            }
        }
    }
}
