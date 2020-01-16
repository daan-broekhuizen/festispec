using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Model.Repositories;
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
        private CustomerRepository _customerRepo;
        public JobViewModel JobVM { get; set; }
        public ICommand SaveJobCommand { get; set; }
        public Dictionary<int, string> Customers { get; private set; }
        public ICommand PreviousPageCommand { get; set; }
        public List<string> Status { get; set; }
        public DateTime MinimalDate
        {
            get => DateTime.Today;
            set
            { return; }
        }

        #region ErrorProperties
        private string _jobnameError;
        public string JobNameError
        {
            get => _jobnameError;
            set
            {
                _jobnameError = value;
                RaisePropertyChanged("JobNameError");
            }
        }

        private string _customernameError;
        public string CustomerNameError
        {
            get => _customernameError;
            set
            {
                _customernameError = value;
                RaisePropertyChanged("CustomerNameError");
            }
        }

        private string _statusError;
        public string StatusError
        {
            get => _statusError;
            set
            {
                _statusError = value;
                RaisePropertyChanged("StatusError");
            }
        }

        private string _beginDateError;
        public string BeginDateError
        {
            get => _beginDateError;
            set
            {
                _beginDateError = value;
                RaisePropertyChanged("BeginDateError");
            }
        }

        private string _endDateError;
        public string EndDateError
        {
            get => _endDateError;
            set
            {
                _endDateError = value;
                RaisePropertyChanged("EndDateError");
            }
        }

        private string _customerwishesError;
        public string CustomerWishesError
        {
            get => _customerwishesError;
            set
            {
                _customerwishesError = value;
                RaisePropertyChanged("CustomerWishesError");
            }
        }
        #endregion
        public AddJobViewModel(NavigationService service, JobRepository repo, CustomerRepository cRepo, StatusRepository sRepo)
        {
            _navigationService = service;
            _jobRepo = repo;
            _customerRepo = cRepo;

            if (service.Parameter is JobViewModel)
                JobVM = service.Parameter as JobViewModel;
            else
                JobVM = new JobViewModel();

            //Set customers
            Customers = new Dictionary<int, string>();
            _customerRepo.GetCustomers().ForEach(c => Customers.Add(c.KlantID, c.Naam));

            //Set statusses
            Status = new List<string>();
            sRepo.GetAllStatus().ForEach(e => Status.Add(e.Betekenis));
            JobVM.Status = Status.FirstOrDefault(s => s.Equals("Nieuwe opdracht"));

            JobVM.StartDatum = DateTime.Today;
            JobVM.EindDatum = DateTime.Today;
            SaveJobCommand = new RelayCommand(CanSaveJob);
            PreviousPageCommand = new RelayCommand(PreviousPage);
        }

        private void PreviousPage()
        {
            _navigationService.GoBack();
        }

        private void SaveJob()
        {
            Opdracht opdracht = new Opdracht()
            {
                OpdrachtNaam = JobVM.JobName,
                Status = JobVM.Status,
                KlantID = JobVM.CustomerID,
                Klantwensen = JobVM.CustomerWishes,
                LaatsteWijziging = DateTime.Now,
                CreatieDatum = DateTime.Now,
                MedewerkerID = _navigationService.Account.Id,
                StartDatum = JobVM.StartDatum,
                EindDatum = JobVM.EindDatum

            };
            _jobRepo.CreateJob(opdracht);
            
        }

        private void CanSaveJob()
        {
            List<ValidationFailure> errors = new JobValidator().Validate(JobVM).Errors.ToList();
            ValidationFailure jobnNameError = errors.Where(e => e.PropertyName.Equals("JobName")).FirstOrDefault();
            ValidationFailure customerNameError = errors.Where(e => e.PropertyName.Equals("CustomerID")).FirstOrDefault();
            ValidationFailure beginDateError = errors.Where(e => e.PropertyName.Equals("StartDatum")).FirstOrDefault();
            ValidationFailure endDateError = errors.Where(e => e.PropertyName.Equals("EindDatum")).FirstOrDefault();
            ValidationFailure statusError = errors.Where(e => e.PropertyName.Equals("Status")).FirstOrDefault();
            ValidationFailure customerWishesError = errors.Where(e => e.PropertyName.Equals("CustomerWishes")).FirstOrDefault();

            if (jobnNameError == null && customerNameError == null && beginDateError == null && endDateError == null && statusError == null && customerWishesError == null)
            {
                SaveJob();
                _navigationService.NavigateTo("Jobs");
                return;
            }

            if (jobnNameError != null)
                JobNameError = jobnNameError.ErrorMessage;
            else
                JobNameError = "";

            if (customerNameError != null)
                CustomerNameError = customerNameError.ErrorMessage;
            else
                CustomerNameError = "";

            if (beginDateError != null)
                BeginDateError = beginDateError.ErrorMessage;
            else
                BeginDateError = "";

            if (endDateError != null)
                EndDateError = endDateError.ErrorMessage;
            else
                EndDateError = "";

            if (statusError != null)
                StatusError = statusError.ErrorMessage;
            else
                StatusError = "";

            if(customerWishesError != null)
                CustomerWishesError = customerWishesError.ErrorMessage;
            else
                CustomerWishesError = "";

        }
    }
}
