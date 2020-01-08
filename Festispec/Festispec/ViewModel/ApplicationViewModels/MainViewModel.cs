using Festispec.Model.Enums;
using Festispec.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : NavigatableViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public ICommand ShowCustomersView { get; set; }
        public ICommand ShowJobsView { get; set; }
        public ICommand ShowQuotationsView { get; set; }
        public ICommand ShowDashboardView { get; set; }
        public ICommand ShowUserRights { get; set; }
        public ICommand LogoutCommand { get; set; }

        private AccountViewModel _accountVM;
        public AccountViewModel AccountVM
        {
            get => _accountVM;
            set
            {
                _accountVM = value;
                RaisePropertyChanged("AccountVM");
            }
        }

        public MainViewModel(NavigationService navigation) : base(navigation)
        {
            AccountVM = _navigationService.Account;

            ShowCustomersView = new RelayCommand(ShowCustomers);
            ShowDashboardView = new RelayCommand(ShowDashboard);
            ShowQuotationsView = new RelayCommand(ShowQuotations);
            ShowJobsView = new RelayCommand(ShowJobs);
            ShowUserRights = new RelayCommand(ShowUserRightsView);

            LogoutCommand = new RelayCommand(Logout);

            if (navigation.AppSettings.DebugMode && !string.IsNullOrEmpty(navigation.AppSettings.StartupPage))
                _navigationService.ApplicationNavigateTo(navigation.AppSettings.StartupPage, null);
        }

        private void Logout() => _navigationService.ApplicationNavigateTo("Login", null);

        private void ShowDashboard() => _navigationService.NavigateTo("Dashboard");
        private void ShowCustomers() => _navigationService.NavigateTo("Customers");
        private void ShowQuotations() => _navigationService.NavigateTo("QuotationList");
        private void ShowJobs() => _navigationService.NavigateTo("Jobs");
        private void ShowUserRightsView() => _navigationService.NavigateTo("UserRights");
    }

}