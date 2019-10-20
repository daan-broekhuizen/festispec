using Festispec.Model.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class SaleModel
    {
        public EnumMonth Month { get; set; }
        public string ZipCode { get; set; }

        public SaleModel()
        {

        }

        public SaleModel(EnumMonth month, string zipCode)
        {
            this.Month = month;
            this.ZipCode = zipCode;
        }
    }
}
