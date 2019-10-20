using Festispec.Model.Misc;
using Festispec.Model.Repositories;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Festispec.ViewModel.Graph
{
    public class OffertesGraphViewModel : ViewModelBase
    {
        private GraphRepository _repo;

        public SeriesCollection OffertesCollection { get; set; }

        public OffertesGraphViewModel()
        {
            this._repo = new GraphRepository();

            this.OffertesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Afgewezen",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(this._repo.GetOffertesAmountByStatus(EnumOfferteStatus.DENIED)) },
                    DataLabels = true,
                    Fill = Brushes.Red
                },
                 new PieSeries
                {
                    Title = "Verzonden",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(this._repo.GetOffertesAmountByStatus(EnumOfferteStatus.SEND)) },
                    DataLabels = true,
                    Fill = Brushes.Yellow
                },
                new PieSeries
                {
                    Title = "Goedgekeurd",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(this._repo.GetOffertesAmountByStatus(EnumOfferteStatus.APPROVED)) },
                    DataLabels = true,
                    Fill = Brushes.Green
                }
            };
        }
    }
}
