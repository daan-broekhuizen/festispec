using Festispec.Model;
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
        string Title { get; set; }

        SeriesCollection Collection { get; set; }

        ObservableCollection<double> Values { get; set; }

        ObservableCollection<string> Labels { get; set; }

        Color ForegroundColor { get; set; }

        void CreateCollection();

        Chart BuildControl();

        /// <summary>
        /// Zet de grafiek om naar een byte array.
        /// </summary>
        /// <returns>De grafiek als byte array</returns>
        byte[] ToByteArray();

        void UpdateLabels(List<string> labels);

        void UpdateValues(List<double> values);

        void Update(List<ChartData> chartData);
    }
}
