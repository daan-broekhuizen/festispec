using Festispec.Model;
using Festispec.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.TemplateViewModels
{
    public class TemplateOverviewViewModel : NavigatableViewModel
    {
        public ICommand CreateButtonClickCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public ObservableCollection<RapportTemplate> Templates { get; set; }

        public TemplateOverviewViewModel(NavigationService service) : base(service)
        {
            this.CreateButtonClickCommand = new RelayCommand(() => Console.WriteLine("Test"));
            this.SearchButtonClickCommand = new RelayCommand<string>((content) => Console.WriteLine(content));
            this.SearchTextChangedCommand = new RelayCommand<string>((content) => Console.WriteLine(content));

            this.Templates = new ObservableCollection<RapportTemplate>();
            for(int i = 0; i < 50; i++)
                this.Templates.Add(new RapportTemplate() { TemplateName = "Test", TemplateDescription = "prGbEM6flQ2YUckUEgO2Pdh4y9J8gRUbSEQw0boZCoIjgNhxoNGFVPQA7AzDUZowDkSLJ93WGHeeUKHZ1AKexT1a3wRjN5ONbhuExU8uig46QCW1UyzHwquDYu6fe6mwq8rnhiHFUXS21pOusA8OKm14p8asoFqyqdtGyLhTDtq8oENLP5Kazl6mjkgafspjfUFkjQYhortW23THikIuEm6DOesvRya6oki4VVLQDzDMTy3qaetESgV5n7IRR6SpScusPlPJG6kDUNiNJT4qxWFVK1wWhRDHXRjiMW9RP2VBjYJkbr7dDxpCq2gU6kKfrTMt5v4n4Lil2x6vsikTXwYyPeMO3HJUepBkUXEVLhthgee0v5L1gIl5yMCb2MRq4yVNzw35ZuAa0FXN" });
        }
    }
}
