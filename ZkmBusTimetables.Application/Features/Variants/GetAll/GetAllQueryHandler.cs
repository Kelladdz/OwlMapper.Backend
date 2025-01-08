using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Variants.GetAll
{
    internal sealed class GetAllQueryHandler(IVariantsRepository variantsRepository) : IRequestHandler<GetAllQuery, IEnumerable<VariantResponse>>
    {
        public async Task<IEnumerable<VariantResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        {
            var lineName = query.LineName;

            var variants = await variantsRepository.GetAllAsync(lineName, cancellationToken);

            var response = new List<VariantResponse>();
            foreach (var variant in variants)
            {
                response.Add(new VariantResponse(variant));
            }

            return response;
        }
    }
}
