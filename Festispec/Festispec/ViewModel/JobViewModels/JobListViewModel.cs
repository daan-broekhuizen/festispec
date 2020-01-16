using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class JobListViewModel : ViewModelBase
    {
        private List<JobViewModel> jobs;

        public List<JobViewModel> Jobs
        {
            get => jobs;
            set
            {
                jobs = value;
                RaisePropertyChanged("Jobs");
            }
        }

        private List<JobViewModel> filteredJobs;

        public List<JobViewModel> FilteredJobs
        {
            get => filteredJobs;
            set
            {
                filteredJobs = value;
                RaisePropertyChanged("FilteredJobs");
            }
        }

        private JobViewModel _selectedJob;

        public JobViewModel SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                RaisePropertyChanged("SelectedJob");
                ShowJobInfo();
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
                SortJobs();
            }

        }
        private JobRepository _jobRepository { get; set; }
        public ICommand SearchJob { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        private NavigationService _navigationService;
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }
        public JobListViewModel(NavigationService service)
        {
            _navigationService = service;
            _jobRepository = new JobRepository();
            Jobs = _jobRepository.GetOpdrachten().Select(c => new JobViewModel(c)).ToList();
            FilteredJobs = Jobs;
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            SearchButtonClickCommand = new RelayCommand<string>(FilterJobs);
            SearchTextChangedCommand = new RelayCommand<string>(FilterJobs);
        }

        public void ShowAddJob()
        {
            _navigationService.NavigateTo("AddJob");
        }

        public void ShowJobInfo()
        {
            if (SelectedJob != null)
                _navigationService.NavigateTo("JobInfo", SelectedJob);
        }

        public void FilterJobs(string searchText)
        {
            FilteredJobs = Jobs.Where(e => e.JobName.ToLower().Contains(searchText)).ToList();
            SortJobs();
        }

        public void SortJobs()
        {
            if (SelectedBox != null)
            {
                switch(SelectedBox.Content)
                {
                    case "Naam oplopend":
                        FilteredJobs = FilteredJobs.OrderBy(e => e.JobName).ToList();
                        break;
                    case "Naam aflopend":
                        FilteredJobs = FilteredJobs.OrderByDescending(e => e.JobName).ToList();
                        break;
                    case "Aanmaak datum oplopend":
                        FilteredJobs = FilteredJobs.OrderBy(e => e.CreationDate).ToList();
                        break;
                    case "Aanmaak datum aflopend":
                        FilteredJobs = FilteredJobs.OrderByDescending(e => e.CreationDate).ToList();
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
