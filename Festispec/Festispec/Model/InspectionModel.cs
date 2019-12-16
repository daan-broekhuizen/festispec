using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class InspectionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public InspectionModel(int id, string name, DateTime date)
        {
            this.ID = id;
            this.Name = name;
            this.Date = date;
        }
    }
}
