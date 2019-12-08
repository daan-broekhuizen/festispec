using Festispec.ViewModel.Components.Charts.Data;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
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
    public class LineChartViewModel : CartesianChartViewModel
    {
        public LineChartViewModel(string title, GeneralChartData chartData) : base(chartData)
        {
            Collection = new SeriesCollection()
            {
                new LineSeries()
                {
                    Title = title,
                    Values = new ChartValues<int>(ChartData.Values)
                }
            };

            /*
            Labels = new string[labels.Count()];

            for (int i = 0; i < labels.Count(); i++)
                Labels[i] = labels.ElementAt(i);
            */
            Title = "TITLE";
            ForegroundColor = Colors.Black;
        }

        public override void ApplyColor()
        {
            ((LineSeries)Collection[0]).Stroke = new SolidColorBrush(ForegroundColor);
        }
    }
}
