using Festispec.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public ICommand ShowCustomersView { get; set; }
        public ICommand ShowJobsView { get; set; }
        public ICommand ShowQuotationsView { get; set; }
        public ICommand ShowDashboardView { get; set; }
        public ICommand ShowMessagesView { get; set; }
        public ICommand ShowAccountView { get; set; }
        public ICommand ShowPlanningView { get; set; }
        public ICommand ShowScheduleView { get; set; }

        private NavigationService _navigationService;

        public MainViewModel(NavigationService navigation)
        {
            _navigationService = navigation;

            ShowCustomersView = new RelayCommand(ShowCustomers);
            ShowDashboardView = new RelayCommand(ShowDashboard);
        }
        private void ShowDashboard() => _navigationService.NavigateTo("Dashboard");
        private void ShowCustomers() => _navigationService.NavigateTo("Customers");
    }


}