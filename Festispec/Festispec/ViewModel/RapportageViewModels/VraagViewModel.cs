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

        public ICollection<Antwoorden> Answers { get; set; }

        public VraagViewModel() { }
        
        public VraagViewModel(Vraag question)
        {
            Question = question.Vraagstelling;
            Type = question.Vraagtype;
            Answers = question.Antwoorden;
        }
    }
}
