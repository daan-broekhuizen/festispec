using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public interface IQuestionnaireRepository
    {
        List<Inspectieformulier> GetInspectieformulier(int OpdrachtID);
        void UpdateInspectieFormulier(Inspectieformulier inspec);
        void CreateInspectieFormulier(Inspectieformulier inspec);
        void DeleteInspectieFormulier(Inspectieformulier inspec);
        
    }
}
