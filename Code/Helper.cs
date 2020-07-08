using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarGeneration
{
    public static class Helper
    {
        public static bool IsWeekend(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static bool IsWorkWeekend(this DateTime date)
            => Constants.WorkWeekendDays.Contains(date);

        public static bool IsOffWeekend(this DateTime date)
            => date.IsWeekend() && !date.IsWorkWeekend();

        public static bool IsHoliday(this DateTime date)
            => Constants.Holidays.Contains(date);

        public static bool IsOffDay(this DateTime date)
            => !date.IsWorkWeekend() && (date.IsWeekend() || date.IsHoliday());

        public static int ToInt(this bool value)
            => value ? 1 : 0;

        private static readonly DateTime LastDate = new DateTime(2021, 1, 4);

        public static List<DateDto> DateGeneration()
        {
            var result = new List<DateTime>
            {
                new DateTime(2015, 12, 29)
            };

            do
            {
                result.Add(result.Last().AddDays(1));
            } while (result.Last() != LastDate);

            return result.Where(x => x.Year != 2015 && x.Year != 2021).Select(x => new DateDto(x)).ToList();
        }

        private const int HolidayOutNum = 8;
        private const int OffDayOutNum = 4;
        private const int InNum = 0;

        public static List<DateDto> DateForwardProcess(this List<DateDto> dateDtos)
        {
            bool frontHoliday = false;
            int holidayIn = InNum;
            int holidayOut = HolidayOutNum;

            bool frontOffDay = false;
            int offDayIn = InNum;
            int offDayOut = OffDayOutNum;


            foreach (var dto in dateDtos)
            {
                if (dto.IsHoliday == 1)
                {
                    if (!frontHoliday)
                    {
                        dto.HolidayFrontEdge = 1;
                        holidayOut = HolidayOutNum;
                        frontHoliday = true;
                    }

                    dto.HolidayInForward = ++holidayIn;
                }
                else
                {
                    if (frontHoliday)
                    {
                        holidayIn = InNum;
                        frontHoliday = false;
                    }

                    dto.HolidayOutForward = holidayOut == 0 ? holidayOut : --holidayOut;
                }


                if (dto.IsOffDay == 1)
                {
                    if (!frontOffDay)
                    {
                        dto.OffDayFrontEdge = 1;
                        offDayOut = OffDayOutNum;
                        frontOffDay = true;
                    }

                    dto.OffDayInForward = ++offDayIn;
                }
                else
                {
                    if (frontOffDay)
                    {
                        offDayIn = InNum;
                        frontOffDay = false;
                    }

                    dto.OffDayOutForward = offDayOut == 0 ? offDayOut : --offDayOut;
                }
            }

            return dateDtos;
        }

        public static List<DateDto> DateBackwardProcess(this List<DateDto> dateDtos)
        {
            bool rearHoliday = false;
            int holidayIn = InNum;
            int holidayOut = HolidayOutNum;

            bool rearOffDay = false;
            int offDayIn = InNum;
            int offDayOut = OffDayOutNum;


            foreach (var dto in dateDtos.OrderByDescending(x => x.Date))
            {
                if (dto.IsHoliday == 1)
                {
                    if (!rearHoliday)
                    {
                        dto.HolidayRearEdge = 1;
                        holidayOut = HolidayOutNum;
                        rearHoliday = true;
                    }

                    dto.HolidayInBackward = ++holidayIn;
                }
                else
                {
                    if (rearHoliday)
                    {
                        holidayIn = InNum;
                        rearHoliday = false;
                    }

                    dto.HolidayOutBackward = holidayOut == 0 ? holidayOut : --holidayOut;
                }


                if (dto.IsOffDay == 1)
                {
                    if (!rearOffDay)
                    {
                        dto.OffDayRearEdge = 1;
                        offDayOut = OffDayOutNum;
                        rearOffDay = true;
                    }

                    dto.OffDayInBackward = ++offDayIn;
                }
                else
                {
                    if (rearOffDay)
                    {
                        offDayIn = InNum;
                        rearOffDay = false;
                    }

                    dto.OffDayOutBackward = offDayOut == 0 ? offDayOut : --offDayOut;
                }
            }

            return dateDtos;
        }
    }
}