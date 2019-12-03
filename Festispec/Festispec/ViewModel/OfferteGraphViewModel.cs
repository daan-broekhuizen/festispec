using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Festispec.Model;
using Festispec.Model.Repositories;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Festispec.ViewModel
{
    public class OfferteGraphViewModel : ViewModelBase
    {
        private JobRepository _jrepo;
        private QuotationRepository _qrepo;

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value;
                setEndDate();
                setGraph();
                RaisePropertyChanged("StartDate");
            }
        }



        public DateTime EndDate { get; set; }
        private ComboBoxItem _selectedItem;

        public ComboBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }


        private SeriesCollection _offertesCollection;

        public SeriesCollection OffertesCollection
        {
            get { return _offertesCollection; }
            set { _offertesCollection = value;
            RaisePropertyChanged("OffertesCollection"); }
        }

        private ObservableValue _offertesRejected;

        public ObservableValue OffertesRejected
        {
            get { return _offertesRejected; }
            set { _offertesRejected = value;
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

        public OfferteGraphViewModel(JobRepository Jrepo, QuotationRepository Qrepo )
        {
            OffertesRejected = new ObservableValue();
            OffertesSent = new ObservableValue();
            OffertesAccepted = new ObservableValue();
            _jrepo = Jrepo;
            _qrepo = Qrepo;
            StartDate = new DateTime(2019, 11,11);
            setEndDate();
            EndDate = _startDate.AddMonths(1);
            // SelectedItem = "Week";
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

        private void setGraph()
        {
            OffertesRejected.Value = GetOffertesRejected();
            OffertesAccepted.Value = GetOffertesAccepted();
            OffertesSent.Value = GetOffertesSent();
            
        }

        private void setEndDate()
        {
            if (SelectedItem != null)
            {
                switch (SelectedItem.Content)
                {
                    case "Week":
                        EndDate = _startDate.AddDays(7);
                        break;
                    case "Maand":
                        EndDate = _startDate.AddMonths(1);
                        break;
                    case "Jaar":
                        EndDate = _startDate.AddYears(1);
                        break;
                }
            }
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
            return _jrepo.GetOpdrachten().Where(e => e.Status.Equals("Offerte geaccepteerd")).Count();
        }

        public int GetOffertesSent()
        {
            return _jrepo.GetOpdrachten().Where(e => e.Status.Equals("Offerte verstuurt")).Count();
        }
    }
}
