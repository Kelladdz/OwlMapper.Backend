using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Addresses.Delete
{
    internal sealed class DeleteCommandHandler(IAddressesRepository addressesRepository) : IRequestHandler<DeleteCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            var id = command.Id;

            return await addressesRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
