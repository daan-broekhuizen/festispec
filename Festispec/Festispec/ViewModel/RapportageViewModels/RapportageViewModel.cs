using Festispec.API.ImageShack;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Service;
using Festispec.View.Components;
using Festispec.View.RapportageView;
using Festispec.ViewModel.Components;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.RapportageViewModels
{
    public class RapportageViewModel : ViewModelBase
    {
        // Services
        private NavigationService _navigationService;

        private RapportTemplate _template;

        private bool _displayExtraOptions;
        public bool DisplayExtraOptions
        {
            get => _displayExtraOptions;
            set
            {
                _displayExtraOptions = value;

                RaisePropertyChanged("DisplayExtraOptions");
            }
        }

        // Commands
        public ICommand ModeChangedCommand { get; set; }
        public ICommand ApplyStyleCommand { get; set; }
        public ICommand FontTypeChangedCommand { get; set; }
        public ICommand FontSizeChangedCommand { get; set; }
        public ICommand FontColorChangedCommand { get; set; }
        public ICommand ApplyAlignmentCommand { get; set; }
        public ICommand SwitchTemplateCommand { get; set; }
        public ICommand CreateChartCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand ExtraOptionsCommand { get; set; }
        public ICommand UnlockCommand { get; set; }
        public ICommand WidthChangedCommand { get; set; }
        public ICommand HeightChangedCommand { get; set; }

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
            AddImageCommand = new RelayCommand<DocumentDesigner>((designer) => AddImage(designer.ViewModel));
            CreateChartCommand = new RelayCommand<object[]>((parameters) => CreateChart(((DocumentDesigner)parameters[0]).ViewModel, (string)parameters[1]));
            ExtraOptionsCommand = new RelayCommand(() => DisplayExtraOptions = !DisplayExtraOptions);
            UnlockCommand = new RelayCommand<DocumentDesigner>((designer) => designer.ViewModel.EnableMovement());
            ApplyAlignmentCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyAlignment((string)parameters[1]));
            WidthChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ChangeAttribute("width", $"{(string)parameters[1]}px", true));
            HeightChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ChangeAttribute("height", $"{(string)parameters[1]}px", true));

            IsEditable = Visibility.Visible;
            DisplayExtraOptions = false;

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

        public void AddImage(DocumentDesignerViewModel designer)
        {
            BitmapImage bmp = new ImageSelectService().SelectPngImage();

            if (bmp.UriSource == null)
                return;

            UploadModel response = new ImageShackClient().UploadImage(new ImageContainer(bmp.UriSource.AbsolutePath));

            if (response.Images.Length > 0)
                designer.AddImage(response.Images.First().HttpLink);
        }

        public void CreateChart(DocumentDesignerViewModel designer, string mode)
        {
            ChartDialogBox chartDialog = new ChartDialogBox();
            chartDialog.ViewModel.AddRequested += AddChartRequested;
            chartDialog.ViewModel.Create(designer, mode);

            chartDialog.ShowDialog();
        }

        private void AddChartRequested(DocumentDesignerViewModel designer, UploadModel uploaded)
        {
            if (uploaded != null)
                designer.AddImage(uploaded.Images.First().HttpLink);
        }
    }
}