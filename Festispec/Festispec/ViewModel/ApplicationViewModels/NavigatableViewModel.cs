using Festispec.Service;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public abstract class NavigatableViewModel : ViewModelBase
    {
        protected NavigationService _navigationService;
        public NavigatableViewModel(NavigationService service)
        {
            _navigationService = service;
        }
    }
}
