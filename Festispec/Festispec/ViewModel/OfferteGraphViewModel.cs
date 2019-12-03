using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;

namespace Festispec.ViewModel
{
    public class OfferteGraphViewModel
    {
        private GraphRepository _repo;

        public SeriesCollection OffertesCollection { get; set; }

        public OfferteGraphViewModel()
        {
            _repo = new GraphRepository();

            OffertesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Afgewezen",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_repo.GetOffertesAmountByStatus)}
            }
        }
    }
}
