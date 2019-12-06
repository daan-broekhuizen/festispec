using Festispec.API;
using Festispec.API.ImageShack;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.ViewModel.Components.Charts;
using Festispec.ViewModel.Components.Charts.Data;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.Components
{
    public class ChartDialogViewModel : ViewModelBase
    {
        // Events
        public delegate void AddRequestedEventHandler(DocumentDesignerViewModel designer, UploadModel uploaded);

        public event AddRequestedEventHandler AddRequested;

        // Commands
        public ICommand SwitchAxisCommand { get; set; }
        public ICommand AddChartCommand { get; set; }
        public ICommand ForegroundColorChangedCommand { get; set; }
        public ICommand TitleChangedCommand { get; set; }
        public ICommand QuestionChangedCommand { get; set; }

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

        public string Title
        {
            get
            {
                if(ChartViewModel != null)
                    return ChartViewModel.Title;

                return "";
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.Title = value;

                RaisePropertyChanged("Title");
            }
        }

        public System.Windows.Media.Color ForegroundColor
        {
            get
            {
                if (ChartViewModel != null)
                    return ChartViewModel.ForegroundColor;

                return Colors.Black;
            }
            set
            {
                if (ChartViewModel != null)
                    ChartViewModel.ForegroundColor = value;

                RaisePropertyChanged("ForegroundColor");
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
            AddChartCommand = new RelayCommand(AddChart);
            ForegroundColorChangedCommand = new RelayCommand<System.Windows.Media.Color>((color) => ForegroundColor = color);
            TitleChangedCommand = new RelayCommand<string>((title) => Title = title);
            QuestionChangedCommand = new RelayCommand<VraagViewModel>(QuestionChanged);
        }

        public void Create(DocumentDesignerViewModel designer, string mode, Opdracht opdracht)
        {
            _opdracht = opdracht;
            _mode = mode;
            Designer = designer;
            IsXAxis = true;

            foreach (Vraag vraag in opdracht.Inspectieformulier.FirstOrDefault().Vraag.Where(x => x.Vraagtype == "mv"))
                Questions.Add(new VraagViewModel(vraag));

            GeneralChartData chartData = new GeneralChartData();
            chartData.UpdateValues(new List<double>() { 0, 0 });

            switch (_mode)
            {
                case "Bar":
                    ChartViewModel = new BarChartViewModel("Test", chartData);

                    break;
                case "Line":
                    ChartViewModel = new LineChartViewModel("Test", chartData);

                    break;
                case "Pie":
                    break;
            }

            if (ChartViewModel != null)
                Control = ChartViewModel.BuildControl();
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
            Title = question.Question;

            if(ChartViewModel != null)
                ChartViewModel.ChartData.Update(question);
        }

        public void AddChart()
        {
            byte[] data = ChartViewModel.ToByteArray();

            UploadModel result = (new ImageShackClient()).UploadImage(new FileData(data));

            if (AddRequested != null)
                AddRequested.Invoke(Designer, result);
        }

    }
}
