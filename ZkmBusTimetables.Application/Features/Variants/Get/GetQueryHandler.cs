using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Variants.Get
{
    internal sealed class GetQueryHandler(IVariantsRepository variantsRepository) : IRequestHandler<GetQuery, VariantResponse>
    {
        public async Task<VariantResponse> Handle(GetQuery query, CancellationToken cancellationToken)
        {
            var variantId = query.Id;
            var lineName = query.LineName;
            var variant = await variantsRepository.GetAsync(lineName, variantId, cancellationToken);

            return new VariantResponse(variant);
        }
    }
}
