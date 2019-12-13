using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication.Controllers
{
    public class InspectionformController : Controller
    {
        private InspectionformRepository _formRepo = new InspectionformRepository();
        // GET: InspectionForm
        public ActionResult Index(int userId)
        {
            List<Inspectieformulier> formList = _formRepo.GetInspectionforms(userId);
            formList.ForEach(i => i.Vraag.OrderBy(q => q.VolgordeNummer));
            return View(formList);
        }

        // GET: InspectionForm/Details/5
        public ActionResult Details(int i = 1, int j = 1)
        {
            Inspectieformulier form = _formRepo.GetInspectionform(1);
            return View(GetViewModel(form, 1));
        }

        [HttpPost, ActionName("Details")]
        public ActionResult AddOrUpdateAnswer(QuestionViewModel questionVM)
        {
            Antwoorden toUpdate = _formRepo.GetAnswer(questionVM.Answer);
            toUpdate.AntwoordText = questionVM.AnswerText;
            if (toUpdate != null)
                _formRepo.UpdateAnswer(toUpdate);
            else
            {
                questionVM.Answer.AntwoordText = questionVM.AnswerText;
                _formRepo.CreateAnswer(questionVM.Answer);
            }
            ModelState.Clear();
            Inspectieformulier form = _formRepo.GetInspectionform(toUpdate.Vraag.InspectieFormulierID);
            return View(GetViewModel(form, questionVM.Answer.InspecteurID));
        }

        private InspectionformViewModel GetViewModel(Inspectieformulier form, int userId)
        {
            form.Vraag = form.Vraag.OrderBy(q => q.VraagID).ToList();
            List<QuestionViewModel> questions = form.Vraag.Select(q => new QuestionViewModel()
            {
                Question = q,
                Answer = q.Antwoorden.FirstOrDefault(a => a.Account.AccountID == userId)
            }).ToList();

            questions.ForEach(q => 
            {
                if (q.Answer == null)
                    q.Answer = new Antwoorden()
                    {
                        InspecteurID = userId,
                        VraagID = q.Question.VraagID,
                        //Todo multiple answers
                        AntwoordNummer = 1
                    };
                else
                {
                    q.AnswerText = q.Answer.AntwoordText;
                    q.AnswerImage = q.Answer.AntwoordImage;
                }
            });

            InspectionformViewModel formVM = new InspectionformViewModel()
            {
                Inspectionform = form,
                Questions = questions
            };

            return formVM;
        }

    }
}
