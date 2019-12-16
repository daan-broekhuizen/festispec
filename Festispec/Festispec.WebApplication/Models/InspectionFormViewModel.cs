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
        public string LogoString
        {
            get
            {
                if (Inspectionform.Opdracht.Klant.Klant_logo != null)
                    return Convert.ToBase64String(Inspectionform.Opdracht.Klant.Klant_logo);
                else
                    return null;
            }
        }
        public int CompletedQuestions { get; set; }
        public bool IsEditable => DateTime.Now.Date == Inspectionform.DatumInspectie;
        
        
    }
}