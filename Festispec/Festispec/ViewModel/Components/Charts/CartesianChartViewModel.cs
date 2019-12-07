using Festispec.ViewModel.Components.Charts.Data;
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

        public CartesianChartViewModel(GeneralChartData data) : base(data)
        {
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
    }
}
