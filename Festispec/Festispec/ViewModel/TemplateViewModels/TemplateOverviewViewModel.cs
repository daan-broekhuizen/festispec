using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.TemplateViewModels
{
    public abstract class TemplateOverviewViewModel : NavigatableViewModel
    {
        public ICommand CreateButtonClickCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SelectTemplateCommand { get; set; }
        public ICommand EditTemplateCommand { get; set; }

        public EnumTemplateMode Mode { get; set; }

        public Visibility IsEditMode
        {
            get => Mode == EnumTemplateMode.EDIT ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility IsSelectMode
        {
            get => Mode == EnumTemplateMode.SELECT ? Visibility.Visible : Visibility.Collapsed;
        }

        protected List<TemplateViewModel> _unfilteredTemplates;

        private List<TemplateViewModel> _templates;

        public List<TemplateViewModel> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                RaisePropertyChanged("Templates");
            }
        }

        public TemplateOverviewViewModel(NavigationService service) : base(service)
        {
            this.CreateButtonClickCommand = new RelayCommand(CreateButtonClick);
            this.SearchButtonClickCommand = new RelayCommand<string>(SearchButtonClick);
            this.SelectTemplateCommand = new RelayCommand<dynamic>(SelectTemplate);
            this.EditTemplateCommand = new RelayCommand<dynamic>(EditTemplate);

            if (service.Parameter is EnumTemplateMode)
                Mode = (EnumTemplateMode)service.Parameter;
            else if (service.Parameter is object[])
                Mode = (EnumTemplateMode)((object[])service.Parameter)[0];
            else
                Mode = EnumTemplateMode.EDIT;
        }

        protected abstract void CreateButtonClick();

        protected virtual void SearchButtonClick(string content)
        {
            if (string.IsNullOrEmpty(content))
                Templates = _unfilteredTemplates;
            else
                Templates = _unfilteredTemplates.Where(x => x.Title.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        protected abstract void SelectTemplate(dynamic template);
        protected abstract void EditTemplate(dynamic template);
    }
}
