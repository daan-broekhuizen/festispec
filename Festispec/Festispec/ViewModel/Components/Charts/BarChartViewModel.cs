using Festispec.ViewModel.Components.Charts.Data;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Festispec.ViewModel.Components.Charts
{
    public class BarChartViewModel : BaseChartViewModel
    {
        private string _xAxis;
        public string XAxis
        {
            get => _xAxis;
            set
            {
                _xAxis = value;
                RaisePropertyChanged("XAxis");
            }
        }

        private string _yAxis;
        public string YAxis
        {
            get => _yAxis;
            set
            {
                _yAxis = value;
                RaisePropertyChanged("YAxis");
            }
        }

        public BarChartViewModel(string title, GeneralChartData chartData) : base(chartData)
        {
            Collection = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Title = title,
                    Values = new ChartValues<double>(ChartData.Values),
                }
            };

            /*
            Labels = new string[labels.Count()];

            for (int i = 0; i < labels.Count(); i++)
                Labels[i] = labels.ElementAt(i);
                */
            XAxis = "XAXIS";
            YAxis = "YAXIS";
            Title = "TITLE";
            ForegroundColor = Colors.Black;
        }

        public override void ApplyColor()
        {
            ((ColumnSeries)Collection[0]).Stroke = new SolidColorBrush(ForegroundColor);
        }
    }
}
