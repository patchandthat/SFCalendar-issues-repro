using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfCalendar.XForms;

namespace CalendarDemo
{
    class CalendarEventDataSource
    {
        private Random random = new Random();

        public CalendarDayModel GetDayModelFor(DateTime date, SfCalendar calendar)
        {
            return new CalendarDayModel()
            {
                Calendar = calendar,
                Date = date.Date,
                HasEvents = random.NextDouble() >= 0.5,
                IsToday = date.Day == DateTime.Today.Day &&
                              date.Month == DateTime.Today.Month &&
                              date.Year == DateTime.Today.Year,
                IsCurrentMonth = date.Month == calendar.DisplayDate.Month &&
                                 date.Year == calendar.DisplayDate.Year
            };
        }
    }

    class CalendarDayModel
    {
        public string DayNumber => Date.Day.ToString();
        public bool IsCurrentMonth { get; set; }
        public bool IsSelected { get; set; }
        public bool IsToday { get; set; }
        public bool HasEvents { get; set; }
        public SfCalendar Calendar { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Day {DayNumber}, CurrentMonth:{IsCurrentMonth}, Selected:{IsSelected}, HasEvents:{HasEvents}";
        }
    }
}
