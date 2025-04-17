using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ZkmBusTimetables.Application.Features.Variants.Insert;
using ZkmBusTimetables.Application.Features.Variants.Delete;
using ZkmBusTimetables.Application.Features.Variants.DeleteAll;
using ZkmBusTimetables.Application.Features.Variants.Update;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.Features.Variants.Get;
using ZkmBusTimetables.Application.Features.Variants.GetAll;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Application.Features.Variants.GetByRouteStopId;
using Microsoft.AspNetCore.Authorization;
using ZkmBusTimetables.Application.Utils.UserContext;

namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/lines/{lineName}/variants")]

    public class VariantsController(IMediator mediator, IUserContext userContext) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            [FromRoute] Guid id,
            [FromRoute] string lineName,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetQuery(id, lineName), cancellationToken);
            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByBusStopId(
            [FromRoute] string lineName,
            [FromQuery] int busStopId,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetByRouteStopIdQuery(lineName, busStopId), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromRoute] string lineName,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetAllQuery(lineName), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromRoute] string lineName,
            [FromBody] VariantRequest request,
            CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            var response = await mediator.Send(new InsertCommand(lineName, request, currentUser), cancellationToken);
            return CreatedAtAction(nameof(Get), new { lineName, id = response.Variant.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromRoute] string lineName,
            [FromRoute] Guid id,
            [FromBody] VariantRequest request,
            CancellationToken cancellationToken)
        {

            return await mediator.Send(new UpdateCommand(lineName, id, request), cancellationToken)
                ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteCommand(id), cancellationToken)
                ? NoContent() : BadRequest();
        }
    }
}