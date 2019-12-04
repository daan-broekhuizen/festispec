﻿using Festispec.API.ImageShack;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.RapportageViewModels
{
    public class RapportageViewModel : ViewModelBase
    {
        // Repositories
        private readonly RapportageRepository _repo;

        // Services
        private readonly NavigationService _navigationService;

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
        public ICommand SaveCommand { get; set; }
        public ICommand DownloadCommand { get; set; }

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

        private bool _isEditable;

        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;

                RaisePropertyChanged("IsEditable");
            }
        }

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

        public bool ShouldAddResults { get; set; }

        private EnumTemplateMode _mode;
        private RapportTemplate _template;
        
        public RapportageViewModel(NavigationService navigationService, RapportageRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;

            if(navigationService.Parameter != null)
            {
                object[] parameters = (object[])navigationService.Parameter;

                _mode = (EnumTemplateMode)parameters[0];
                if (parameters[1] is RapportTemplate)
                {
                    _template = (RapportTemplate)parameters[1];
                    Content = _template.TemplateText;
                }
            }

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
            SaveCommand = new RelayCommand<DocumentDesigner>((designer) => Save(designer.ViewModel));
            DownloadCommand = new RelayCommand<DocumentDesigner>((designer) => Download(designer.ViewModel));

            IsEditable = false;
            DisplayExtraOptions = false;
        }

        public void Init(RapportTemplate template)
        {
            _template = template;
        }

        public void ChangeMode(DocumentDesigner designer, int selectedMode)
        {
            designer.ViewModel.ChangeMode(selectedMode);
            if (selectedMode == 1)
                IsEditable = true;
            else
                IsEditable = false;
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

        private void Save(DocumentDesignerViewModel designer)
        {
            if(_mode == EnumTemplateMode.CREATE)
            {
                SaveDialogBox saveDialog = new SaveDialogBox();
                if(saveDialog.ShowDialog() == false)
                {
                    RapportTemplate template = new RapportTemplate()
                    {
                        TemplateText = designer.DesignerContent,
                        TemplateName = saveDialog.ViewModel.Name,
                        TemplateDescription = saveDialog.ViewModel.Description
                    };
                    _repo.CreateTemplate(template);

                    _navigationService.NavigateTo("RapportageTemplateOverview", EnumTemplateMode.EDIT);
                }
            }
            else if(_mode == EnumTemplateMode.EDIT)
            {
                designer.UpdateContent();
                if(_template != null)
                {
                    _template.TemplateText = designer.DesignerContent;

                    _repo.UpdateTemplate(_template);
                    _navigationService.NavigateTo("RapportageTemplateOverview", EnumTemplateMode.EDIT);
                }
            }
            else if(_mode == EnumTemplateMode.SELECT)
            {

            }
        }

        private void Download(DocumentDesignerViewModel designer)
        {
            designer.UpdateContent();

            byte[] data = designer.ExportToPdf();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream dataStream = saveFileDialog.OpenFile())
                {
                    dataStream.Write(data, 0, data.Length);
                }
            }
        }
    }
}