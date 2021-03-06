﻿using Festispec.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class DashboardViewModel : NavigatableViewModel
    {
        public ICommand ShowCustomersCommand { get; set; }
        public ICommand ShowJobsCommand { get; set; }
        public ICommand ShowQuotationsCommand { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public ICommand ShowManagementCommand { get; set; }
        public ICommand ShowScheduleCommand { get; set; }
        public ICommand ShowInspectionTemplates { get; set; }
        public ICommand ShowReportTemplates { get; set; }

        public DashboardViewModel(NavigationService service) : base(service)
        {
            ShowAddCustomerCommand = new RelayCommand(ShowAddCustomer);
            ShowCustomersCommand = new RelayCommand(ShowCustomers);
            ShowQuotationsCommand = new RelayCommand(ShowQuotations);
            ShowJobsCommand = new RelayCommand(ShowJobs);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            ShowManagementCommand = new RelayCommand(ShowManagement);
            ShowInspectionTemplates = new RelayCommand(ShowFormTemplate);
            ShowReportTemplates = new RelayCommand(ShowReportTemplate);

        }

        private void ShowReportTemplate() => _navigationService.NavigateTo("RapportageTemplateOverview");
        private void ShowFormTemplate() => _navigationService.NavigateTo("InspectionFormTemplateOverview");
        private void ShowManagement() => _navigationService.NavigateTo("ManagementReport");
        private void ShowJobs() => _navigationService.NavigateTo("Jobs");
        private void ShowAddCustomer() => _navigationService.NavigateTo("AddCustomerInfo");
        private void ShowCustomers() => _navigationService.NavigateTo("Customers");
        private void ShowQuotations() => _navigationService.NavigateTo("QuotationList");
        private void ShowAddJob() => _navigationService.NavigateTo("AddJob");
    }
}
