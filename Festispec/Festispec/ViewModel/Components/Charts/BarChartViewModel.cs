using Festispec.ViewModel.Components.Charts.Data;
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
    public class BarChartViewModel : CartesianChartViewModel
    {
        public BarChartViewModel(string title, GeneralChartData chartData) : base(chartData)
        {
            Collection = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Title = title,
                    Values = new ChartValues<int>(ChartData.Values),
                }
            };

            /*
            Labels = new string[labels.Count()];

            for (int i = 0; i < labels.Count(); i++)
                Labels[i] = labels.ElementAt(i);
                */
            ForegroundColor = Colors.Black;
        }

        public override void ApplyColor()
        {
            ((ColumnSeries)Collection[0]).Stroke = new SolidColorBrush(ForegroundColor);
        }
    }
}
