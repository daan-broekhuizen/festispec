using Festispec.Model.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class InspectorModel
    {
        public string Name { get; set; }

        public EnumMonth RegistrationMonth { get; set; }

        public InspectorModel()
        {

        }

        public InspectorModel(string name, EnumMonth month)
        {
            this.Name = name;
            this.RegistrationMonth = month;
        }
    }
}
