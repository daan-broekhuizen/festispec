using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectionFormViewModels.Interfaces
{
    public interface IInspectionFormViewModel
    {
        Inspectieformulier InspectionForm { get; set; }
        ObservableCollection<QuestionViewModel> Questions { get; }
    }
}
