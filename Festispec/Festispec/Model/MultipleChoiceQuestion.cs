using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class MultipleChoiceQuestion : Question
    {
        public List<String> PossibleAnswers { get; set; }

        public new int Answer;

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

    }
}
