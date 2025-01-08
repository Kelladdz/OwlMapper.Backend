using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Application.DTOs.Responses;
using MediatR;

namespace ZkmBusTimetables.Application.Features.Variants.GetByRouteStopId
{
    internal sealed class GetByRouteStopIdQueryHandler(IVariantsRepository variantsRepository) : IRequestHandler<GetByRouteStopIdQuery, IEnumerable<VariantResponse>>
    {
        public async Task<IEnumerable<VariantResponse>> Handle(GetByRouteStopIdQuery query, CancellationToken cancellationToken)
        {
            var lineName = query.LineName;
            var busStopId = query.BusStopId;

            var variants = await variantsRepository.GetByBusStopIdAsync(lineName, busStopId, cancellationToken);

            var response = new List<VariantResponse>();
            foreach (var variant in variants)
            {
                response.Add(new VariantResponse(variant));
            }

            return response;
        }
    }
}
