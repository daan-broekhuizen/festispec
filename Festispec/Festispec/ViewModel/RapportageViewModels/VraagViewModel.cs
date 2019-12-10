using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.RapportageViewModels
{
    public class VraagViewModel : ViewModelBase
    {
        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }

        private string _question;
        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                RaisePropertyChanged("Question");
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }

        private string _typeDescription;
        public string TypeDescription
        {
            get => _typeDescription;
            set
            {
                _typeDescription = value;
                RaisePropertyChanged("TypeDescription");
            }
        }

        public ICollection<Antwoorden> Answers { get; set; }

        public VraagViewModel() { }
        
        public VraagViewModel(Vraag question)
        {
            ID = question.VraagID;
            Question = question.Vraagstelling;
            Type = question.Vraagtype;
            Answers = question.Antwoorden;
        }
    }
}
