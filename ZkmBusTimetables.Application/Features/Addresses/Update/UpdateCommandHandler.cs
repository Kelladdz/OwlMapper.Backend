using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Addresses.Update
{
    internal sealed class UpdateCommandHandler(IAddressesRepository addressesRepository) : IRequestHandler<UpdateCommand, bool>
    {
        public async Task<bool> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            var id = command.Id;
            var addressesDocument = command.AddressesDocument;

            return await addressesRepository.UpdateAsync(id, addressesDocument, cancellationToken);
        }
    }
}
