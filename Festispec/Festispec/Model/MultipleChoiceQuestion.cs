using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class MultipleChoiceQuestion : Question
    {
        public List<String> PossibleAnswers { get; set; }

        public new int Answer;

        public Char AnswerChar { get; set; }

        public ObservableCollection<Char> CharCodes { get; set; }

        public MultipleChoiceQuestion()
        {
            PossibleAnswers = new List<string>();
            CharCodes = new ObservableCollection<char>();
            CharCodes.Add('a');
            CharCodes.Add('b');
            CharCodes.Add('c');
            CharCodes.Add('d');
        }

        public override void SetAnswer(String a)
        {
            for (int i = 0; i < PossibleAnswers.Count(); i++)
            {
                if (PossibleAnswers[i].Equals(a))
                {
                    Answer = i;
                }
            }
        }

        public override string GetAnswer()
        {
            return PossibleAnswers[Answer];
        }

        public String AnswerA
        {
            get
            {
                if (PossibleAnswers.Count >= 1)
                {
                    return PossibleAnswers[0]; 
                }
                return null;
            }
        }

        public String AnswerB
        {
            get {
                if (PossibleAnswers.Count >= 2)
                {
                    return PossibleAnswers[1];
                }
                return null;
            }
        }
        public String AnswerC
        {
            get {
                if (PossibleAnswers.Count >= 3)
                {
                    return PossibleAnswers[2];
                }
                return null;
            }
        }
        public String AnswerD
        {
            get {
                if (PossibleAnswers.Count == 4)
                {
                    return PossibleAnswers[3];
                }
                return null;
            }
        }

        
    }
}
