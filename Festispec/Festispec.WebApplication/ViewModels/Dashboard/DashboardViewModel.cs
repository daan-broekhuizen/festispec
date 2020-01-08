using Festispec.WebApplication.Models;
using Festispec.WebApplication.ViewModels.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public Account Account { get; set; }

        public List<InspectionformViewModel> Inspectionform { get; set; }
    }
}