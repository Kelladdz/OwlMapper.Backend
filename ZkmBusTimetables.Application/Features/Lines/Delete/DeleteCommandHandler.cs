using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Lines.Delete
{
    internal sealed class DeleteCommandHandler(ILinesRepository linesRepository) : IRequestHandler<DeleteCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            var name = command.Name;

            return await linesRepository.DeleteAsync(name, cancellationToken);
        }
    }
}
