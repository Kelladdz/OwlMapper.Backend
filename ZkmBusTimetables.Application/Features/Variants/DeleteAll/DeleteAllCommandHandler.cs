using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Variants.DeleteAll
{
    internal sealed class DeleteAllCommandHandler(IVariantsRepository variantsRepository) : IRequestHandler<DeleteAllCommand, bool>
    {
        public async Task<bool> Handle(DeleteAllCommand command, CancellationToken cancellationToken)
        {
            return await variantsRepository.DeleteAllAsync(cancellationToken);
        }
    }
}
