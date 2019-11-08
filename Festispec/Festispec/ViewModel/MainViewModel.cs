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

        public ICommand ShowCustomerView { get; set; }
        public ICommand ShowJobView { get; set; }
        public ICommand ShowQuotationView { get; set; }
        public ICommand ShowDashboardView { get; set; }
        public ICommand ShowMessageView { get; set; }
        public ICommand ShowAccountView { get; set; }
        public ICommand ShowPlanningView { get; set; }
        public ICommand ShowCalendarView { get; set; }

        private UserControl currentPage;
        public UserControl CurrentPage 
        {
            get => currentPage; 
            set
            {
                currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        public MainViewModel()
        {
            CurrentPage = new UserControl();
            ShowCustomerView = new RelayCommand(ShowCustomer);
            ShowDashboardView = new RelayCommand(ShowDashboard);
        }

        private void ShowDashboard() => CurrentPage = new HomeWindow();
        private void ShowCustomer() => CurrentPage = new Page1();
    }


}