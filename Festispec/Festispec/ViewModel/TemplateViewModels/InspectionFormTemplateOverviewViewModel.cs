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
        private TemplateRepository _repo;

        public InspectionFormTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            _repo = templateRepository;
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
                Templates = _unfilteredTemplates.Where(x => x.Title.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        protected override void SelectTemplate(dynamic template)
        {
            Inspectieformulier inspectionTemplate = _repo.GetInspectionFormTemplate(template);

            _navigationService.NavigateTo("CreateInspectionForm", inspectionTemplate);
        }

        protected override void EditTemplate(dynamic template)
        {
            throw new NotImplementedException();
        }
    }
}
