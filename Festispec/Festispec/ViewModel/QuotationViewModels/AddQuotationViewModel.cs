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
    public class AddQuotationViewModel : NavigatableViewModel
    {
        public QuotationViewModel QuotationVM { get; set; }
        public ICommand SaveQuotationCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }

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

        private QuotationRepository _quotationRepository;

        public AddQuotationViewModel(NavigationService service, QuotationRepository repo) : base(service)
        {
            _quotationRepository = repo;
            if (service.Parameter is QuotationViewModel)
                QuotationVM = service.Parameter as QuotationViewModel;
            else
                QuotationVM = new QuotationViewModel();

            SaveQuotationCommand = new RelayCommand(SaveQuotation);
            PreviousPageCommand = new RelayCommand(PreviousPage);
        }

        private void PreviousPage()
        {
            Opdracht job = _quotationRepository.GetJob(QuotationVM.JobId);
            _navigationService.NavigateTo("JobInfo", new JobViewModel(job));
        }

        private void SaveQuotation()
        {
            if (QuotationVM.JobId < 1)
                return;

            ValidationResult result = new QuotationValidator().Validate(QuotationVM);
            if (result.IsValid)
            {
                decimal price;
                decimal.TryParse(QuotationVM.Price.Trim('€'), out price);
                Offerte quotation = new Offerte()
                {
                    OpdrachtID = QuotationVM.JobId,
                    Beschrijving = QuotationVM.Description,
                    Totaalbedrag = price,
                    Aanmaakdatum = DateTime.Now,
                    LaatsteWijziging = DateTime.Now
                };

                _quotationRepository.CreateQuotation(quotation);
                QuotationVM.Status = "Nieuwe opdracht";
                QuotationVM.IsSent = false;
                QuotationVM.IsLatestQuotation = true;
                QuotationVM.QuotationId = _quotationRepository.GetQuotations().Max(q => q.OfferteID);
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
                    PriceError = null;
            }

        }
    }
}
