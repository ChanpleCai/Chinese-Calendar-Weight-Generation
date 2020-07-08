using System;

namespace CalendarGeneration
{
    public class DateDto
    {
        public DateDto(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }

        public int Year => Date.Year;

        public int Month => Date.Month;

        public int Day => Date.Day;

        public string ShortDateString => Date.ToString("yyyy-MM-dd");

        public int IsWeekend => Date.IsWeekend().ToInt();

        public int IsWorkWeekend => Date.IsWorkWeekend().ToInt();

        public int IsOffWeekend => Date.IsOffWeekend().ToInt();

        public int IsHoliday => Date.IsHoliday().ToInt();

        public int IsOffDay => Date.IsOffDay().ToInt();


        public int HolidayFrontEdge { get; set; }

        public int HolidayRearEdge { get; set; }

        public int HolidayEdge => HolidayFrontEdge | HolidayRearEdge;


        public int HolidayOutForward { get; set; }

        public int HolidayOutBackward { get; set; }

        public int HolidayOutward => Math.Max(HolidayOutForward, HolidayOutBackward);


        public int HolidayInForward { get; set; }

        public int HolidayInBackward { get; set; }

        public int HolidayInward => Math.Min(HolidayInForward, HolidayInBackward);


        public int OffDayFrontEdge { get; set; }

        public int OffDayRearEdge { get; set; }

        public int OffDayEdge => OffDayFrontEdge | OffDayRearEdge;


        public int OffDayOutForward { get; set; }

        public int OffDayOutBackward { get; set; }

        public int OffDayOutward => Math.Max(OffDayOutForward, OffDayOutBackward);


        public int OffDayInForward { get; set; }

        public int OffDayInBackward { get; set; }

        public int OffDayInward => Math.Min(OffDayInForward, OffDayInBackward);
    }
}