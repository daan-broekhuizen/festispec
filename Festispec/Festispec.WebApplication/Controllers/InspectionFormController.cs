using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Details(int i = 1, int j = 2)
        {
            Inspectieformulier form = _formRepo.GetInspectionform(i);
            return View(GetViewModel(form, j));
        }

        [HttpPost, ActionName("Details")]
        public ActionResult AddOrUpdateAnswer(QuestionViewModel questionVM)
        {
            ModelState.Clear();
            Antwoorden toUpdate = _formRepo.GetAnswer(questionVM.Answer);
            UploadImage(questionVM);
            
            if (toUpdate != null)
            {
                toUpdate.AntwoordText = questionVM.AnswerText;
                toUpdate.AntwoordImage = questionVM.Answer.AntwoordImage;
                _formRepo.UpdateAnswer(toUpdate);
            }
            else
            {
                questionVM.Answer.AntwoordText = questionVM.AnswerText;
                toUpdate = _formRepo.CreateAnswer(questionVM.Answer);
            }
            Inspectieformulier form = _formRepo.GetInspectionform(toUpdate.Vraag.InspectieFormulierID);
            return View(GetViewModel(form, questionVM.Answer.InspecteurID));
        }

        private void UploadImage(QuestionViewModel model)
        {
            string[] validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if(model.ImageFile != null && model.ImageFile.ContentLength > 0 && validImageTypes.Contains(model.ImageFile.ContentType))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.ImageFile.InputStream.CopyTo(ms);
                    model.Answer.AntwoordImage = ms.GetBuffer();
                }
            }
        }        

        private InspectionformViewModel GetViewModel(Inspectieformulier form, int userId)
        {
            form.Vraag = form.Vraag.OrderBy(q => q.VraagID).ToList();
            List<QuestionViewModel> questions = form.Vraag.Select(q => new QuestionViewModel()
            {
                Question = q,
                Answer = q.Antwoorden.FirstOrDefault(a => a.Account.AccountID == userId)
            }).ToList();

            InspectionformViewModel formVM = new InspectionformViewModel()
            {
                Inspectionform = form,
                Questions = questions,
                CompletedQuestions = 0
            };

            formVM.Questions.ForEach(q => 
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
                    formVM.CompletedQuestions += 1;
                    q.IsAnswered = true;
                }
            });

            return formVM;
        }

    }
}
