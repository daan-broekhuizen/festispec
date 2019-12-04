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
        private JobViewModel _job;
        private TemplateRepository _repo;

        public RapportageTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            _repo = templateRepository;
            _unfilteredTemplates = new List<TemplateViewModel>(_repo.GetRapportTemplates());
            Templates = _unfilteredTemplates;

            if (service.Parameter is object[] parameters)
                if (parameters.Length > 1 && parameters[1] != null && parameters[1] is JobViewModel)
                    _job = (JobViewModel)parameters[1];
        }

        protected override void CreateButtonClick()
        {
            _navigationService.NavigateTo("Rapportage", new object[1] { EnumTemplateMode.CREATE });
        }

        protected override void SelectTemplate(dynamic template)
        {
            RapportTemplate rapportTemplate = _repo.GetRapportTemplate(template);

            _navigationService.NavigateTo("Rapportage", new object[3] { EnumTemplateMode.SELECT, rapportTemplate, _job });
        }

        protected override void EditTemplate(dynamic template)
        {
            RapportTemplate rapportTemplate = _repo.GetRapportTemplate(template);

            _navigationService.NavigateTo("Rapportage", new object[2] { EnumTemplateMode.EDIT, template });
        }
    }
}
