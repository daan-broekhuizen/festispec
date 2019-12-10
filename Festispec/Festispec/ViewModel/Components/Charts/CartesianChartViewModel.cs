using Festispec.Model.Enums;
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
    public class CartesianChartViewModel : BaseChartViewModel
    {
        public Axis XAxis { get; set; }
        public Axis YAxis { get; set; }

        public string XAxisTitle
        {
            get => XAxis != null ? XAxis.Title : "";
            set
            {
                if (XAxis != null)
                    XAxis.Title = value;
            }
        }

        public string YAxisTitle
        {
            get => YAxis != null ? YAxis.Title : "";
            set
            {
                if (YAxis != null)
                    YAxis.Title = value;
            }
        }

        public CartesianChartViewModel() : base()
        {
            Labels.Add("Test1");
            Labels.Add("Test2");

            Values.Add(10);
            Values.Add(20);
        }

        public CartesianChartViewModel(List<string> labels, List<double> values) : base(labels, values)
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

            XAxis = new Axis();
            YAxis = new Axis();

            cc.AxisX.Clear();
            cc.AxisX.Add(XAxis);

            cc.AxisY.Clear();
            cc.AxisY.Add(YAxis);

            _control = cc;

            return cc;
        }

        public override void CreateCollection() { }

        public override void Configure()
        {
            base.Configure();

            Configuration.Update(EnumChartConfiguration.AXIS, true);
        }

        public override void OnConfigurationOptionChanged(EnumChartConfiguration key, object value)
        {
            switch(key)
            {
                case EnumChartConfiguration.XAXISTITLE:
                    XAxisTitle = (string)value;

                    break;
                case EnumChartConfiguration.YAXISTITLE:
                    YAxisTitle = (string)value;

                    break;
            }
        }
    }
}
