using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class QuestionViewModel : ViewModelBase
    {
        private Question _question;

        public QuestionViewModel()
        {
            _question = new OpenQuestion();
        }

        public Question GetQuestion()
        {
            return _question;
        }

        public int Id
        {
            get { return _question.QuestionID; }
            set { _question.QuestionID = value; }
        }

        public String QuestionText
        {
            get { return _question.QuestionText;}
            set { _question.QuestionText = value; }
        }

        public String QuestionAnswer
        {
            get { return _question.Answer; }
            set { _question.Answer = value; }
        }


    }
}
