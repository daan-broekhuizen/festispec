using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class JobInfoViewModel
    {


        public JobViewModel JobVM { get; set; }

        private NavigationService _navigationService;
        private JobRepository repo;
        public ICommand SaveJobCommand { get; set; }
        public JobInfoViewModel(NavigationService service, JobRepository repo)
        {
            SaveJobCommand = new RelayCommand(SaveJob);
            this.repo = repo;
            if (service.Parameter is JobViewModel)
                JobVM = service.Parameter as JobViewModel;
        }

        public void SaveJob()
        {
            repo.UpdateJob(new Opdracht()
            {
                OpdrachtNaam = JobVM.JobName,
                Status = JobVM.Status,
                KlantID = new CustomerRepository().GetCustomers().Where(e => e.Naam == JobVM.CustomerName).FirstOrDefault().KvKNummer,
                Klantwensen = JobVM.CustomerWishes,
                LaatsteWijziging = DateTime.Now,
                CreatieDatum = DateTime.Now,
                MedewerkerID = 2,
                OpdrachtID = JobVM.JobID
            });
        }
    }
}
