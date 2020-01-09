using Festispec.Model.Enums;
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
        public Brush BackgroundColor
        {
            get => ((Series)Collection[0]).Fill;
            set
            {
                if (Collection != null)
                    ((Series)Collection[0]).Fill = value;
            }
        }

        public BarChartViewModel() : base()
        {

        }

        public BarChartViewModel(List<string> labels, List<double> values) : base(labels, values)
        {

        }

        public override void CreateCollection()
        {
            Collection = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Values = new ChartValues<double>(Values)
                }
            };
        }

        public override void Configure()
        {
            base.Configure();

            Configuration.Update(EnumChartConfiguration.BACKGROUNDCOLOR, Colors.Black);
        }

        public override void OnConfigurationOptionChanged(EnumChartConfiguration key, object value)
        {
            base.OnConfigurationOptionChanged(key, value);

            switch (key)
            {
                case EnumChartConfiguration.BACKGROUNDCOLOR:
                    BackgroundColor = new SolidColorBrush((Color)value);

                    break;
            }
        }

        public override void OnLoaded()
        {
            BackgroundColor = new SolidColorBrush((Color)Configuration[EnumChartConfiguration.BACKGROUNDCOLOR]);
        }
    }
}
