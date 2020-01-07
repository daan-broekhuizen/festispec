using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Converters;
using Festispec.Utility.Validators;
using FluentValidation.Results;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

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
            Console.WriteLine(IsSendable);
            AcceptQuotationCommand = new RelayCommand(AcceptQuotation, CanRegisterDecision);
            CancelJobCommand = new RelayCommand(CancelJob, CanRegisterDecision);
            RejectQuotationCommand = new RelayCommand(RejectQuotation, CanRegisterDecision);
            DownloadQuotationCommand = new RelayCommand(DownloadQuotation);
            NewQuotationCommand = new RelayCommand(NewQuotation, CanCreate);
            SaveQuotationCommand = new RelayCommand(SaveQuotation, CanSave);
        }

        private bool CanCreate() => QuotationVM.Status == "Offerte geweigerd" && QuotationVM.IsLatestQuotation == true;
        private bool CanSave() => QuotationVM.Status == "Nieuwe opdracht";
        private bool CanRegisterDecision() => QuotationVM.Status == "Offerte verstuurt";
        private void SaveQuotation()
        {
            ValidationResult result = new QuotationValidator().Validate(QuotationVM);
            if(result.IsValid)
            {
                DescriptionError = "";
                PriceError = "";
                DecisionError = "";


                decimal price;
                Decimal.TryParse(QuotationVM.Price.Trim('€'), out price);
                _quotationRepository.UpdateQuotation(new Offerte()
                {
                    OfferteID = QuotationVM.QuotationId,
                    OpdrachtID = QuotationVM.JobId,
                    Beschrijving = QuotationVM.Description,
                    Totaalbedrag = price,
                    KlantbeslissingReden = QuotationVM.Decision,
                    Aanmaakdatum = QuotationVM.CreationDate,
                    LaatsteWijziging = DateTime.Now,
                }) ;
                if (QuotationVM.IsSent)
                {
                    _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Offerte verstuurt");
                    QuotationVM.Status = "Offerte verstuurt";
                }
                Messenger.Default.Send("Wijzigingen opgeslagen", this.GetHashCode());
                _navigationService.NavigateTo("ShowQuotation", QuotationVM);
            } 
            else
            {
                ValidationFailure descriptionError = result.Errors.Where(e => e.PropertyName == "Description").FirstOrDefault();
                if (descriptionError != null)
                    DescriptionError = descriptionError.ToString();
                else
                    DescriptionError = "";

                ValidationFailure priceError = result.Errors.Where(e => e.PropertyName == "Price").FirstOrDefault();
                if (priceError != null)
                    PriceError = priceError.ToString();
                else
                    PriceError = "";
            }

        }
        private void NewQuotation()
        {
            _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Nieuwe opdracht");
            _navigationService.NavigateTo("AddQuotation", new QuotationViewModel(
                new Offerte()
                {
                    Opdracht = _quotationRepository.GetJob(QuotationVM.JobId),
                    OpdrachtID = QuotationVM.JobId
                }, _quotationRepository));
        }
        private void DownloadQuotation()
        {
            string title = $"Offerte";
            new PDFConverter().Export(QuotationVM, title);
        }

        private void RejectQuotation() => RegisterCustomerDecision("Offerte geweigerd");
        private void CancelJob() => RegisterCustomerDecision("Opdracht geannuleerd");
        private void AcceptQuotation()
        {
            if(QuotationVM.Decision != null)
                _quotationRepository.UpdateDecision(QuotationVM.QuotationId, QuotationVM.Decision);
            _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Offerte geaccepteerd");
            QuotationVM.Status = "Offerte geaccepteerd";
            DecisionError = "";

        }
        private void RegisterCustomerDecision(string status)
        {
            if (QuotationVM.Decision != null)
            {
                DecisionError = "";
                _quotationRepository.UpdateDecision(QuotationVM.QuotationId, QuotationVM.Decision);
                _quotationRepository.UpdateJobStatus(QuotationVM.JobId, status);
                QuotationVM.Status = status;
            }
            else
                DecisionError = "Voer een klantbeslissingsreden in.";
        }
    }
}
