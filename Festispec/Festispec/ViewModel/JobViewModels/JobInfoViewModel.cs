using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Model.DTO;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Validators;
using Festispec.ViewModel.QuotationViewModels;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;

namespace Festispec.ViewModel
{
    public class JobInfoViewModel : ViewModelBase
    {
        public JobViewModel JobVM { get; set; }

        private NavigationService _navigationService;
        private JobRepository _jobRepo;
        private QuotationRepository _quotationRepo;
        public ICommand SaveJobCommand { get; set; }
        public ICommand ShowQuotationCommand { get; set; }
        public ICommand ShowInspectionFormsCommand { get; set; }
        public ICommand ShowRapportageCommand { get; set; }
        public ICommand SaveJobOfflineCommand { get; set; }
        public DateTime MinimumDate
        {
            get => DateTime.Today;
            set { return; }
        }

        public List<string> Status { get; set; }

        #region ErrorProperties
        private string _jobNameError;
        public string JobNameError
        {
            get => _jobNameError;
            set
            {
                _jobNameError = value;
                RaisePropertyChanged("JobNameError");
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

        private string _customerWishesError;
        public string CustomerWishesError
        {
            get => _customerWishesError;
            set
            {
                _customerWishesError = value;
                RaisePropertyChanged("CustomerWishesError");
            }
        }
        #endregion

        public JobInfoViewModel(NavigationService service, JobRepository Jrepo, StatusRepository Srepo, QuotationRepository quotationRepo)
        {
            SaveJobCommand = new RelayCommand(CanSaveJob);
            ShowQuotationCommand = new RelayCommand(ShowQuotation);
            ShowInspectionFormsCommand = new RelayCommand(ShowInspectionForms);
            ShowRapportageCommand = new RelayCommand(ShowRapportage);
            SaveJobOfflineCommand = new RelayCommand(SaveJobOffline);
            _navigationService = service;
            _quotationRepo = quotationRepo;
            _jobRepo = Jrepo;

            if (service.Parameter is JobViewModel)
                JobVM = service.Parameter as JobViewModel;

            Status = new List<string>();
            Srepo.GetAllStatus().ForEach(e => Status.Add(e.Betekenis));
        }

        private void ShowQuotation()
        {
            if (JobVM == null)
                return;

            Offerte latest = _quotationRepo.GetQuotations()
                .Where(q => q.OpdrachtID == JobVM.JobID)
                .OrderByDescending(q => q.OfferteID).FirstOrDefault();

            if (latest != null)
                _navigationService.NavigateTo("ShowQuotation", new QuotationViewModel(latest, _quotationRepo));
            else
            {
                _navigationService.NavigateTo("AddQuotation", new QuotationViewModel(
                new Offerte()
                {
                    Opdracht = _quotationRepo.GetJob(JobVM.JobID),
                    OpdrachtID = JobVM.JobID
                }, _quotationRepo));
            }
        }

        private void ShowRapportage()
        {
            if (JobVM != null)
            {
                JobViewModel updateViewModel = new JobViewModel(_quotationRepo.GetJob(JobVM.JobID));

                if (updateViewModel != null && !string.IsNullOrEmpty(updateViewModel.Report))
                    _navigationService.NavigateTo("Rapportage", new object[2] { EnumTemplateMode.SELECT, updateViewModel });
                else if (updateViewModel != null)
                    _navigationService.NavigateTo("RapportageTemplateOverview", new object[2] { EnumTemplateMode.SELECT,  updateViewModel });
            }
        }

        public void SaveJob()
        {
            _jobRepo.UpdateJob(new Opdracht()
            {
                OpdrachtNaam = JobVM.JobName,
                Status = JobVM.Status,
                KlantID = JobVM.CustomerID,
                Klantwensen = JobVM.CustomerWishes,
                LaatsteWijziging = DateTime.Now,
                MedewerkerID = 2,
                OpdrachtID = JobVM.JobID,
                StartDatum = JobVM.StartDatum,
                EindDatum = JobVM.EindDatum
            });
            Messenger.Default.Send("Wijzigingen opgeslagen", this.GetHashCode());
        }

        public void ShowInspectionForms()
        {
            _navigationService.NavigateTo("InspectionFormShowView", JobVM.JobID);
        }

        private void CanSaveJob()
        {
            List<ValidationFailure> errors = new JobValidator().Validate(JobVM).Errors.ToList();
            ValidationFailure jobNameError = errors.Where(e => e.PropertyName.Equals("JobName")).FirstOrDefault();
            ValidationFailure beginDateError = errors.Where(e => e.PropertyName.Equals("StartDatum")).FirstOrDefault();
            ValidationFailure endDateError = errors.Where(e => e.PropertyName.Equals("EindDatum")).FirstOrDefault();
            ValidationFailure statusError = errors.Where(e => e.PropertyName.Equals("Status")).FirstOrDefault();
            ValidationFailure customerWishesError = errors.Where(e => e.PropertyName.Equals("CustomerWishes")).FirstOrDefault();


            if (jobNameError == null && beginDateError == null && endDateError == null && statusError == null && customerWishesError == null)
            {
                SaveJob();
                _navigationService.NavigateTo("Jobs");
                return;
            }

            if (jobNameError != null)
                JobNameError = jobNameError.ErrorMessage;
            else
                JobNameError = "";

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

            if (customerWishesError != null)
                CustomerWishesError = customerWishesError.ErrorMessage;
            else
                CustomerWishesError = "";
        }

        private void SaveJobOffline()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/bin/opdrachten.json";
            List<JsonJob> jsonArray = new List<JsonJob>();
            JsonJob jobToSave = (JsonJob)_jobRepo.GetSingleJob(JobVM.JobID);
            if (!File.Exists(path))
            {
                jsonArray.Add(jobToSave);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(jsonArray));
                }
            }
            else
            {
                string newJson;
                using (StreamReader sr = new StreamReader(path))
                {
                    string json = sr.ReadToEnd();
                    List<JsonJob> jsonJobs = JsonConvert.DeserializeObject<List<JsonJob>>(json);
                    if(!jsonJobs.Contains(jobToSave))
                        jsonJobs.Add(jobToSave);
                    newJson = JsonConvert.SerializeObject(jsonJobs);
                }
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine(newJson);
                }
            }
        }
    }
}