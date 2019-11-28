using Festispec.Model;
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

        public int QuotationId { get => _quotation.OfferteID; }
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
            get => Convert.ToString(_quotation.Totaalbedrag);
            set
            {
                Decimal price;
                if (Decimal.TryParse(value, out price))
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
                    return _quotation.Opdracht.Status;
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
                    case "ov":
                        return Colors.Yellow;
                    case "Opdracht geannuleerd":
                        return Colors.Black;
                    case "og":
                        return Colors.Red;
                    case "Nieuwe opdracht":
                        return Colors.Blue;
                    default:
                        return Colors.Green;
                }
            } 
        }
        public ImageSource Logo { get => ImageByteConverter.BytesToImage(_quotation.Opdracht.Klant.KlantLogo); }
        public QuotationViewModel(Offerte quotation)
        {
            _quotation = quotation;
            if (Status != "no" && Status != "Nieuwe opdracht")
                IsSent = true;
        }
        public QuotationViewModel()
        {
            _quotation = new Offerte();
        }
    }
}
