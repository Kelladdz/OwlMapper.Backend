using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZkmBusTimetables.Application.DTOs.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using ZkmBusTimetables.Application.Features.Lines.Get;
using ZkmBusTimetables.Application.Features.Lines.GetAll;
using ZkmBusTimetables.Application.Features.Lines.GetByRouteStopId;
using ZkmBusTimetables.Application.Features.Lines.Update;
using ZkmBusTimetables.Application.Features.Lines.Delete;
using ZkmBusTimetables.Application.Features.Lines.DeleteAll;
using ZkmBusTimetables.Application.Features.Lines.Insert;


namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class LinesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{lineName}")]
        public async Task<IActionResult> Get([FromRoute] string lineName, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetQuery(lineName), cancellationToken);
            return Ok(response);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetByBusStopId([FromQuery] int busStopId, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetByBusStopIdQuery(busStopId), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LineRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new InsertCommand(request), cancellationToken);
            return CreatedAtAction(nameof(Get), new { lineName = response.Line.Name }, response);
        }

        [HttpPatch("{name}")]
        public async Task<IActionResult> Put(
            [FromRoute] string name,
            [FromBody] JsonPatchDocument lineDocument,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateCommand(name, lineDocument), cancellationToken)
                ? NoContent() : BadRequest();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteCommand(name), cancellationToken)
                ? NoContent() : BadRequest();
        }
    }
}
