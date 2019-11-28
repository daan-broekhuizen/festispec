using Festispec.Model;
using Festispec.Service;
using Festispec.View.Components;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Festispec.ViewModel.RapportageViewModels
{
    public class RapportageViewModel : ViewModelBase
    {
        // Services
        private NavigationService _navigationService;

        private RapportTemplate _template;

        // Commands
        public ICommand ModeChangedCommand { get; set; }
        public ICommand ApplyStyleCommand { get; set; }
        public ICommand FontTypeChangedCommand { get; set; }
        public ICommand FontSizeChangedCommand { get; set; }
        public ICommand FontColorChangedCommand { get; set; }
        public ICommand SwitchTemplateCommand { get; set; }
        public ICommand CreateChartCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand ExtraOptionsCommand { get; set; }

        // Properties
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;

                RaisePropertyChanged("Content");
            }
        }

        private Visibility _isEditable;

        public Visibility IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;

                RaisePropertyChanged("IsEditable");
            }
        }

        public bool ShouldAddResults { get; set; }

        public RapportageViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            Content = "<html><body><h1>Header</h1><a href=\"http://www.google.nl\">Google</a><img src=\"https://www.perwez.be/actualites/images-actualites/test.png/@@images/image.png\" alt=\"test\" width=\"100\"/></body></html>";

            ModeChangedCommand = new RelayCommand<object[]>((parameters) => ChangeMode((DocumentDesigner)parameters[0], (int)parameters[1]));
            ApplyStyleCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyStyle((string)parameters[1]));
            FontTypeChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyFontType((string)parameters[1]));
            FontSizeChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyFontSize((string)parameters[1]));
            FontColorChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyFontColor((Color)parameters[1]));
            SwitchTemplateCommand = new RelayCommand(() => { throw new NotImplementedException(); });
            AddImageCommand = new RelayCommand(AddImage);
            CreateChartCommand = new RelayCommand(CreateChart);

            IsEditable = Visibility.Visible;

            if (_navigationService.Parameter is RapportTemplate)
                _template = (RapportTemplate)_navigationService.Parameter;
        }

        public void Init(RapportTemplate template)
        {
            _template = template;
        }

        public void ChangeMode(DocumentDesigner designer, int selectedMode)
        {
            designer.ViewModel.ChangeMode(selectedMode);
            if (selectedMode == 1)
                IsEditable = Visibility.Visible;
            else
                IsEditable = Visibility.Collapsed;
        }

        public void AddImage()
        {
            // TODO: Open File Dialog
        }

        public void CreateChart()
        {
            // TODO: Open Grafiek Create Scherm
        }
    }
}