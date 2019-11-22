using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Validators;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class AddJobViewModel : ViewModelBase
    {
        private NavigationService _navigationService;
        private JobRepository _jobRepo;

        public JobViewModel JobVM { get; set; }
        public ICommand SaveJobCommand { get; set; }

        private string _nameError;
        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                RaisePropertyChanged("NameError");
            }
        }

        public AddJobViewModel(NavigationService service, JobRepository repo)
        {
            _navigationService = service;
            _jobRepo = repo;

            if (service.Parameter is JobViewModel)
                JobVM = service.Parameter as JobViewModel;
            else
                JobVM = new JobViewModel();

            SaveJobCommand = new RelayCommand(CanSaveJob);
        }

        private void SaveJob()
        {
            string name = JobVM.CustomerName;
            Opdracht opdracht = new Opdracht()
            {
                OpdrachtNaam = JobVM.JobName,
                Status = JobVM.Status,
                KlantID = new CustomerRepository().GetCustomers().Where(e => e.Naam == JobVM.CustomerName).FirstOrDefault().KvKNummer,
                Klantwensen = JobVM.CustomerWishes,
                LaatsteWijziging = DateTime.Now,
                CreatieDatum = DateTime.Now,
                MedewerkerID = 2

            };
            _jobRepo.CreateJob(opdracht);
            
        }

        private void CanSaveJob()
        {
            List<ValidationFailure> errors = new JobValidator().Validate(JobVM).Errors.ToList();
            ValidationFailure nameError = errors.Where(e => e.PropertyName.Equals("JobName")).FirstOrDefault();

            if(nameError != null)
            {
                NameError = nameError.ErrorMessage;
            }
        }
    }
}
