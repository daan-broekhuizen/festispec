using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public interface IQuestionRepository
    {
        List<Vraag> GetVragen(int inspectieFormulierID);
        void UpdateVraag(Vraag vraag);
        void CreateVraag(Vraag vraag);
    }
}
