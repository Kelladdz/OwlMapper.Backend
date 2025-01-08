using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Variants.Delete
{
    internal sealed class DeleteCommandHandler(IVariantsRepository variantsRepository) : IRequestHandler<DeleteCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            var id = command.Id;

            return await variantsRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
