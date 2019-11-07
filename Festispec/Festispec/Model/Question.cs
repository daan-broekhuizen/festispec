using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public abstract class Question
    {
        public int QuestionID { get; set; }
        public String QuestionText {get; set; }

        public String Picture { get; set; }

        public String Answer { get; set; }

        public virtual void SetAnswer(String a)
        {
            Answer = a;
        }

        public virtual String GetAnswer()
        {
            return Answer;
        }
    }
}
