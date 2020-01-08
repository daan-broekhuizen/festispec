using Festispec.Model.Enums;
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

        public IList<string> XAxisLabels
        {
            get => XAxis != null ? XAxis.Labels : new List<string>();
            set
            {
                if (XAxis != null)
                    XAxis.Labels = value;
            }
        }

        public IList<string> YAxisLabels
        {
            get => YAxis != null ? YAxis.Labels : new List<string>();
            set
            {
                if (YAxis != null)
                    YAxis.Labels = value;
            }
        }

        public LegendLocation LegendLocation { get; set; }

        public CartesianChartViewModel() : base()
        {
            Labels.Add("");
            Labels.Add("");

            Values.Add(1);
            Values.Add(1);
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
                AxisY = new AxesCollection(),
                LegendLocation = LegendLocation
            };

            if(LegendLocation > 0)
                cc.LegendLocation = LegendLocation;

            XAxis = new Axis();
            YAxis = new Axis();

            if (Labels != null && Labels.Count > 0)
            {
                XAxis.ShowLabels = true;
                XAxis.Labels = Labels;
            }

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
                case EnumChartConfiguration.XAXISLABELS:
                    XAxisLabels = (List<string>)value;

                    break;
                case EnumChartConfiguration.YAXISLABELS:
                    YAxisLabels = (List<string>)value;
                    break;
            }
        }
    }
}
