using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.Features.BusStops.Delete;
using ZkmBusTimetables.Application.Features.BusStops.DeleteAll;
using ZkmBusTimetables.Application.Features.BusStops.Get;
using ZkmBusTimetables.Application.Features.BusStops.GetAll;
using ZkmBusTimetables.Application.Features.BusStops.Insert;
using ZkmBusTimetables.Application.Features.BusStops.Search;
using ZkmBusTimetables.Application.Features.BusStops.Update;

namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/bus-stops")]
    public class BusStopsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(
            [FromRoute] string slug,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetQuery(slug), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new SearchQuery(term), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusStopRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new InsertCommand(request), cancellationToken);
            return CreatedAtAction(nameof(Get), new { slug = response.BusStop.Slug }, response);
        }

        [HttpPut("{slug}")]
        public async Task<IActionResult> Put(
            [FromRoute] string slug,
            [FromBody] BusStopRequest request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateCommand(slug, request), cancellationToken)
                ? NoContent() : BadRequest();
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string slug,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteCommand(slug), cancellationToken)
                ? NoContent() : BadRequest();
        }
    }
}
