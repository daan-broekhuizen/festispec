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
    public class RapportageTemplateOverviewViewModel : TemplateOverviewViewModel
    {
        public ObservableCollection<RapportTemplate> Templates { get; set; }

        public RapportageTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            this.Templates = new ObservableCollection<RapportTemplate>(templateRepository.GetRapportTemplates());
        }

        protected override void CreateButtonClick()
        {
            throw new NotImplementedException();
        }

        protected override void SearchButtonClick(string content)
        {
            throw new NotImplementedException();
        }

        protected override void SelectTemplate(dynamic template)
        {
            RapportTemplate rapportTemplate = template;

            Console.WriteLine(rapportTemplate.TemplateName);
        }
    }
}
