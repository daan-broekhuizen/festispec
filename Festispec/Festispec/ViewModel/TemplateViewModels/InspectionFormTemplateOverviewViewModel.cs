using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.TemplateViewModels
{
    public class InspectionFormTemplateOverviewViewModel : TemplateOverviewViewModel
    {
        private List<Inspectieformulier> _unfilteredTemplates;

        private List<Inspectieformulier> _templates;

        public List<Inspectieformulier> Templates
        {
            get
            {
                return _templates;
            }
            set
            {
                _templates = value;
                RaisePropertyChanged("Templates");
            }
        }

        public InspectionFormTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            _unfilteredTemplates = templateRepository.GetInspectieformulierTemplates();
            Templates = _unfilteredTemplates;
        }

        protected override void CreateButtonClick()
        {
            throw new NotImplementedException();
        }

        protected override void SearchButtonClick(string content)
        {
            if (string.IsNullOrEmpty(content))
                Templates = _unfilteredTemplates;
            else
                Templates = _unfilteredTemplates.Where(x => x.InspectieFormulierTitel.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        protected override void SelectTemplate(dynamic template)
        {
            Inspectieformulier inspectieformulierTemplate = template;

            _navigationService.NavigateTo("CreateInspectionForm", inspectieformulierTemplate);
        }

        protected override void EditTemplate(dynamic template)
        {
            throw new NotImplementedException();
        }
    }
}
