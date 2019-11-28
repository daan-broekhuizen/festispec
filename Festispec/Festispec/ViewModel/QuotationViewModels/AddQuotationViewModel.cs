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
        }

        private void SaveQuotation()
        {
            if (QuotationVM.JobId == 0) return;
            ValidationResult result = new QuotationValidator().Validate(QuotationVM);
            if (result.IsValid)
            {
                decimal price;
                Decimal.TryParse(QuotationVM.Price, out price);
                _quotationRepository.CreateQuotation(new Offerte()
                {
                    OpdrachtID = QuotationVM.JobId,
                    Beschrijving = QuotationVM.Description,
                    Totaalbedrag = price,
                    Aanmaakdatum = DateTime.Now,
                    LaatsteWijziging = DateTime.Now
                });
            }
            else
            {
                DescriptionError = result.Errors.Where(e => e.PropertyName == "Description").FirstOrDefault().ToString();
                PriceError = result.Errors.Where(e => e.PropertyName == "Price").FirstOrDefault().ToString();
            }

        }
    }
}
