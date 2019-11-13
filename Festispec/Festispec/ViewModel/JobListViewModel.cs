using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;

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

        private JobRepository JobRepository { get; set; }
        public JobListViewModel()
        {
            JobRepository = new JobRepository();
            Jobs = JobRepository.GetOpdrachten().Select(c => new JobViewModel(c)).ToList();
        }
    }
}
