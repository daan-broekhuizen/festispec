using Festispec.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class MultipleChoiceQuestionViewModel
    {
        private MultipleChoiceQuestion _question;

        public MultipleChoiceQuestionViewModel()
        {
            _question = new MultipleChoiceQuestion();
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
            get { return _question.QuestionText; }
            set { _question.QuestionText = value; }
        }

        public int QuestionAnswer
        {
            get { return _question.Answer; }
            set { _question.Answer = value; }
        }

        public int getChoicesCount
        {
            get { return _question.PossibleAnswers.Count(); }
        }

        public void AddPosAnswer(String posAnswer)
        {
            _question.PossibleAnswers.Add(posAnswer);
        }

        public ObservableCollection<Char> GetCharCollection
        {
            get { 
                return _question.CharCodes;
            }
        }
        
    }
}
