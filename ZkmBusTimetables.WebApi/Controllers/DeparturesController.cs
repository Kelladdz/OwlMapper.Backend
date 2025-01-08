using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZkmBusTimetables.Application.Features.Departures.GetByBusStopId;

namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/departures")]
    public class DeparturesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("filter")]
        public async Task<IActionResult> GetByBusStopId([FromQuery] int busStopId, [FromQuery] string lineName, [FromQuery] DateOnly date, CancellationToken cancellationToken, int page = 1, int pageSize = 0)
        {
            var response = await mediator.Send(new GetByBusStopIdQuery(busStopId, page, pageSize, lineName, date), cancellationToken);
            return Ok(response);
        }
    }
}
