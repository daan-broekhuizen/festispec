using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Extensions;
using Festispec.ViewModel.Components.Charts;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.ViewModel
{
    public class ManagementViewModel : ViewModelBase
    {
        #region Properties
        public ICommand ExportCommand { get; set; }

        private JobRepository _jRepo;
        private QuotationRepository _qRepo;
        private UserRepository _uRepo;
        private CustomerRepository _cRepo;
        private InspectionFormRepository _iRepo;

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;

                UpdateCharts();
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

                UpdateCharts();
                RaisePropertyChanged("SelectedItem");
            }
        }

        public Chart PieChartControl { get; set; }

        private Chart _inspectionChartControl;
        public Chart InspectionChartControl
        {
            get => _inspectionChartControl;
            set
            {
                _inspectionChartControl = value;

                RaisePropertyChanged("InspectionChartControl");
            }
        }

        private Chart _salesChartControl;
        public Chart SalesChartControl
        {
            get => _salesChartControl;
            set
            {
                _salesChartControl = value;

                RaisePropertyChanged("SalesChartControl");
            }
        }

        public ObservableCollection<Pushpin> PushpinList { get; set; }
        public IChart PieChartViewModel { get; set; }

        public IChart InspectionChartViewModel { get; set; }

        public IChart SalesChartViewModel { get; set; }
        #endregion

        public ManagementViewModel(JobRepository jRepo, QuotationRepository qRepo, UserRepository uRepo, CustomerRepository cRepo, InspectionFormRepository iRepo)
        {
            _jRepo = jRepo;
            _qRepo = qRepo;
            _uRepo = uRepo;
            _cRepo = cRepo;
            _iRepo = iRepo;

            PushpinList = new ObservableCollection<Pushpin>();
            SetPushPinsAsync();

            ExportCommand = new RelayCommand<FrameworkElement>(Export);

            PieChartViewModel = new PieChartViewModel() { ShowLabels = true };
            PieChartControl = PieChartViewModel.BuildControl();

            InspectionChartViewModel = new BarChartViewModel();
            InspectionChartControl = InspectionChartViewModel.BuildControl();

            SalesChartViewModel = new BarChartViewModel();
            SalesChartControl = SalesChartViewModel.BuildControl();

            StartDate = DateTime.Now;

            UpdateCharts();
        }

        private void UpdateCharts()
        {
            SetEndDate();

            // Pie Chart
            PieChartViewModel.Update(
                new List<string>() { "Afgewezen", "Verzonden", "Goedgekeurd" },
                new List<double>() { GetOffertesRejected(), GetAmountOfOffertes("Offerte verstuurt"), GetAmountOfOffertes("Offerte geaccepteerd") }
            );

            // Inspection
            InspectionChartViewModel.UpdateLabels(GetLabels());
            InspectionChartViewModel.UpdateValues(GetInspectorsValues());

            InspectionChartViewModel.Configuration.Update(EnumChartConfiguration.XAXISLABELS, InspectionChartViewModel.Labels.ToList());

            // Sales
            SalesChartViewModel.UpdateLabels(GetLabels());
            SalesChartViewModel.UpdateValues(GetSalesValues());

            SalesChartViewModel.Configuration.Update(EnumChartConfiguration.XAXISLABELS, InspectionChartViewModel.Labels.ToList());
        }

        private void SetEndDate()
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

        private List<string> GetLabels()
        {
            List<string> labels = new List<string>();

            if (SelectedItem != null)
            {
                int size = (string)SelectedItem.Content == "Week" ? 7 : 30;
                switch (SelectedItem.Content)
                {
                    case "Week":
                        case "Maand":
                            for (int i = 0; i < size; i++)
                                labels.Add(StartDate.AddDays(i).ToShortDateString());

                        break;
                    case "Jaar":
                        Array months = Enum.GetValues(typeof(EnumMonth));
                        for (int i = 0; i < months.Length; i++)
                        {
                            EnumMonth month = (EnumMonth)months.GetValue(i);

                            labels.Add(char.ToUpper(month.ToString()[0]) + month.ToString().Substring(1).ToLower());
                        }
                        break;
                }
            }

            return labels;
        }

        private List<double> GetInspectorsValues()
        {
            List<double> response = new List<double>();

            int[] inspectorValues = new int[12];

            if (SelectedItem != null)
            {
                if ((string)SelectedItem.Content == "Week")
                    inspectorValues = new int[7];
                else if ((string)SelectedItem.Content == "Maand")
                    inspectorValues = new int[30];

                switch (SelectedItem.Content)
                {
                    case "Week":
                        case "Maand":
                            _uRepo.GetUsers().Where(e => e.Rol.Equals("in") && e.DatumCertificering != null && e.DatumCertificering.Value.Date >= StartDate && e.DatumCertificering.Value.Date <= EndDate).ToList().ForEach(e =>
                            {
                                for (int i = 0; i < InspectionChartViewModel.Labels.Count; i++)
                                {
                                    if (e.DatumCertificering.Equals(DateTime.Parse(InspectionChartViewModel.Labels[i])))
                                        inspectorValues[i] += 1;
                                }
                            });
                            break;
                    case "Jaar":
                        inspectorValues = new int[12];
                        
                        _uRepo.GetUsers().Where(e => e.Rol.Equals("in")&& e.DatumCertificering != null && e.DatumCertificering.Value.Year == StartDate.Year).ToList().ForEach(e =>
                        {
                            inspectorValues[e.DatumCertificering.Value.Month - 1] += 1;
                        });
                        break;
                }
            }

            for (int i = 0; i < inspectorValues.Length; i++)
                response.Add(inspectorValues[i]);

            return response;
        }


        private List<double> GetSalesValues()
        {
            List<double> response = new List<double>();

            double[] salesValues = new double[12];

            if (SelectedItem != null)
            {
                if ((string)SelectedItem.Content == "Week")
                    salesValues = new double[7];
                else if ((string)SelectedItem.Content == "Maand")
                    salesValues = new double[30];

                switch (SelectedItem.Content)
                {
                    case "Week":
                        case "Maand":
                            foreach (Offerte quotation in _qRepo.GetLatestQuotationForEachJob(StartDate, EndDate))
                            {
                                if (quotation != null)
                                {
                                    for (int i = 0; i < InspectionChartViewModel.Labels.Count; i++)
                                    {
                                        if (quotation.Aanmaakdatum.Equals(DateTime.Parse(InspectionChartViewModel.Labels[i])))
                                            salesValues[i] += (double)quotation.Totaalbedrag;
                                    }
                                }
                            }

                            break;
                    case "Jaar":
                        _qRepo.GetQuotations().Where(e => e.Aanmaakdatum.Year == StartDate.Year).OrderByDescending(e => e.Aanmaakdatum).GroupBy(e => e.OpdrachtID).ToList().ForEach(e =>
                        {
                            e.FirstOrDefault();
                            if (e.FirstOrDefault().Opdracht.Status.Equals("Offerte geaccepteerd"))
                                salesValues[e.FirstOrDefault().Aanmaakdatum.Month - 1] += (double)e.FirstOrDefault().Totaalbedrag;
                        });
                        break;
                }
            }

            for (int i = 0; i < salesValues.Length; i++)
                response.Add(salesValues[i]);

            return response;
        }

        public void Export(FrameworkElement control)
        {
            ((FrameworkElement)control.FindName("controlExport")).Visibility = Visibility.Collapsed;
            byte[] data = control.ToByteArray();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "png files (*.png)|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream dataStream = saveFileDialog.OpenFile())
                        dataStream.Write(data, 0, data.Length);

                    Process.Start(saveFileDialog.FileName);
                }
            }
            ((FrameworkElement)control.FindName("controlExport")).Visibility = Visibility.Visible;

        }

        public int GetOffertesRejected()
        {
            int amountOfOffertes = _qRepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).Count();
            int amountOfJobs = _qRepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).Select(e => e.OpdrachtID).Distinct().Count();

            int counter = 0;
            List<Offerte> jobsBetweenDates = _qRepo.GetQuotations().Where(e => e.Aanmaakdatum > StartDate && e.Aanmaakdatum < EndDate).ToList();
            if (jobsBetweenDates.Count() != 0)
            {
                jobsBetweenDates.OrderByDescending(e => e.Aanmaakdatum).GroupBy(e => e.OpdrachtID).ToList().ForEach(e =>
                {
                    if (e.FirstOrDefault().Opdracht.Status.Equals("Offerte geweigerd"))
                        counter++;
                });
            }

            return (amountOfOffertes - amountOfJobs) + counter;
        }

        public int GetAmountOfOffertes(string status)
        {
            int counter = 0;
            List<Opdracht> jobsWithStatus = _jRepo.GetOpdrachtenWithQuotations().Where(e => e.Status.Equals(status)).ToList();

            jobsWithStatus.Select(e => e.Offerte).ToList().ForEach(e =>
            {
                if (e.FirstOrDefault() != null)
                {
                    if (e.FirstOrDefault().Aanmaakdatum > StartDate && e.FirstOrDefault().Aanmaakdatum < EndDate)
                        counter++;
                }
            });
            return counter;
        }

        public async Task SetPushPinsAsync()
        {
            LocationService locationService = new LocationService();
            _uRepo.GetUsers().ForEach(async e =>
            {
                if (e.Stad == null || !e.Rol.Equals("in"))
                    return;
                string query = $"{e.Straatnaam} {e.Huisnummer} {e.Stad}";
                BingMapsRESTToolkit.Location address = await locationService.GetLocation(query);
                AddPushPin(new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)), address);

            });

            _cRepo.GetCustomers().ForEach(async e =>
            {
                if (e.Stad == null)
                    return;
                string query = $"{e.Straatnaam} {e.Huisnummer} {e.Stad}";
                BingMapsRESTToolkit.Location address = await locationService.GetLocation(query);
                AddPushPin(new SolidColorBrush(Color.FromArgb(100, 0, 255, 0)), address);

            });

            _iRepo.GetAllInspectieFormulieren().ForEach(async e =>
            {
                if (e.Stad == null)
                    return;
                string query = $"{e.Straatnaam} {e.Huisnummer} {e.Stad}";
                BingMapsRESTToolkit.Location address = await locationService.GetLocation(query);
                AddPushPin(new SolidColorBrush(Color.FromArgb(100, 0, 0, 255)), address);
            });

        }

        private void AddPushPin(SolidColorBrush color, BingMapsRESTToolkit.Location address)
        {
                Pushpin pushpin = new Pushpin();
                pushpin.Background = color;
                pushpin.Location = new Location(address.Point.Coordinates[0], address.Point.Coordinates[1]);
                PushpinList.Add(pushpin);
        }
    }
}
