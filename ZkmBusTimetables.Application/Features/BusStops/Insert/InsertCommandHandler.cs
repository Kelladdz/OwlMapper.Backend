using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.BusStops.Insert
{
    internal sealed class InsertCommandHandler(IBusStopsRepository busStopsRepository, IMapper mapper) : IRequestHandler<InsertCommand, BusStopResponse>
    {
        public async Task<BusStopResponse> Handle(InsertCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var stopToInsert = mapper.Map<BusStop>(request);

            var response = await busStopsRepository.InsertAsync(stopToInsert, cancellationToken);
            return new BusStopResponse(response);
        }
    }
}
