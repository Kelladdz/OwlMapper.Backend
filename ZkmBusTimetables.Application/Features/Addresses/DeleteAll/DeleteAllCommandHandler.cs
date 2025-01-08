using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Addresses.DeleteAll
{
    internal sealed class DeleteAllCommandHandler(IAddressesRepository adressesRepository) : IRequestHandler<DeleteAllCommand, bool>
    {
        public async Task<bool> Handle(DeleteAllCommand command, CancellationToken cancellationToken)
        {
            return await adressesRepository.DeleteAllAsync(cancellationToken);
        }
    }
}
