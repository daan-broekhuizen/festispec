using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Components.Charts
{
    public class CartesianChartViewModel : BaseChartViewModel
    {
        public override void ApplyColor(){}

        public Axis XAxis { get; set; }
        public Axis YAxis { get; set; }

        public CartesianChartViewModel(List<string> labels, List<double> values) : base(labels, values)
        {
        }

        public CartesianChartViewModel() : base()
        {
            Labels.Add("Test1");
            Labels.Add("Test2");

            Values.Add(10);
            Values.Add(20);
        }

        public override Chart BuildControl()
        {
            CartesianChart cc = new CartesianChart
            {
                Series = Collection,
                AxisX = new AxesCollection(),
                AxisY = new AxesCollection()
            };

            _control = cc;

            return cc;
        }

        public override void CreateCollection() { }
    }
}
