﻿using Festispec.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class DashboardViewModel
    {
        private NavigationService _navigationService;
        public ICommand ShowCustomersCommand { get; set; }
        public ICommand ShowJobsCommand { get; set; }
        public ICommand ShowQuotationsCommand { get; set; }
        public ICommand ShowAddCustomerCommand { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        public ICommand ShowAddQuotationCommand { get; set; }
        public ICommand ShowPlanningCommand { get; set; }
        public ICommand ShowScheduleCommand { get; set; }
        public ICommand ShowMessagesCommand { get; set; }


        public DashboardViewModel(NavigationService service)
        {
            _navigationService = service;
        }


    }
}
