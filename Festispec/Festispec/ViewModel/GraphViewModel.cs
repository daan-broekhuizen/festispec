using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using BingMapsRESTToolkit;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Festispec.ViewModel
{
    public class GraphViewModel : ViewModelBase
    {
        #region Properties
        private JobRepository _jrepo;
        private QuotationRepository _qrepo;
        private UserRepository _urepo;
        private CustomerRepository _crepo;

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                setEndDate();
                SetGraph();
                SetSalesLabels();
                RaisePropertyChanged("StartDate");
            }
        }



        public DateTime EndDate { get; set; }
        private ComboBoxItem _selectedItem;

        public ComboBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                setEndDate();
                SetGraph();
                SetSalesLabels();
                SetSalesValues();
                RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion

        #region propertiesOfferte
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


        #endregion

        #region propertiesSales
        public SeriesCollection SalesCollection { get; set; }
        public string[] Labels { get; set; }

        private ChartValues<ObservableValue> _salesValues;

        public ChartValues<ObservableValue> SalesValues
        {
            get { return _salesValues; }
            set
            {
                _salesValues = value;
                RaisePropertyChanged("SalesValues");
            }
        }
        #endregion

        #region propertiesInspectors
        public SeriesCollection InspectorCollection { get; set; }

        private ChartValues<ObservableValue> _inspectorValues;

        public ChartValues<ObservableValue> InspectorValues
        {
            get { return _inspectorValues; }
            set
            {
                _inspectorValues = value;
                RaisePropertyChanged("InspectorValues");
            }
        }
        #endregion

        List<Klant> Customers { get; set; }
        public string[] ProvinceLabels { get; set; }
        int[] _Values { get; set; }
        public SeriesCollection ProvinceCollection { get; set; }

        private ChartValues<ObservableValue> _provinceValues;

        public ChartValues<ObservableValue> ProvinceValues
        {
            get { return _provinceValues; }
            set { _provinceValues = value; }
        }


        public GraphViewModel(JobRepository Jrepo, QuotationRepository Qrepo, UserRepository Urepo, CustomerRepository Crepo)
        {
            // Offerte Graph
            OffertesRejected = new ObservableValue();
            OffertesSent = new ObservableValue();
            OffertesAccepted = new ObservableValue();
            SalesValues = new ChartValues<ObservableValue> { };
            InspectorValues = new ChartValues<ObservableValue> { };
            ProvinceValues = new ChartValues<ObservableValue> { };
            Random r = new Random();

            Customers = Crepo.GetCustomers();

            _jrepo = Jrepo;
            _qrepo = Qrepo;
            _urepo = Urepo;
            _crepo = Crepo;
            StartDate = new DateTime(2019, 11,11);
            setEndDate();
            EndDate = _startDate.AddMonths(1);
            OffertesRejected.Value = GetOffertesRejected();
            OffertesSent.Value = GetOffertesSent();
            OffertesAccepted.Value = GetOffertesAccepted();
            SetSalesValues();
            SetInspectorValues();
            SetProvinceLabels();
            SetProvinceValues();
            setValues();

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
            //End offerte graph

            //Sales graph
            SalesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Sales",
                    Values= SalesValues
                }
            };

            InspectorCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Aantal Inspecteurs certificering gehaald",
                    Values = InspectorValues
                }
            };

            ProvinceCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Aantal Inspecteurs certificering gehaald",
                    Values = ProvinceValues
                }
            };

            SetSalesLabels();

            }

        private void SetSalesLabels()
        {
            Labels = new string[12];
            Array months = Enum.GetValues(typeof(EnumMonth));
            for (int i = 0; i < months.Length; i++)
            {
                EnumMonth month = (EnumMonth)months.GetValue(i);
                this.Labels[i] = char.ToUpper(month.ToString()[0]) + month.ToString().Substring(1).ToLower();
            }
        }

        private void SetProvinceLabels()
        {
            ProvinceLabels = new string[12];
            Array provinces = Enum.GetValues(typeof(EnumProvince));
            for (int i = 0; i < provinces.Length; i++)
            {
                EnumProvince province = (EnumProvince)provinces.GetValue(i);
                this.ProvinceLabels[i] = char.ToUpper(province.ToString()[0]) + province.ToString().Substring(1).ToLower();                
            }

        }

        private void SetSalesValues()
        {
            SalesValues.ToList().ForEach(e => SalesValues.Remove(e));   
            double[] _salesValues = new double[12];


            _qrepo.GetQuotations().Where(e => e.Aanmaakdatum.Year == StartDate.Year).OrderByDescending(e => e.Aanmaakdatum).GroupBy(e => e.OpdrachtID).ToList().ForEach(e =>
            {
                if (e.FirstOrDefault().Opdracht.Status.Equals("Offerte geaccepteerd"))
                {
                    _salesValues[e.FirstOrDefault().Aanmaakdatum.Month - 1] += (double)e.FirstOrDefault().Totaalbedrag;
                }
            });

            for(int i = 0; i < _salesValues.Length; i++)
            {
                SalesValues.Add(new ObservableValue(_salesValues[i]));
            }
        }

        private void SetInspectorValues()
        {
            InspectorValues.ToList().ForEach(e => InspectorValues.Remove(e));
            int[] _inspectorValues = new int[12];
            List<Account> accounts = _urepo.GetUsers();

            _urepo.GetUsers().Where(e => e.Rol.Equals("in") && e.DatumCertificering.Value.Year == StartDate.Year).ToList().ForEach(e =>
            {
                _inspectorValues[e.DatumCertificering.Value.Month - 1] += 1;
            });

            for(int i = 0; i < _inspectorValues.Length; i++)
            {
                InspectorValues.Add(new ObservableValue(_inspectorValues[i]));
            }
        }

        private void SetProvinceValues()
        {
            ProvinceValues.ToList().ForEach(e => ProvinceValues.Remove(e));
           _Values = new int[12];

            Array provinces = Enum.GetValues(typeof(EnumProvince));
            string CustomerProvince;

            _crepo.GetCustomers().Select(c => new CustomerViewModel(c)).ToList().ForEach(e =>
            {
                if (e.Province != null)
                {
                    CustomerProvince = e.Province;
                }
                else
                {
                    CustomerProvince = "";
                }

                int o = 0;
                for (int i = 0; i < provinces.Length; i++)
                {
                    EnumProvince province = (EnumProvince)provinces.GetValue(i);
                    string EnumProvince = char.ToUpper(province.ToString()[0]) + province.ToString().Substring(1).ToLower();

                    if (CustomerProvince.Equals(EnumProvince))
                    {
                        _Values[i] += 1;
                    }
                }
            });
            /*            _provinceValues[4] = 1;
                        _provinceValues[8] = 1;
                        _provinceValues[9] = 2;*/
        }
        private void setValues()
        {
            for (int i = 0; i < _Values.Length; i++)
            {
                ProvinceValues.Add(new ObservableValue(_Values[i]));
            }
        }

        private void SetGraph()
        {
            OffertesRejected.Value = GetOffertesRejected();
            OffertesAccepted.Value = GetOffertesAccepted();
            OffertesSent.Value = GetOffertesSent();
            SetSalesValues();
            SetInspectorValues();
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
