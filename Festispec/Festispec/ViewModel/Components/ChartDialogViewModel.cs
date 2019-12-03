using Festispec.API;
using Festispec.API.ImageShack;
using Festispec.Model.Enums;
using Festispec.ViewModel.Components.Charts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
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

        public DocumentDesignerViewModel Designer { get; set; }

        public IChart ChartViewModel { get; set; }

        public ChartDialogViewModel()
        {
            SwitchAxisCommand = new RelayCommand<string>(SwitchAxis);
            AddChartCommand = new RelayCommand<ContentControl>((x) => AddChart((Chart)x.Content));
            ForegroundColorChangedCommand = new RelayCommand<System.Windows.Media.Color>((color) => ForegroundColor = color);
            TitleChangedCommand = new RelayCommand<string>((title) => Title = title);
        }

        public void Create(DocumentDesignerViewModel designer, string mode)
        {
            this.Designer = designer;
            IsXAxis = true;

            switch (mode)
            {
                case "Bar":
                    ChartViewModel = new BarChartViewModel("Test", new List<double>() { 10, 20, 30 }, new List<string>() { "A", "B", "C" });

                    break;
                case "Line":
                    ChartViewModel = new LineChartViewModel("Test", new List<double>() { 10, 20, 30 }, new List<string>() { "A", "B", "C" });

                    break;
                case "Pie":
                    break;
            }

            if(ChartViewModel != null)
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

        public void AddChart(Chart chart)
        {
            byte[] data = ChartViewModel.ToByteArray();

            UploadModel result = (new ImageShackClient()).UploadImage(new FileData(data, "image/png"));

            if (AddRequested != null)
                AddRequested.Invoke(Designer, result);
        }

    }
}
