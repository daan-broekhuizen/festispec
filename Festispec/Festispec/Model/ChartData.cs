using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class ChartData
    {
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public int Amount { get; set; }
    }
}
