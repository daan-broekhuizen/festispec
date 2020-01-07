using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class PossibleAnwserViewModel : ViewModelBase
    {
        private VraagMogelijkAntwoord _possibleAnwser;

        public PossibleAnwserViewModel(VraagMogelijkAntwoord vma)
        {
            _possibleAnwser = vma;
        }

        public VraagMogelijkAntwoord PossibleAnwser
        {
            get => _possibleAnwser;
            set
            {
                _possibleAnwser = value;
                RaisePropertyChanged("PossibleAnwser");
            }
        }

        public String AnwserTextString
        {
            get => _possibleAnwser.AntwoordText;
            set
            {
                _possibleAnwser.AntwoordText = value;
                RaisePropertyChanged("AnwserTextString");
            }
        }

        public int AnwserTextInt
        {
            get => Int32.Parse(_possibleAnwser.AntwoordText);
            set
            {
                _possibleAnwser.AntwoordText = value.ToString();
                RaisePropertyChanged("AnwserTextInt");
            }
        }

        public int AnwserNumber
        {
            get => _possibleAnwser.AntwoordNummer;
            set
            {
                _possibleAnwser.AntwoordNummer = value;
                RaisePropertyChanged("AnwserNumber");
            }
        }

        public int QuestionNumber
        {
            get => _possibleAnwser.VraagID;
            set
            {
                _possibleAnwser.VraagID = value;
                RaisePropertyChanged("QuestionNumber");
            }
        }
    }
}
