using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Variants.Update
{
    internal sealed class UpdateCommandHandler(IMapper mapper, IVariantsRepository variantsRepository) : IRequestHandler<UpdateCommand, bool>
    {
        public async Task<bool> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            var lineName = command.LineName;
            var variantId = command.Id;  
            var request = command.Request;

            var variantToUpdate = mapper.Map<Variant>(request);
            variantToUpdate.Id = variantId;

            return await variantsRepository.UpdateAsync(lineName, variantId, variantToUpdate, cancellationToken);
        }
    }
}
