using AutoMapper;
using MediatR;
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

            return await variantsRepository.UpdateAsync(lineName, variantId, variantToUpdate, cancellationToken);
        }
    }
}