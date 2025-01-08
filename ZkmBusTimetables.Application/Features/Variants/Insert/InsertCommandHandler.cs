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

namespace ZkmBusTimetables.Application.Features.Variants.Insert
{
    internal sealed class InsertCommandHandler(IMapper mapper, IVariantsRepository variantsRepository) : IRequestHandler<InsertCommand, VariantResponse>
    {
        public async Task<VariantResponse> Handle(InsertCommand command, CancellationToken cancellationToken)
        {
            var lineName = command.LineName;

            var request = command.Request;

            var variantToInsert = mapper.Map<Variant>(request);


            var createdVariant = await variantsRepository.InsertAsync(lineName, variantToInsert, cancellationToken);

            return new VariantResponse(createdVariant);
        }
    }
}
