using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.BusStops.Update
{
    internal sealed class UpdateCommandHandler(IMapper mapper, IBusStopsRepository busStopsRepository) : IRequestHandler<UpdateCommand, bool>
    {
        public async Task<bool> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            var slug = command.Slug;
            var request = command.Request;

            var busStopToUpdate = mapper.Map<BusStop>(request);

            return await busStopsRepository.UpdateAsync(slug, busStopToUpdate, cancellationToken);
        }
    }
}
