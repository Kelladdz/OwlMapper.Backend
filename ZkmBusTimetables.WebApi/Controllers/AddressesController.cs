using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.Features.Addresses.Delete;
using ZkmBusTimetables.Application.Features.Addresses.DeleteAll;
using ZkmBusTimetables.Application.Features.Addresses.Get;
using ZkmBusTimetables.Application.Features.Addresses.GetAll;
using ZkmBusTimetables.Application.Features.Addresses.Insert;
using ZkmBusTimetables.Application.Features.Addresses.Search;
using ZkmBusTimetables.Application.Features.Addresses.Update;



namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AddressesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetQuery(id), cancellationToken);
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

        [HttpPost("batch")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<AddressRequest> request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new InsertCommand(request), cancellationToken);
            var createdResults = response.Select(x => new
            {
                x.Address,
                Url = Url.Action(nameof(Get), new { id = x.Address.Id })
            }).ToList();

            return Ok(createdResults);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(
            [FromRoute] Guid id,
            [FromBody] JsonPatchDocument addressesDocument,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateCommand(id, addressesDocument), cancellationToken)
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteAllCommand(), cancellationToken)
                ? NoContent() : BadRequest();
        }
    }
}
