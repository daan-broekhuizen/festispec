using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories.Interfaces
{
    interface IVOCRepository
    {
        List<InspectieformulierVragenlijstCombinatie> GetVragenlijstCombinaties(int InspectieformulierID);
        void RemoveVraagFromInspectieFormulier(InspectieformulierVragenlijstCombinatie voc);
        void AddVraagToInspectieFormulier(InspectieformulierVragenlijstCombinatie voc);
    }
}
