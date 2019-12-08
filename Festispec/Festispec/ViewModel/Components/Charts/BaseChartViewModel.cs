using Festispec.Model;
using Festispec.ViewModel.RapportageViewModels;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.Components.Charts
{
    public abstract class BaseChartViewModel : ViewModelBase, IChart
    {
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

        public SeriesCollection Collection { get; set; }

        private Color _foregroundColor;
        public Color ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;

                RaisePropertyChanged("ForegroundColor");
                ApplyColor();
            }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;

                RaisePropertyChanged("BackgroundColor");
                ApplyColor();
            }
        }

        protected Chart _control;

        public ObservableCollection<string> Labels { get; set; }
        public ObservableCollection<double> Values { get; set; }


        public BaseChartViewModel()
        {
            Labels = new ObservableCollection<string>();
            Values = new ObservableCollection<double>();

            Values.CollectionChanged += ChartValuesChanged;

            CreateCollection();
        }

        public BaseChartViewModel(List<string> labels, List<double> values) : this()
        {
            foreach (string label in labels)
                Labels.Add(label);

            foreach (double val in values)
                Values.Add(val);
        }

        protected virtual void ChartValuesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: Multiple
            Collection[0].Values.Clear();
            foreach (double val in Values)
                Collection[0].Values.Add(val);
        }

        public abstract Chart BuildControl();

        public byte[] ToByteArray()
        {
            byte[] data;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)_control.ActualWidth, (int)_control.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(_control);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            using (MemoryStream ms = new MemoryStream())
            {
                png.Save(ms);

                data = ms.ToArray();
            }

            return data;
        }

        public virtual void UpdateLabels(List<string> labels)
        {
            Labels.Clear();
            foreach (string label in labels)
                Labels.Add(label);
        }

        public virtual void UpdateValues(List<double> values)
        {
            Values.Clear();

            foreach (double val in values)
                Values.Add(val);
        }

        public virtual void Update(List<ChartData> chartData)
        {
            UpdateLabels(GetLabelsFromChartData(chartData));
            UpdateValues(GetValuesFromChartData(chartData));
        }

        protected List<string> GetLabelsFromChartData(List<ChartData> chartData)
        {
            List<string> labels = new List<string>();

            foreach (ChartData data in chartData)
                labels.Add(data.Answer);

            return labels;
        }

        protected List<double> GetValuesFromChartData(List<ChartData> chartData)
        {
            List<double> values = new List<double>();

            foreach (ChartData data in chartData)
                values.Add(data.Amount);

            return values;
        }

        public abstract void ApplyColor();

        public abstract void CreateCollection();
    }
}
