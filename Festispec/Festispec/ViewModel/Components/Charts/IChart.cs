using LiveCharts;
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
    public interface IChart
    {
        string Title { get; set; }

        SeriesCollection Collection { get; set; }

        string[] Labels { get; set; }

        Color ForegroundColor { get; set; }

        Chart BuildControl();

        /// <summary>
        /// Zet de grafiek om naar een byte array.
        /// </summary>
        /// <returns>De grafiek als byte array</returns>
        byte[] ToByteArray();
    }
}
