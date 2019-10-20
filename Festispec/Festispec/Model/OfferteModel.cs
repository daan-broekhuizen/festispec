using Festispec.Model.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class OfferteModel
    {
        public string Name { get; set; }

        public EnumOfferteStatus Status { get; set; }

        public OfferteModel()
        {

        }

        public OfferteModel(string name, EnumOfferteStatus status)
        {
            this.Name = name;
            this.Status = status;
        }
    }
}
