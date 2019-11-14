using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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
        public JobListViewModel()
        {
            JobRepository = new JobRepository();
            Jobs = JobRepository.GetOpdrachten().Select(c => new JobViewModel(c)).ToList();
            FilteredJobs = Jobs;
            SearchJob = new RelayCommand(FilterJobs);
            FilterJob = "";
        }

        public void FilterJobs()
        {
            if (SelectedBox != null)
            {
                switch (SelectedBox.Content)
                {
                    case "A - Z":
                        FilteredJobs = GetFilteredOpdrachtenASC();
                        break;
                    case "Z - A":
                        FilteredJobs = GetFilteredOpdrachtenDESC();
                        break;
                }
            }
            else
            {
                FilteredJobs = GetFilteredOpdrachten();

            }
        }

        public void SortJobs()
        {
            if (FilterJob != null)
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            FilteredJobs = GetFilteredOpdrachtenASC();
                            break;
                        case "Z - A":
                            FilteredJobs = GetFilteredOpdrachtenDESC();
                            break;
                    }
                }
                else
                {
                    FilteredJobs = GetFilteredOpdrachten();


                }
            }
            else
            {
                if (SelectedBox != null)
                {
                    switch (SelectedBox.Content)
                    {
                        case "A - Z":
                            FilteredJobs = GetOpdrachtenASC();
                            break;
                        case "Z - A":
                            FilteredJobs = GetOpdrachtenDESC();
                            break;
                    }
                }
                else
                {
                    FilteredJobs = Jobs;

                }
            }
        }

        public List<JobViewModel> GetFilteredOpdrachten()
        {
            FilteredJobs = Jobs.Where(e => e.JobName.Contains(FilterJob)).ToList();
            return FilteredJobs;
        }
        public List<JobViewModel> GetFilteredOpdrachtenASC()
        {
            FilteredJobs = Jobs.Where(e => e.JobName.Contains(FilterJob)).OrderBy(e => e.JobName).ToList();
            return FilteredJobs;
        }
        public List<JobViewModel> GetFilteredOpdrachtenDESC()
        {
            FilteredJobs = Jobs.Where(e => e.JobName.Contains(FilterJob)).OrderByDescending(e => e.JobName).ToList();
            return FilteredJobs;
        }
        public List<JobViewModel> GetOpdrachtenASC()
        {
            FilteredJobs = Jobs.OrderBy(e => e.JobName).ToList();
            return FilteredJobs;
        }
        public List<JobViewModel> GetOpdrachtenDESC()
        {
            FilteredJobs = Jobs.OrderByDescending(e => e.JobName).ToList();
            return FilteredJobs;
        }
    }
}
