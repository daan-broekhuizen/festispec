using Festispec.Model.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model
{
    public class AvailabilityModel
    {
        public int WeekNumber { get; set; }
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }

        public AvailabilityModel()
        {

        }

        public AvailabilityModel(DateTime date)
        {
            this.Date = date;
            this.WeekNumber = DateTimeExtension.WeekNumber(date);
            this.DayNumber = (int)date.DayOfWeek+1;
        }

    }
}
