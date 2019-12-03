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
        public ICommand ShowMessagesView { get; set; }
        public ICommand ShowAccountView { get; set; }
        public ICommand ShowPlanningView { get; set; }
        public ICommand ShowScheduleView { get; set; }
        public ICommand ShowRapportageTemplatesView { get; set; }
        public ICommand ShowInspectionFormTemplatesView { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ShowManagementReportView { get; set; }

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
            if (_navigationService.Parameter is AccountViewModel)
                AccountVM = _navigationService.Parameter as AccountViewModel;

            ShowCustomersView = new RelayCommand(ShowCustomers);
            ShowDashboardView = new RelayCommand(ShowDashboard);
            ShowRapportageTemplatesView = new RelayCommand(ShowRapportageTemplates);
            ShowInspectionFormTemplatesView = new RelayCommand(ShowInspectionFormTemplates);
            ShowJobsView = new RelayCommand(ShowJobs);
            ShowManagementReportView = new RelayCommand(ShowManagementReport);

            LogoutCommand = new RelayCommand(Logout);

            if (navigation.AppSettings.DebugMode && !string.IsNullOrEmpty(navigation.AppSettings.StartupPage))
                _navigationService.ApplicationNavigateTo(navigation.AppSettings.StartupPage, null);
        }

        private void Logout() => _navigationService.ApplicationNavigateTo("Login", null);

        private void ShowDashboard() => _navigationService.NavigateTo("Dashboard");
        private void ShowCustomers() => _navigationService.NavigateTo("Customers");
        private void ShowJobs() => _navigationService.NavigateTo("Jobs");
        private void ShowRapportageTemplates() => _navigationService.NavigateTo("RapportageTemplateOverview", EnumTemplateMode.EDIT);
        private void ShowInspectionFormTemplates() => _navigationService.NavigateTo("InspectionFormTemplateOverview", EnumTemplateMode.EDIT);
        private void ShowManagementReport() => _navigationService.NavigateTo("ManagementReport");
    }


}