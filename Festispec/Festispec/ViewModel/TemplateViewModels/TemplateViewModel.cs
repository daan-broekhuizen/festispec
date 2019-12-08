using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.TemplateViewModels
{
    public class TemplateViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;

                RaisePropertyChanged("Title");
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                RaisePropertyChanged("Description");
            }
        }

        private int _rapportTemplateId;
        public int RapportTemplateID
        {
            get => _rapportTemplateId;
            set
            {
                _rapportTemplateId = value;

                RaisePropertyChanged("RapportTemplateID");
            }
        }

        private int _inspectionFormTemplateId;
        public int InspectionFormTemplateID
        {
            get => _inspectionFormTemplateId;
            set
            {
                _inspectionFormTemplateId = value;

                RaisePropertyChanged("InspectionFormTemplateId");
            }
        }

        public TemplateViewModel(){}

        public TemplateViewModel(RapportTemplate template)
        {
            Title = template.TemplateName;
            Description = template.TemplateDescription;
            RapportTemplateID = template.TemplateID;
        }

        public TemplateViewModel(Inspectieformulier inspection)
        {
            Title = inspection.InspectieFormulierTitel;
            Description = inspection.Beschrijving;
            InspectionFormTemplateID = inspection.InspectieformulierID;
        }
    }
}
