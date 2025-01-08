using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record DepartureRequest(ScheduleDay ScheduleDay, TimeOnly Time, bool IsOnlyInSchoolDays, bool IsOnlyInDaysWithoutSchool);
}
