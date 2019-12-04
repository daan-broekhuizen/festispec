using Festispec.ViewModel.Components.Charts.Data;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
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

        public GeneralChartData ChartData { get; set; }

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

        protected Chart _control;

        public BaseChartViewModel() { }

        public BaseChartViewModel(GeneralChartData chartData)
        {
            ChartData = chartData;
            ChartData.Values.CollectionChanged += ChartValuesChanged;
        }

        protected virtual void ChartValuesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: Multiple
            Collection[0].Values.Clear();
            foreach (int i in ChartData.Values)
                Collection[0].Values.Add(i);
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

        public abstract void ApplyColor();
    }
}
