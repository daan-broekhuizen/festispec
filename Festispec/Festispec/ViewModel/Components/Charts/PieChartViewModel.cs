using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Model;
using Festispec.Model.Enums;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace Festispec.ViewModel.Components.Charts
{
    public class PieChartViewModel : BaseChartViewModel
    {
        public Func<ChartPoint, string> LabelPoints { get; set; }

        public bool ShowLabels { get; set; }

        public LegendLocation LegendLocation { get; set; }

        public PieChartViewModel() : base()
        {
            LegendLocation = LegendLocation.Bottom;
        }

        public PieChartViewModel(List<string> labels, List<double> values) : base(labels, values)
        {
            LegendLocation = LegendLocation.Bottom;
        }

        public override void CreateCollection()
        {
            Collection = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title = "TITLE",
                    Values = new ChartValues<double>() { 25 },
                    DataLabels = ShowLabels
                }
            };
        }

        public override Chart BuildControl()
        {
            PieChart cc = new PieChart()
            {
                Series = Collection,
                LegendLocation = LegendLocation
            };

            _control = cc;

            return cc;
        }

        protected override void ChartValuesChanged(object sender, NotifyCollectionChangedEventArgs e) {}

        public override void Update(List<string> labels, List<double> values)
        {
            Labels = new ObservableCollection<string>(labels);
            Values = new ObservableCollection<double>(values);

            Collection.Clear();

            for (int i = 0; i < Values.Count; i++)
            {
                double val = Values[i];
                string label = Labels[i];

                Collection.Add(new PieSeries()
                {
                    Title = label,
                    Values = new ChartValues<double>() { val },
                    DataLabels = ShowLabels
                });
            }
        }
    }
}
