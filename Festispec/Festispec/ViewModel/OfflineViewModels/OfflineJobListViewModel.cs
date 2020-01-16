using Festispec.Model.Repositories;
using Festispec.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.OfflineViewModels
{
    public class OfflineJobListViewModel : ViewModelBase
    {
        private List<OfflineJobViewModel> _jobs;

        public List<OfflineJobViewModel> Jobs
        {
            get => _jobs;
            set
            {
                _jobs = value;
                RaisePropertyChanged("Jobs");
            }
        }

        private List<OfflineJobViewModel> _filteredJobs;

        public List<OfflineJobViewModel> FilteredJobs
        {
            get => _filteredJobs;
            set
            {
                _filteredJobs = value;
                RaisePropertyChanged("FilteredJobs");
            }
        }

        private OfflineJobViewModel _selectedJob;

        public OfflineJobViewModel SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                RaisePropertyChanged("SelectedJob");
                ShowOfflineJobInfo();
            }
        }

        private ComboBoxItem _selectedBox;
        public ComboBoxItem SelectedBox
        {
            get
            {
                return _selectedBox;
            }
            set
            {
                _selectedBox = value;
                SortOfflineJobs();
            }

        }

        public string FilterJob { get; set; }
        
        private OfflineJobRepository _offlineJobRepo { get; set; }
        public ICommand SearchJob { get; set; }
        private NavigationService _navigationService;
        public OfflineJobListViewModel(NavigationService service)
        {
            _navigationService = service;
            _offlineJobRepo = new OfflineJobRepository();
            Jobs = _offlineJobRepo
                    .GetOfflineOpdrachten(Directory.GetParent(Directory
                    .GetCurrentDirectory()).Parent.FullName + 
                    "/bin/opdrachten.json")
                    .Select(c => new OfflineJobViewModel(c)).ToList();
            FilteredJobs = Jobs;
            SearchJob = new RelayCommand(FilterOfflineJobs);
            FilterJob = "";
        }

        public void ShowOfflineJobInfo()
        {
            if (SelectedJob != null)
                _navigationService.OfflineNavigation("OfflineJob", SelectedJob);
        }

        public void FilterOfflineJobs()
        {
            FilteredJobs = Jobs.Where(e => e.OpdrachtNaam.Contains(FilterJob)).ToList();
        }

        public void SortOfflineJobs()
        {
            if (SelectedBox != null)
            {
                switch (SelectedBox.Content)
                {
                    case "Naam oplopend":
                        FilteredJobs = FilteredJobs.OrderBy(e => e.OpdrachtNaam).ToList();
                        break;
                    case "Naam aflopend":
                        FilteredJobs = FilteredJobs.OrderByDescending(e => e.OpdrachtNaam).ToList();
                        break;
                    case "Status oplopend":
                        FilteredJobs = FilteredJobs.OrderBy(e => e.Status).ToList();
                        break;
                    case "Status aflopend":
                        FilteredJobs = FilteredJobs.OrderByDescending(e => e.Status).ToList();
                        break;
                }
            }
        }
    }
}
