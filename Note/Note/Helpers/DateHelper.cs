using System;
using System.Collections.Generic;
using System.Text;

namespace Note.Helpers
{
    public class DateHelper
    {
        public string ShortWeekName(int day)
        {
            if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Monday")
            {
                return "Пн";
            }
            else if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Tuesday")
            {
                return "Вт";
            }
            else if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Wednesday")
            {
                return "Ср";
            }
            else if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Thursday")
            {
                return "Чт";
            }
            else if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Friday")
            {
                return "Пт";
            }
            else if (DateTime.Now.AddDays(day).DayOfWeek.ToString() == "Saturday")
            {
                return "Сб";
            }
            else
            {
                return "Вс";
            }
        }
    }
}
