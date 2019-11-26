using Festispec.Model;
using Festispec.Model.Repositories;
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
    public abstract class TemplateOverviewViewModel : NavigatableViewModel
    {
        public ICommand CreateButtonClickCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SelectTemplateCommand { get; set; }

        public TemplateOverviewViewModel(NavigationService service) : base(service)
        {
            this.CreateButtonClickCommand = new RelayCommand(CreateButtonClick);
            this.SearchButtonClickCommand = new RelayCommand<string>(SearchButtonClick);
            this.SelectTemplateCommand = new RelayCommand<dynamic>(SelectTemplate);
        }

        protected abstract void CreateButtonClick();
        protected abstract void SearchButtonClick(string content);
        protected abstract void SelectTemplate(dynamic template);
    }
}
