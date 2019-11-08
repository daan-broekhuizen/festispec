using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Misc
{
    public static class DateTimeExtension
    {
        public static int WeekNumber(System.DateTime value)
        {
            return
                CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                (value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
