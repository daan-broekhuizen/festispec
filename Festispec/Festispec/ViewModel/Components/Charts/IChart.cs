using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Utility;
using Festispec.ViewModel.RapportageViewModels;
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
    public interface IChart
    {
        SeriesCollection Collection { get; set; }

        ObservableCollection<double> Values { get; set; }

        ObservableCollection<string> Labels { get; set; }

        /// <summary>
        /// Customization options.
        /// </summary>
        ObservableDictionary<EnumChartConfiguration, object> Configuration { get; set; }

        /// <summary>
        /// Creates the SeriesCollection.
        /// </summary>
        void CreateCollection();

        /// <summary>
        /// Builds the Chart Control.
        /// </summary>
        /// <returns>The Control.</returns>
        Chart BuildControl();

        /// <summary>
        /// Zet de grafiek om naar een byte array.
        /// </summary>
        /// <returns>De grafiek als byte array.</returns>
        byte[] ToByteArray();

        /// <summary>
        /// Configure the customization here.
        /// </summary>
        void Configure();

        void OnLoaded();

        void UpdateLabels(List<string> labels);

        void UpdateValues(List<double> values);

        void Update(List<ChartData> chartData);

        void OnConfigurationOptionChanged(EnumChartConfiguration key, object value);
    }
}
