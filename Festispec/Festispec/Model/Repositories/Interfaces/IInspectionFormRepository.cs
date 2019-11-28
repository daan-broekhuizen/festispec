using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public interface IInspectionFormRepository
    {
        List<Inspectieformulier> GetInspectieformulier(int OpdrachtID);
        void UpdateInspectieFormulier(Inspectieformulier inspec);
        void CreateInspectieFormulier(Inspectieformulier inspec);
        void DeleteInspectieFormulier(Inspectieformulier inspec);
        void AddQuestionToInspectionForm(InspectieformulierVragenlijstCombinatie voc);
        void RemoveQuestionFromInspectionForm(InspectieformulierVragenlijstCombinatie voc);
        void UpdateQuestionOrderInspectionForm(List<InspectieformulierVragenlijstCombinatie> newOrder);
        void UpdateQuestion(Vraag question);
        void DeleteQuestion(Vraag question);
        void AddQueistion(Vraag question);
        void DeletePossibleAnwser(VraagMogelijkAntwoord pos);
        void AddPossibleAnwser(VraagMogelijkAntwoord pos);
        void updatePossibleAnswer(VraagMogelijkAntwoord pos);
    }
}
