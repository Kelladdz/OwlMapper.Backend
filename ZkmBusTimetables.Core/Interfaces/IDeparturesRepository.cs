using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Core.Interfaces
{
    public interface IDeparturesRepository
    {
        Task<IEnumerable<Departure>> GetByBusStopIdAsync(int busStopId, string lineName, IList<ScheduleDay> scheduleDayValues, int page, int pageSize, CancellationToken cancellationToken);
    }
}
