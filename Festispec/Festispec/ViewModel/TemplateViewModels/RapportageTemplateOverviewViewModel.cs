using Festispec.Model;
using Festispec.Model.Enums;
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
    public class RapportageTemplateOverviewViewModel : TemplateOverviewViewModel
    {
        private List<RapportTemplate> _unfilteredTemplates;

        private List<RapportTemplate> _templates;

        public List<RapportTemplate> Templates
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

        public RapportageTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            _unfilteredTemplates = templateRepository.GetRapportTemplates();
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
                Templates = _unfilteredTemplates.Where(x => x.TemplateName.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        protected override void SelectTemplate(dynamic template)
        {
            RapportTemplate rapportTemplate = template;

            _navigationService.NavigateTo("Rapportage", rapportTemplate);
        }

        protected override void EditTemplate(dynamic template)
        {
            throw new NotImplementedException();
        }
    }
}
