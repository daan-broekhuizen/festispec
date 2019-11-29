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

        public string FilterJob { get; set; }

        private JobRepository JobRepository { get; set; }

        public ICommand SearchJob { get; set; }
        public ICommand ShowAddJobCommand { get; set; }
        private NavigationService _navigationService;
        public JobListViewModel(NavigationService service)
        {
            _navigationService = service;
            JobRepository = new JobRepository();
            Jobs = JobRepository.GetOpdrachten().Select(c => new JobViewModel(c)).ToList();
            FilteredJobs = Jobs;
            SearchJob = new RelayCommand(FilterJobs);
            ShowAddJobCommand = new RelayCommand(ShowAddJob);
            FilterJob = "";
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

        public void FilterJobs()
        {
            FilteredJobs = Jobs.Where(e => e.JobName.Contains(FilterJob)).ToList();
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
                }
            }
        }
    }
}
