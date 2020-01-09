using Festispec.Model.Enums;
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
        public Brush ForegroundColor
        {
            get => ((Series)Collection[0]).Stroke;
            set
            {
                if (Collection != null)
                    ((Series)Collection[0]).Stroke = value;
            }
        }

        public Brush BackgroundColor
        {
            get => ((Series)Collection[0]).Fill;
            set
            {
                if (Collection != null)
                    ((Series)Collection[0]).Fill = value;
            }
        }

        public LineChartViewModel() : base()
        {

        }

        public LineChartViewModel(List<string> labels, List<double> values) : base(labels, values)
        {

        }

        public override void CreateCollection()
        {
            Collection = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<double>(Values),
                    Title = "Aantal keer geantwoord"
                }
            };
        }

        public override void Configure()
        {
            base.Configure();

            Configuration.Update(EnumChartConfiguration.FOREGROUNDCOLOR, Colors.Black);
            Configuration.Update(EnumChartConfiguration.BACKGROUNDCOLOR, Colors.White);
        }


        public override void OnConfigurationOptionChanged(EnumChartConfiguration key, object value)
        {
            base.OnConfigurationOptionChanged(key, value);
            switch (key)
            {
                case EnumChartConfiguration.FOREGROUNDCOLOR:
                    ForegroundColor = new SolidColorBrush((Color)value);

                    break;
                case EnumChartConfiguration.BACKGROUNDCOLOR:
                    BackgroundColor = new SolidColorBrush((Color)value);

                    break;

            }
        }

        public override void OnLoaded()
        {
            ForegroundColor = new SolidColorBrush((Color)Configuration[EnumChartConfiguration.FOREGROUNDCOLOR]);
            BackgroundColor = new SolidColorBrush((Color)Configuration[EnumChartConfiguration.BACKGROUNDCOLOR]);
        }
    }
}
