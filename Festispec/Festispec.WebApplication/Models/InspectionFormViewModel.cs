using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models
{
    public class InspectionformViewModel
    {
        public Inspectieformulier Inspectionform { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}