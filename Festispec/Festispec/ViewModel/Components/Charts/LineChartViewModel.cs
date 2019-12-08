using Festispec.ViewModel.RapportageViewModels;
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
        public LineChartViewModel(string title, List<string> labels, List<double> values) : base(labels, values)
        {
            Title = "TITLE";
            ForegroundColor = Colors.Black;
        }

        public LineChartViewModel(string title) : base()
        {

        }

        public override void CreateCollection()
        {
            Collection = new SeriesCollection()
            {
                new LineSeries()
                {
                    Title = Title,
                    Values = new ChartValues<double>(Values)
                }
            };
        }

        public override void ApplyColor()
        {
            ((LineSeries)Collection[0]).Stroke = new SolidColorBrush(ForegroundColor);
        }
    }
}
