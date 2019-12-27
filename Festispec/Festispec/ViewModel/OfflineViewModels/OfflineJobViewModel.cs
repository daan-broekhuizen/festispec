using Festispec.Model.DTO;
using Festispec.Utility.Converters;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Festispec.ViewModel.OfflineViewModels
{
    public class OfflineJobViewModel : ViewModelBase
    {
        private JsonJob _jsonJob;

        public OfflineJobViewModel(JsonJob job)
        {
            _jsonJob = job;
        }

        public int OpdrachtID
        {
            get => _jsonJob.ID;
            set
            {
                _jsonJob.ID = value;
                RaisePropertyChanged("OpdrachtID");
            }
        }
        public string OpdrachtNaam
        {
            get => _jsonJob.OpdrachtNaam;
            set
            {
                _jsonJob.OpdrachtNaam = value;
                RaisePropertyChanged("OpdrachtNaam");
            }
        }

        public string Status
        {
            get => _jsonJob.Status;
            set
            {
                _jsonJob.Status = value;
                RaisePropertyChanged("Status");
            }
        }
        public DateTime StartDatum
        {
            get => _jsonJob.StartDatum;
            set
            {
                _jsonJob.StartDatum = value;
                RaisePropertyChanged("StartDatum");
            }
        }

        public DateTime EindDatum
        {
            get => _jsonJob.EindDatum;
            set
            {
                _jsonJob.EindDatum = value;
                RaisePropertyChanged("EindDatum");
            }
        }

        public string KlantNaam
        {
            get => _jsonJob.KlantNaam;
            set
            {
                _jsonJob.KlantNaam = value;
                RaisePropertyChanged("KlantNaam");
            }
        }

        public string KlantWensen
        {
            get => _jsonJob.KlantWensen;
            set
            {
                _jsonJob.KlantWensen = value;
                RaisePropertyChanged("KlantWensen");
            }
        }

        public ImageSource Logo
        {
            get => ImageByteConverter.BytesToImage(_jsonJob.Logo);
            set
            {
                _jsonJob.Logo = ImageByteConverter.PngImageToBytes(value);
                RaisePropertyChanged("Logo");
            }
        }
    }
}
