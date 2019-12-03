using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Festispec.Model.Repositories;
using FestiSpec.Domain.Repositories;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Festispec.ViewModel
{
    public class OfferteGraphViewModel
    {
        private JobRepository _jrepo;
        private QuotationRepository _qrepo;

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = ; }
        }


        public string SelectedItem { get; set; }

        public SeriesCollection OffertesCollection { get; set; }

        public OfferteGraphViewModel(JobRepository Jrepo, QuotationRepository Qrepo )
        {
            _jrepo = Jrepo;
            _qrepo = Qrepo;
            OffertesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Afgewezen",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(GetOffertesRejected())},
                    DataLabels = true,
                    Fill = Brushes.Red
                },
                 new PieSeries
                {
                    Title = "Verzonden",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(GetOffertesSent()) },
                    DataLabels = true,
                    Fill = Brushes.Yellow
                },
                new PieSeries
                {
                    Title = "Goedgekeurd",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(GetOffertesAccepted()) },
                    DataLabels = true,
                    Fill = Brushes.Green
                }
        };
            }

        public int GetOffertesRejected()
        {
            int amountOfOffertes = _qrepo.GetQuotations().Count();
            int amountOfJobs = _qrepo.GetQuotations().Select(e => e.OpdrachtID).Distinct().Count();

            int amountOfRejectedOffertes = _jrepo.GetOpdrachtenWithQuotations().Where(e => e.Status.Equals("Offerte geweigerd") && e.Offerte.Where(e=> e.Aanmaakdatum >= StartDate && e.Aanmaakdatum < EndDate).Count();

            return (amountOfOffertes - amountOfJobs) + amountOfRejectedOffertes;
        }

        public int GetOffertesAccepted()
        {
            return _jrepo.GetOpdrachten().Where(e => e.Status.Equals("Offerte geaccepteerd")).Count();
        }

        public int GetOffertesSent()
        {
            return _jrepo.GetOpdrachten().Where(e => e.Status.Equals("Offerte verstuurt")).Count();
        }
    }
}
