using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Conditions
{
    internal static class ValidationConditions
    {
        internal static bool IsScheduleDayValid(ScheduleDay scheduleDay)
            => scheduleDay == ScheduleDay.Weekdays || scheduleDay == ScheduleDay.Saturdays || scheduleDay == ScheduleDay.SundayAndHolidays;

        internal static bool IsDepartureTimeValid(TimeOnly time)
            => time.Hour >= 0 && time.Hour <= 23 && time.Minute >= 0 && time.Minute <= 59;

        internal static bool IsCoordinateValid(Coordinate coordinate)
            => coordinate.Lat >= -90 && coordinate.Lat <= 90 && coordinate.Lng >= -180 && coordinate.Lng <= 180;
    }
}
