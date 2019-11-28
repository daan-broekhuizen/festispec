using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Validators;
using FluentValidation.Results;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.QuotationViewModels
{
    public class ShowQuotationViewModel : NavigatableViewModel
    {
        public ICommand AcceptQuotationCommand { get; set; }
        public ICommand RejectQuotationCommand { get; set; }
        public ICommand CancelJobCommand { get; set; }
        public ICommand DownloadQuotationCommand { get; set; }
        public ICommand NewQuotationCommand { get; set; }
        public ICommand SaveQuotationCommand { get; set; }

        private string _descriptionError;
        public string DescriptionError 
        {
            get => _descriptionError;
            set
            {
                _descriptionError = value;
                RaisePropertyChanged("DescriptionError");
            }
        }
        private string _priceError;
        public string PriceError 
        {
            get => _priceError;
            set
            {
                _priceError = value;
                RaisePropertyChanged("PriceError");
            }
        }
        private string _decisionError;
        public string DecisionError 
        {
            get => _decisionError;
            set
            {
                _decisionError = value;
                RaisePropertyChanged("DecisionError");
            }
        }
        public bool IsSendable { get => CanSave(); }
        public QuotationViewModel QuotationVM { get; set; }

        private QuotationRepository _quotationRepository;

        public ShowQuotationViewModel(NavigationService service, QuotationRepository repo) : base(service)
        {
            _quotationRepository = repo;
            if (service.Parameter is QuotationViewModel)
                QuotationVM = service.Parameter as QuotationViewModel;

            AcceptQuotationCommand = new RelayCommand(AcceptQuotation, CanRegisterDecision);
            CancelJobCommand = new RelayCommand(CancelJob, CanRegisterDecision);
            RejectQuotationCommand = new RelayCommand(RejectQuotation, CanRegisterDecision);
            DownloadQuotationCommand = new RelayCommand(DownloadQuotation);
            NewQuotationCommand = new RelayCommand(NewQuotation, CanCreate);
            SaveQuotationCommand = new RelayCommand(SaveQuotation, CanSave);
        }

        private bool CanCreate() => QuotationVM.Status == "og";
        private bool CanSave()
        {
            return QuotationVM.Status == "no";
        }
        private bool CanRegisterDecision() => QuotationVM.Status =="ov";
        private void SaveQuotation()
        {
            ValidationResult result = new QuotationValidator().Validate(QuotationVM);
            if(result.IsValid)
            {
                decimal price;
                Decimal.TryParse(QuotationVM.Price, out price);
                _quotationRepository.UpdateQuotation(new Offerte()
                {
                    OfferteID = QuotationVM.QuotationId,
                    OpdrachtID = QuotationVM.JobId,
                    Beschrijving = QuotationVM.Description,
                    Totaalbedrag = price,
                    Aanmaakdatum = QuotationVM.CreationDate,
                    LaatsteWijziging = DateTime.Now,
                });
                if (QuotationVM.IsSent)
                    _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Offerte verstuurt");
            } 
            else
            {
                ValidationFailure descriptionError = result.Errors.Where(e => e.PropertyName == "Description").FirstOrDefault();
                if (descriptionError != null)
                    DescriptionError = descriptionError.ToString();

                ValidationFailure priceError = result.Errors.Where(e => e.PropertyName == "Price").FirstOrDefault();
                if (priceError != null)
                    PriceError = priceError.ToString();
            }

        }
        private void NewQuotation()
        {
            _navigationService.NavigateTo("AddQuotation", new QuotationViewModel(
                new Offerte()
                {
                    Opdracht = _quotationRepository.GetJob(QuotationVM.JobId),
                    OpdrachtID = QuotationVM.JobId
                }));
        }
        private void DownloadQuotation()
        {
            throw new NotImplementedException();
        }
        private void RejectQuotation() => RegisterCustomerDecision("Offerte geweigerd");
        private void CancelJob() => RegisterCustomerDecision("Opdracht geannuleerd");
        private void AcceptQuotation()
        {
            if(QuotationVM.Decision != null)
                _quotationRepository.UpdateDecision(QuotationVM.QuotationId, QuotationVM.Decision);
            _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Offerte geaccepteerd");
            QuotationVM.Status = "og";

        }
        private void RegisterCustomerDecision(string status)
        {
            if (QuotationVM.Decision != null)
            {
                _quotationRepository.UpdateDecision(QuotationVM.QuotationId, QuotationVM.Decision);
                _quotationRepository.UpdateJobStatus(QuotationVM.JobId, status);
                QuotationVM.Status = new Status() { Betekenis = status }.Afkorting;
            }
            else
                DecisionError = "Voer een klantbeslissingsreden in.";
        }
    }
}
