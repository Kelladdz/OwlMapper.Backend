using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;

namespace ZkmBusTimetables.Core.Models
{
    public class Departure
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public Guid VariantId { get; set; }
        [JsonIgnore]
        public  Variant Variant { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsOnlyInSchoolDays { get; set; }
        public bool IsOnlyInDaysWithoutSchool { get; set; }
    }
}
