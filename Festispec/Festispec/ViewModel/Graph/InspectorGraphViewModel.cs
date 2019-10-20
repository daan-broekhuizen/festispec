using Festispec.Model;
using Festispec.Model.Misc;
using Festispec.Model.Repositories;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Graph
{
    public class InspectorGraphViewModel : ViewModelBase
    {
        private GraphRepository _repo;

        public SeriesCollection InspectorCollection { get; set; }

        public string[] MonthLabels { get; set; }

        public InspectorGraphViewModel()
        {
            this._repo = new GraphRepository();
            this.MonthLabels = new string[12];

            this.InspectorCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Registraties",
                    Values = new ChartValues<double>(this._repo.GetInspectorValues())
                }
            };

            Array months = Enum.GetValues(typeof(EnumMonth));
            for (int i = 0; i < months.Length; i++)
            {
                EnumMonth month = (EnumMonth)months.GetValue(i);
                this.MonthLabels[i] = char.ToUpper(month.ToString()[0]) + month.ToString().Substring(1).ToLower();
            }
        }
    }
}
