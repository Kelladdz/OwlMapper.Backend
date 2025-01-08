using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.BusStops.Delete
{
    internal sealed class DeleteCommandHandler(IBusStopsRepository busStopsRepository) : IRequestHandler<DeleteCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            var slug = command.Slug;

            return await busStopsRepository.DeleteAsync(slug, cancellationToken);
        }
    }
}
