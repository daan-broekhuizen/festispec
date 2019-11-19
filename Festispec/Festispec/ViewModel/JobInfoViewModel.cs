using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Service;

namespace Festispec.ViewModel
{
    public class JobInfoViewModel
    {


        public JobViewModel JobVM { get; set; }
        private NavigationService _navigationService;
        public JobInfoViewModel(NavigationService service)
        {
            if (service.Parameter is JobViewModel)
                JobVM = service.Parameter as JobViewModel;
        }
    }
}
