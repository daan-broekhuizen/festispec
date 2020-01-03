using Festispec.API;
using Festispec.API.ImageShack;
using Festispec.API.Uploading;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Utility;
using Festispec.Utility.Extensions;
using Festispec.ViewModel.Components.Charts;
using Festispec.ViewModel.InspectionFormViewModels;
using Festispec.ViewModel.RapportageViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.Components
{
    public class ChartDialogViewModel : ViewModelBase
    {
        // Repository
        private RapportageRepository _rapportageRepository;

        // Events
        public delegate void AddRequestedEventHandler(DocumentDesignerViewModel designer, UploadModel uploaded);

        public event AddRequestedEventHandler AddRequested;

        // Commands
        public ICommand SwitchAxisCommand { get; set; }
        public ICommand AddChartCommand { get; set; }
        public ICommand ForegroundColorChangedCommand { get; set; }
        public ICommand BackgroundColorChangedCommand { get; set; }
        public ICommand TitleChangedCommand { get; set; }
        public ICommand QuestionChangedCommand { get; set; }
        public ICommand XAxisTextChangedCommand { get; set; }
        public ICommand YAxisTextChangedCommand { get; set; }

        // Properties
        private bool _isXAxis;
        public bool IsXAxis
        {
            get => _isXAxis;
            set
            {
                _isXAxis = value;

                RaisePropertyChanged("IsXAxis");
            }
        }

        private bool _isYAxis;
        public bool IsYAxis
        {
            get => _isYAxis;
            set
            {
                _isYAxis = value;

                RaisePropertyChanged("IsYAxis");
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;

                RaisePropertyChanged("Title");
            }
        }

        public string XAxisTitle
        {
            get
            {
                if (ChartViewModel != null)
                    return (string)ChartViewModel.Configuration[EnumChartConfiguration.XAXISTITLE];

                return "";
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.Configuration.Update(EnumChartConfiguration.XAXISTITLE, value);

                RaisePropertyChanged("XAxisTitle");
            }
        }

        public string YAxisTitle
        {
            get
            {
                if (ChartViewModel != null)
                    return (string)ChartViewModel.Configuration[EnumChartConfiguration.YAXISTITLE];

                return "";
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.Configuration.Update(EnumChartConfiguration.YAXISTITLE, value);

                RaisePropertyChanged("YAxisTitle");
            }
        }

        public System.Windows.Media.Color ForegroundColor
        {
            get
            {
                if (ChartViewModel != null && ChartViewModel.Configuration[EnumChartConfiguration.FOREGROUNDCOLOR] != null)
                    return (System.Windows.Media.Color)ChartViewModel.Configuration[EnumChartConfiguration.FOREGROUNDCOLOR];

                return Colors.Black;
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.Configuration.Update(EnumChartConfiguration.FOREGROUNDCOLOR, value);

                RaisePropertyChanged("ForegroundColor");
            }
        }


        public System.Windows.Media.Color BackgroundColor
        {
            get
            {
                if (ChartViewModel != null && ChartViewModel.Configuration[EnumChartConfiguration.BACKGROUNDCOLOR] != null)
                    return (System.Windows.Media.Color)ChartViewModel.Configuration[EnumChartConfiguration.BACKGROUNDCOLOR];

                return Colors.Black;
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.Configuration.Update(EnumChartConfiguration.BACKGROUNDCOLOR, value);

                RaisePropertyChanged("BackgroundColor");
            }
        }

        public ObservableDictionary<EnumChartConfiguration, object> Configuration
        {
            get
            {
                if (ChartViewModel != null)
                    return ChartViewModel.Configuration;

                return null;
            }
        }

        public ObservableCollection<VraagViewModel> Questions { get; set; }

        private Chart _control;
        public Chart Control
        {
            get => _control;
            set
            {
                _control = value;

                RaisePropertyChanged("Control");
            }
        }

        private VraagViewModel _selectedQuestion;
        public VraagViewModel SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;

                RaisePropertyChanged("SelectedQuestion");
            }
        }

        public DocumentDesignerViewModel Designer { get; set; }

        public IChart ChartViewModel { get; set; }

        private Opdracht _opdracht;
        private string _mode;

        public ChartDialogViewModel()
        {
            Questions = new ObservableCollection<VraagViewModel>();
            SwitchAxisCommand = new RelayCommand<string>(SwitchAxis);
            AddChartCommand = new RelayCommand<Grid>(AddChart);
            TitleChangedCommand = new RelayCommand<string>((title) => Title = title);
            ForegroundColorChangedCommand = new RelayCommand<System.Windows.Media.Color>((color) => ForegroundColor = color);
            BackgroundColorChangedCommand = new RelayCommand<System.Windows.Media.Color>((color) => BackgroundColor = color);
            QuestionChangedCommand = new RelayCommand<VraagViewModel>(QuestionChanged);
            XAxisTextChangedCommand = new RelayCommand<string>(x => XAxisTitle = x);
            YAxisTextChangedCommand = new RelayCommand<string>(x => YAxisTitle = x);
        }

        public void Create(DocumentDesignerViewModel designer, string mode, Opdracht opdracht, RapportageRepository rapportageRepository)
        {
            _opdracht = opdracht;
            _mode = mode;
            Designer = designer;
            _rapportageRepository = rapportageRepository;
            IsXAxis = true;

            foreach (Vraag vraag in opdracht.Inspectieformulier.FirstOrDefault().Vraag.Where(x => x.Vraagtype == "mv"))
            {
                if (vraag.Antwoorden.Count > 0)
                {
                    VraagViewModel questionViewModel = new VraagViewModel(vraag);
                    questionViewModel.TypeDescription = _rapportageRepository.GetQuestionType(questionViewModel.Type).Beschrijving;

                    Questions.Add(questionViewModel);
                }
            }

            switch (_mode)
            {
                case "Bar":
                    ChartViewModel = new BarChartViewModel();

                    break;
                case "Line":
                    ChartViewModel = new LineChartViewModel();

                    break;
                case "Pie":
                    ChartViewModel = new PieChartViewModel()
                    {
                        ShowLabels = true
                    };
                    break;
            }

            if (ChartViewModel != null)
                Control = ChartViewModel.BuildControl();

            ChartViewModel.OnLoaded();
            RaisePropertyChanged("Configuration");
            RaisePropertyChanged("ForegroundColor");
            RaisePropertyChanged("BackgroundColor");
        }

        public void SwitchAxis(string axis)
        {
            if(axis == "X")
            {
                IsXAxis = true;
                IsYAxis = false;
            }
            else
            {
                IsXAxis = false;
                IsYAxis = true;
            }
        }

        private void QuestionChanged(VraagViewModel question)
        {
            SelectedQuestion = question;

            if(ChartViewModel != null)
                ChartViewModel.Update(_rapportageRepository.GetChartData(question.ID));
        }

        public void AddChart(Grid grid)
        {
            byte[] data = grid.ToByteArray();
            UploadModel result = (new UploadClient()).UploadImage(new FileData(data));

            if (AddRequested != null)
                AddRequested.Invoke(Designer, result);
        }
    }
}
