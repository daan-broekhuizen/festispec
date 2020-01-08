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
        private int _jobId;

        public InspectionFormTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            _repo = templateRepository;
            if (service.Parameter is object[] parameters)
                if (parameters.Length > 1 && parameters[1] != null && parameters[1] is int)
                    _jobId = (int)parameters[1];
            _unfilteredTemplates = templateRepository.GetInspectieformulierTemplates();
            Templates = _unfilteredTemplates;
        }

        protected override void CreateButtonClick()
        {
            _navigationService.NavigateTo("InspectionFormEditView", null);
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
            Inspectieformulier inspectionTemplate = _repo.GetInspectionFormTemplate(((TemplateViewModel)template).InspectionFormTemplateID);
            inspectionTemplate.OpdrachtID = _jobId;
            inspectionTemplate.InspectieformulierID = 0;
            inspectionTemplate.Beschrijving = null;
            inspectionTemplate.InspectieFormulierTitel = "Nieuw inspectieformulier";
            foreach(Vraag question in inspectionTemplate.Vraag)
            {
                question.InspectieFormulierID = 0;
                question.VraagID = 0;
                foreach(VraagMogelijkAntwoord vma in question.VraagMogelijkAntwoord)
                    vma.VraagID = 0;
            }

            _navigationService.NavigateTo("InspectionFormEditView", inspectionTemplate);
        }

        protected override void EditTemplate(dynamic template)
        {
            Inspectieformulier inspectionTemplate = _repo.GetInspectionFormTemplate(((TemplateViewModel)template).InspectionFormTemplateID);
            _navigationService.NavigateTo("InspectionFormEditView", inspectionTemplate);
        }

        protected override void DeleteTemplate(dynamic template)
        {
            _repo.DeleteInspectionFormTemplate(((TemplateViewModel)template).InspectionFormTemplateID);
            _unfilteredTemplates = _repo.GetInspectieformulierTemplates();
            SearchButtonClick(null);

        }
    }
}
