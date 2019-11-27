using Festispec.Model;
using Festispec.Service;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class CreateInspectionFormViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        private Inspectieformulier _template;

        public Inspectieformulier Template { get => _template; set => _template = value; }

        public CreateInspectionFormViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            if (_navigationService.Parameter is Inspectieformulier)
                _template = (Inspectieformulier)_navigationService.Parameter;
        }
    }
}
