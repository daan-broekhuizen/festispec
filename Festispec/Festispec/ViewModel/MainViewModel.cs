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

        private ViewFactory _viewFactory;

        private UserControl _currentView;
        public UserControl CurrentView 
        {
            get => _currentView; 
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }
        public MainViewModel()
        {
            _viewFactory = new ViewFactory();
            ShowCustomersView = new RelayCommand(ShowCustomers);
            ShowDashboardView = new RelayCommand(ShowDashboard);
        }

        private void ShowDashboard() => CurrentView = _viewFactory.GetView("Dashboard");
        private void ShowCustomers() => CurrentView = _viewFactory.GetView("Customers");
        private void ShowJobs() => CurrentView = _viewFactory.GetView("Jobs");
        private void ShowQuotations() => CurrentView = _viewFactory.GetView("Quotations");
        private void ShowMessages() => CurrentView = _viewFactory.GetView("Messages");
        private void ShowPlanning() => CurrentView = _viewFactory.GetView("Planning");
        private void ShowSchedule() => CurrentView = _viewFactory.GetView("Schedule");

    }


}