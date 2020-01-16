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
        private List<AccountViewModel> _filteredAccounts;
        private List<RollenViewModel> _currentRollen;
        public List<string> RolsString;
        private NavigationService _navigationService;
        public ICommand AddUser { get; set; }


        public UserRightsViewModel()
        { 
        }

        public List<AccountViewModel> FilteredAccounts
        {
            get => _filteredAccounts;
            set
            {
                _filteredAccounts = value;
                RaisePropertyChanged("FilteredAccounts");
            }
        }

        public List<RollenViewModel> CurrentRoles
        {
            get => _currentRollen;
            set
            {
                _currentRollen = value;
                RaisePropertyChanged("FilteredAccounts");
            }
        }

        private AccountViewModel _selectedAccount;
        public AccountViewModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                RaisePropertyChanged("SelectedAccount");
                ShowUserInfo();
            }
        }

        public ICommand SortChangedCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public UserRightsViewModel(NavigationService service, UserRepository repo) 
        {
            _navigationService = service;
            Accounts = new ObservableCollection<AccountViewModel>(repo.GetUsers().Select(c => new AccountViewModel(c)).ToList());
            Roles = new ObservableCollection<RollenViewModel>(repo.GetRols().Select(c => new RollenViewModel(c)).ToList());

            FilteredAccounts = Accounts.ToList();
            CurrentRoles = Roles.ToList();


            SearchButtonClickCommand = new RelayCommand<string>(FilterAccounts);
            SearchTextChangedCommand = new RelayCommand<string>(FilterAccounts);
            SortChangedCommand = new RelayCommand<int>(ChangeSort);
            AddUser = new RelayCommand(OpenAddUser);
        }

        private void FilterAccounts(string searchText) => FilteredAccounts = Accounts.Where(e => e.Username.ToLower().Contains(searchText)).ToList();

        private void OpenAddUser()
        {
            _navigationService.NavigateTo("AddUser");
        }

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

        private void ShowUserInfo()
        {
            if (SelectedAccount != null)
                _navigationService.NavigateTo("AccountInfo", SelectedAccount);
        }
    }
}
