using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Converters;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Festispec.ViewModel.QuotationViewModels
{
    public class QuotationViewModel : ViewModelBase
    {
        private Offerte _quotation;
        public int QuotationId
        { 
            get => _quotation.OfferteID;
            set => _quotation.OfferteID = value;
        }
        public string Job { get => _quotation.Opdracht.OpdrachtNaam; }
        public DateTime CreationDate { get => _quotation.Aanmaakdatum; }
        public DateTime LastEdit { get => _quotation.LaatsteWijziging; }
        public int JobId
        {
            get => _quotation.OpdrachtID;
            set
            {
                _quotation.OpdrachtID = value;
                RaisePropertyChanged("JobId");
            }
        }
        public string Description
        {
            get => _quotation.Beschrijving;
            set
            {
                _quotation.Beschrijving = value;
                RaisePropertyChanged("Description");
            }
        }
        public string Decision
        {
            get => _quotation.KlantbeslissingReden;
            set
            {
                _quotation.KlantbeslissingReden = value;
                RaisePropertyChanged("Decision");
            }
        }
        public string Price
        {
            get => "€" + Convert.ToString(_quotation.Totaalbedrag);
            set
            {
                Decimal price;
                if (Decimal.TryParse(value.Trim('€'), out price))
                {
                    _quotation.Totaalbedrag = price;
                    RaisePropertyChanged("Price");
                }
            }
        }
        public string Status
        {
            get
            {
                if (_quotation.Opdracht.Status != null)
                {
                    if (IsLatestQuotation == false)
                        return "Offerte geweigerd";
                    return _quotation.Opdracht.Status;
                }
                else
                    return "Nieuwe opdracht";
            }
            set
            {
                _quotation.Opdracht.Status = value;
                RaisePropertyChanged("Status");
                RaisePropertyChanged("ColorCode");
            }
        }
        public bool IsLatestQuotation { get; set; }

        private bool _isSent = false;
        public bool IsSent
        {
            get => _isSent;
            set
            {
                _isSent = value;
                RaisePropertyChanged("IsSent");
            }
        }
        public Color ColorCode 
        {
            get 
            {
                switch (Status)
                {
                    case "Offerte verstuurt":
                        return Colors.Yellow;
                    case "Opdracht geannuleerd":
                        return Colors.Black;
                    case "Offerte geweigerd":
                        return Colors.Red;
                    case "Nieuwe opdracht":
                        return Colors.Blue;
                    default:
                        return Colors.Green;
                }
            } 
        }
        public ImageSource Logo { get => ImageByteConverter.BytesToImage(_quotation.Opdracht.Klant.KlantLogo); }
        public QuotationViewModel(Offerte quotation, QuotationRepository repo)
        {
            _quotation = quotation;
            Offerte latest =  repo.GetQuotations().Where(q => q.OpdrachtID == quotation.OpdrachtID).OrderByDescending(q => q.OfferteID).FirstOrDefault();
            if (latest != null && !latest.OfferteID.Equals(quotation.OfferteID))
                IsLatestQuotation = false;
            else
                IsLatestQuotation = true;
            if (Status != "Nieuwe opdracht")
                IsSent = true;
        }
        public QuotationViewModel()
        {
            _quotation = new Offerte();
        }
    }
}
