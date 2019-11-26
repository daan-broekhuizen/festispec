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
    public class InspectieformulierTemplateOverviewViewModel : TemplateOverviewViewModel
    {
        public ObservableCollection<Inspectieformulier> Templates { get; set; }

        public InspectieformulierTemplateOverviewViewModel(NavigationService service, TemplateRepository templateRepository) : base(service)
        {
            this.Templates = new ObservableCollection<Inspectieformulier>();
            for (int i = 0; i < 50; i++)
                this.Templates.Add(new Inspectieformulier() { InspectieFormulierTitel = "test", Beschrijving = "prGbEM6flQ2YUckUEgO2Pdh4y9J8gRUbSEQw0boZCoIjgNhxoNGFVPQA7AzDUZowDkSLJ93WGHeeUKHZ1AKexT1a3wRjN5ONbhuExU8uig46QCW1UyzHwquDYu6fe6mwq8rnhiHFUXS21pOusA8OKm14p8asoFqyqdtGyLhTDtq8oENLP5Kazl6mjkgafspjfUFkjQYhortW23THikIuEm6DOesvRya6oki4VVLQDzDMTy3qaetESgV5n7IRR6SpScusPlPJG6kDUNiNJT4qxWFVK1wWhRDHXRjiMW9RP2VBjYJkbr7dDxpCq2gU6kKfrTMt5v4n4Lil2x6vsikTXwYyPeMO3HJUepBkUXEVLhthgee0v5L1gIl5yMCb2MRq4yVNzw35ZuAa0FXN" });
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
            throw new NotImplementedException();
        }
    }
}
