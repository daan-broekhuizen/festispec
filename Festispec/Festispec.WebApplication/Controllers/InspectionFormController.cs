using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using Festispec.WebApplication.ViewModels.Inspection;
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
        private string[] _validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
        // GET: InspectionForm
        public ActionResult Index(int userId)
        {
            List<Inspectieformulier> formList = _formRepo.GetInspectionforms(userId);
            formList.ForEach(i => i.Vraag.OrderBy(q => q.VolgordeNummer));
            return View(formList);
        }

        // GET: InspectionForm/Details/5
        public ActionResult Details(int inspectionId, int userId)
        {
            int? user = (int?)Session["user"];
            if (user == null)
                return RedirectToAction("Error", "Error");
            Inspectieformulier form = _formRepo.GetInspectionform(inspectionId);
            return View(GetViewModel(form, userId));
        }

        [HttpPost, ActionName("Details")]
        public ActionResult AddOrUpdateAnswer(QuestionViewModel questionVM)
        {
            ModelState.Clear();

            Vraag question = _formRepo.GetQuestion(questionVM.QuestionId);
            if (question.Vraagtype != "tv")
                AddOrUpdateSingleAnswer(questionVM);
            else
                AddOrUpdateTableAnswers(questionVM, question);

            Inspectieformulier form = _formRepo.GetInspectionform(question.InspectieFormulierID);
            return View(GetViewModel(form, questionVM.Answer.InspecteurID));
        }

        private void AddOrUpdateTableAnswers(QuestionViewModel questionVM, Vraag question)
        {
            foreach (TableAnswerViewModel a in questionVM.TableAnswers)
            {
                if (a.Text == null) continue;
                questionVM.Answer.AntwoordText = a.Text;
                questionVM.Answer.AntwoordNummer = a.Id;
                Antwoorden toUpdate = _formRepo.GetAnswer(questionVM.Answer);
                if (toUpdate == null)
                    _formRepo.CreateAnswer(questionVM.Answer);
                else
                    _formRepo.UpdateAnswer(questionVM.Answer);
            }
        }

        private void AddOrUpdateSingleAnswer(QuestionViewModel questionVM)
        {
            Antwoorden toUpdate = _formRepo.GetAnswer(questionVM.Answer);
            if(questionVM.ImageFile != null) UploadImage(questionVM);

            if (toUpdate != null)
            {
                toUpdate.AntwoordText = questionVM.AnswerText;
                toUpdate.AntwoordImage = questionVM.Answer.AntwoordImage;
                _formRepo.UpdateAnswer(toUpdate);
            }
            else
            {
                questionVM.Answer.AntwoordText = questionVM.AnswerText;
                _formRepo.CreateAnswer(questionVM.Answer);
            }
        }
        private void UploadImage(QuestionViewModel model)
        {
            if(model.ImageFile.ContentLength > 0 && _validImageTypes.Contains(model.ImageFile.ContentType))
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

            InspectionformViewModel formVM = new InspectionformViewModel(form)
            {
                Questions = questions,
                CompletedQuestions = 0
            };

            formVM.Questions.ForEach(q => 
            {
                q.QuestionId = q.Question.VraagID;
                q.TableAnswers = new List<TableAnswerViewModel>();

                if (q.Answer == null && q.Question.Vraagtype != "tx")
                {
                    q.Answer = new Antwoorden()
                    {
                        InspecteurID = userId,
                        VraagID = q.Question.VraagID,
                        AntwoordNummer = 1,
                    };

                    for (int i = 0; i < q.Question.VraagMogelijkAntwoord.Count(); i++)
                    {
                        q.TableAnswers.Add(new TableAnswerViewModel() { Id = q.TableAnswers.Count() + 1, Text = String.Empty });
                        q.TableAnswers.Add(new TableAnswerViewModel() { Id = q.TableAnswers.Count() + 1, Text = String.Empty });
                        q.TableAnswers.Add(new TableAnswerViewModel() { Id = q.TableAnswers.Count() + 1, Text = String.Empty });
                    }
                }
                    
                else
                {
                    if(q.Question.Vraagtype == "tv") InitTableAnswers(q, userId);
                    if(q.Question.Vraagtype != "tx") q.AnswerText = q.Answer.AntwoordText;
                    if (q.Question.Vraagtype == "tx" || q.AnswerText != null || q.Answer.AntwoordImage != null)
                    {
                        formVM.CompletedQuestions += 1;
                        q.IsAnswered = true;    
                    }
                }
            });

            return formVM;
        }

       private void InitTableAnswers(QuestionViewModel q, int userId)
        {
            List<Antwoorden> answers = q.Question.Antwoorden.Where(a => a.InspecteurID == userId).ToList();
            List<TableAnswerViewModel> newList = new List<TableAnswerViewModel>();
            answers.ForEach(a => q.TableAnswers.Add(new TableAnswerViewModel() { Id = a.AntwoordNummer, Text = a.AntwoordText }));
            int max = q.TableAnswers.OrderByDescending(t => t.Id).First().Id + q.Question.VraagMogelijkAntwoord.Count()*3;
            int amountOfAnswers = RoundAmountOfAnswers(max, q.Question.VraagMogelijkAntwoord.Count());

            for (int i = 0; i < amountOfAnswers; i++)
                newList.Add(new TableAnswerViewModel() { Id = i + 1, Text = String.Empty });
            for(int j = 0; j < q.TableAnswers.Count(); j++)
            {
                TableAnswerViewModel temp = newList.FirstOrDefault(a => a.Id == q.TableAnswers[j].Id);
                if (temp != null)
                    temp.Text = q.TableAnswers[j].Text;
            }

            q.TableAnswers = newList;
        }

        private int RoundAmountOfAnswers(int answers, int amount)
        {
            int a = (answers / amount) * amount;
            int b = a + amount;
            return (answers - a > b - answers) ? b : a;
        }
    }

}
