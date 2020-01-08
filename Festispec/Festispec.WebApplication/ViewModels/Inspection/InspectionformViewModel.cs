using Festispec.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.ViewModels.Inspection
{
    public class InspectionformViewModel
    {
        public InspectionformViewModel(Inspectieformulier form)
        {
            Inspectionform = form;
        }
 
        public Inspectieformulier Inspectionform { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public string LogoString => Inspectionform.Opdracht.Klant.KlantLogo != null ? Convert.ToBase64String(Inspectionform.Opdracht.Klant.KlantLogo) : null;
        public int CompletedQuestions { get; set; }
        public bool IsEditable => DateTime.Now.Date == Inspectionform.DatumInspectie;
        
        
    }
}