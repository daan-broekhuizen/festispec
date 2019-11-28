using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
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
        public QuotationViewModel QuotationVM { get; set; }

        private QuotationRepository _quotationRepository;

        public ShowQuotationViewModel(NavigationService service, QuotationRepository repo) : base(service)
        {
            _quotationRepository = repo;
            if (service.Parameter is QuotationViewModel)
                QuotationVM = service.Parameter as QuotationViewModel;

            AcceptQuotationCommand = new RelayCommand(AcceptQuotation, QuotationVM.Status == "Offerte verstuurd");
            CancelJobCommand = new RelayCommand(CancelJob, QuotationVM.Status == "Offerte verstuurd");
            RejectQuotationCommand = new RelayCommand(RejectQuotation, QuotationVM.Status == "Offerte verstuurd");
            DownloadQuotationCommand = new RelayCommand(DownloadQuotation);
            NewQuotationCommand = new RelayCommand(NewQuotation, QuotationVM.Status == "Offerte geweigerd");
            SaveQuotationCommand = new RelayCommand(SaveQuotation);
        }

        private void SaveQuotation()
        {
            if (QuotationVM.IsSent)
                _quotationRepository.UpdateJobStatus(QuotationVM.JobId, "Offerte verstuurt");
        }

        private void NewQuotation()
        {
            _navigationService.NavigateTo("AddQuotation", new QuotationViewModel(new Offerte() { Opdracht = _quotationRepository.GetJob(QuotationVM.JobId)}));
        }

        private void DownloadQuotation()
        {
            throw new NotImplementedException();
        }

        private void RejectQuotation()
        {
            throw new NotImplementedException();
        }

        private void CancelJob()
        {
            throw new NotImplementedException();
        }

        private void AcceptQuotation()
        {
            throw new NotImplementedException();
        }
    }
}
