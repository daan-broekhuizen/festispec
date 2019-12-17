using Festispec.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public Account Account { get; set; }

        public List<Inspectieformulier> Inspectionform { get; set; }
    }
}