using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Festispec.Model;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Festispec.ViewModel
{
    public class OfferteGraphViewModel : ManagementViewModel
    {
        #region propertiesOfferte
        private SeriesCollection _offertesCollection;

        public SeriesCollection OffertesCollection
        {
            get { return _offertesCollection; }
            set
            {
                _offertesCollection = value;
                RaisePropertyChanged("OffertesCollection");
            }
        }

        private ObservableValue _offertesRejected;

        public ObservableValue OffertesRejected
        {
            get { return _offertesRejected; }
            set
            {
                _offertesRejected = value;
                RaisePropertyChanged("OffertesRejected");
            }
        }

        private ObservableValue _offertesAccepted;

        public ObservableValue OffertesAccepted
        {
            get { return _offertesAccepted; }
            set
            {
                _offertesAccepted = value;
                RaisePropertyChanged("OffertesAccepted");
            }
        }
        private ObservableValue _offertesSent;

        public ObservableValue OffertesSent
        {
            get { return _offertesSent; }
            set
            {
                _offertesSent = value;
                RaisePropertyChanged("OffertesSent");
            }
        }


        #endregion

        public OfferteGraphViewModel()
        {
            OffertesRejected = new ObservableValue();
            OffertesSent = new ObservableValue();
            OffertesAccepted = new ObservableValue();

            OffertesRejected.Value = GetOffertesRejected();
            OffertesSent.Value = GetOffertesSent();
            OffertesAccepted.Value = GetOffertesAccepted();

            OffertesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Afgewezen",
                    Values = new ChartValues<ObservableValue> { OffertesRejected},
                    DataLabels = true,
                    Fill = Brushes.Red
                },
                 new PieSeries
                {
                    Title = "Verzonden",
                    Values = new ChartValues<ObservableValue>{ OffertesSent },
                    DataLabels = true,
                    Fill = Brushes.Yellow
                },
                new PieSeries
                {
                    Title = "Goedgekeurd",
                    Values = new ChartValues<ObservableValue>{ OffertesAccepted },
                    DataLabels = true,
                    Fill = Brushes.Green
                }
        };
        }

        public int GetOffertesRejected()
        {
            int amountOfOffertes = _qrepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).Count();
            int amountOfJobs = _qrepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).Select(e => e.OpdrachtID).Distinct().Count();

            int counter = 0;
            List<Offerte> JobsBetweenDates = _qrepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).ToList();
            if (JobsBetweenDates.Count() != 0)
            {
                JobsBetweenDates.OrderByDescending(e => e.Aanmaakdatum).GroupBy(e => e.OpdrachtID).ToList().ForEach(e =>
                {
                    if (e.FirstOrDefault().Opdracht.Status.Equals("Offerte geweigerd"))
                    {
                        counter++;
                    }
                });
            }

            return (amountOfOffertes - amountOfJobs) + counter;
        }

        public int GetOffertesAccepted()
        {
            int counter = 0;
            List<Opdracht> JobsWithStatus = _jrepo.GetOpdrachtenWithQuotations().Where(e => e.Status.Equals("Offerte geaccepteerd")).ToList();

            JobsWithStatus.Select(e => e.Offerte).ToList().ForEach(e =>
            {
                if (e.FirstOrDefault().Aanmaakdatum > StartDate && e.FirstOrDefault().Aanmaakdatum < EndDate)
                {
                    counter++;
                }
            });
            return counter;
        }

        public int GetOffertesSent()
        {
            int counter = 0;
            List<Opdracht> JobsWithStatus = _jrepo.GetOpdrachtenWithQuotations().Where(e => e.Status.Equals("Offerte verstuurt")).ToList();

            JobsWithStatus.Select(e => e.Offerte).ToList().ForEach(e =>
            {
                if (e.FirstOrDefault().Aanmaakdatum > StartDate && e.FirstOrDefault().Aanmaakdatum < EndDate)
                {
                    counter++;
                }
            });
            return counter;
        }
    }
}
