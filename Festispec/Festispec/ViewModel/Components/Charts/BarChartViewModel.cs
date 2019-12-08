using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Festispec.ViewModel.Components.Charts
{
    public class BarChartViewModel : CartesianChartViewModel
    {
        public BarChartViewModel(string title, List<string> labels, List<double> values) : base(labels, values)
        {
            Title = title;
            ForegroundColor = Colors.Black;
        }

        public BarChartViewModel(string title) : base()
        {

        }

        public override void ApplyColor()
        {
            ((ColumnSeries)Collection[0]).Stroke = new SolidColorBrush(ForegroundColor);
        }

        public override void CreateCollection()
        {
            Collection = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Title = Title,
                    Values = new ChartValues<double>(Values),
                }
            };
        }
    }
}
