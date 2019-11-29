using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class QuestionViewModel : ViewModelBase
    {
        private Vraag _question;
        private int _CurrentInspectionFormID;
        private List<InspectieformulierVragenlijstCombinatie> _VIC;

        public QuestionViewModel(Vraag v, int curInspecID)
        {
            _question = v;
            _CurrentInspectionFormID = curInspecID;
            _VIC = new List<InspectieformulierVragenlijstCombinatie>();
            foreach (var vic in _question.InspectieformulierVragenlijstCombinatie)
            {
                _VIC.Add(vic);
            }
        }

        public int QuestionID
        {
            get => _question.VraagID;
            set
            {
                _question.VraagID = value;
                RaisePropertyChanged("QuestionID");
            }
        }

        public Vraag Question
        {
            get => _question;
        }

        public string QuestionText
        {
            get => _question.Vraagstelling;
            set
            {
                _question.Vraagstelling = value;
                RaisePropertyChanged("QuestionText");
            }
        }

        public string QuestionType
        {
            get => _question.Vraagtype;
            set
            {
                _question.Vraagtype= value;
                RaisePropertyChanged("QuestionType");
            }
        }

        public byte[] Image
        {
            get => _question.Bijlage;
            set
            {
                _question.Bijlage = value;
                RaisePropertyChanged("Image");
            }
        }

        public DateTime LastChange
        {
            get => _question.LaatsteWijziging;
            set
            {
                _question.LaatsteWijziging = value;
                RaisePropertyChanged("LastChange");
            }
        }

        public int OrderNumber
        {
            get => VIC.Where(i => i.InspectieformulierID == _CurrentInspectionFormID).FirstOrDefault().VraagVolgordeNummer;
            set
            {
                VIC.Where(i => i.InspectieformulierID == _CurrentInspectionFormID).FirstOrDefault().VraagVolgordeNummer = value;
                RaisePropertyChanged("OrderNumber");
            }
        }

        public List<InspectieformulierVragenlijstCombinatie> VIC
        {
            get => _VIC;
            set
            {
                _VIC = value;
                RaisePropertyChanged("VIC");
            }
        }
    }
}
