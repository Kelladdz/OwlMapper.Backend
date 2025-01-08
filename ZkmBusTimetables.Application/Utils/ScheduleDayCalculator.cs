using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;

namespace ZkmBusTimetables.Application.Utils
{
    internal static class ScheduleDayCalculator
    {
        private static readonly int CurrentYear = DateTime.UtcNow.Year;
        private static readonly List<DateOnly> Holidays = new List<DateOnly>
        {
        // Nowy Rok
        new DateOnly(CurrentYear, 1, 1),
        // Święto Trzech Króli
        new DateOnly(CurrentYear, 1, 6),
        // Święto Pracy
        new DateOnly(CurrentYear, 5, 1),
        // Święto Konstytucji 3 Maja
        new DateOnly(CurrentYear, 5, 3),
        // Wniebowzięcie Najświętszej Maryi Panny
        new DateOnly(CurrentYear, 8, 15),
        // Wszystkich Świętych
        new DateOnly(CurrentYear, 11, 1),
        // Narodowe Święto Niepodległości
        new DateOnly(CurrentYear, 11, 11),
        // Boże Narodzenie
        new DateOnly(CurrentYear, 12, 25),
        // Drugi dzień Bożego Narodzenia
        new DateOnly(CurrentYear, 12, 26)
        };

        public static List<ScheduleDay> GetScheduleDayValues(DateOnly date)
        {
            var days = new List<DateOnly>() { date, date.AddDays(1), date.AddDays(2) };
            var values = new List<ScheduleDay>();

            foreach (var day in days)
            {
                Holidays.Add(GetEasterSunday(day.Year).AddDays(1)); // Poniedziałek Wielkanocny
                Holidays.Add(GetEasterSunday(day.Year).AddDays(60)); // Boże Ciało

                if (Holidays.Contains(day) || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    values.Add(ScheduleDay.SundayAndHolidays); // Niedziela lub święto
                }
                else if (day.DayOfWeek == DayOfWeek.Saturday)
                {
                    values.Add(ScheduleDay.Saturdays); // Sobota
                }
                else
                {
                    values.Add(ScheduleDay.Weekdays); // Dzień powszedni
                }
            }
            return values;
        }

        public static bool IsSchoolDay()
        {
            var today = DateTime.Today;
            return today.Month != 7 && today.Month != 8;
        }

        private static DateOnly GetEasterSunday(int year)
        {
            int month = 3;
            int g = year % 19;
            int c = year / 100;
            int h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
            int i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));
            int day = i - ((year + year / 4 + i + 2 - c + c / 4) % 7) + 28;
            if (day > 31)
            {
                month = 4;
                day -= 31;
            }
            return new DateOnly(year, month, day);
        }
    }
}
