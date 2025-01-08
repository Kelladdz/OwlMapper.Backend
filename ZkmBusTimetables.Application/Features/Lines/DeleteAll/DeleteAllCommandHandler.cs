using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Lines.DeleteAll
{
    internal sealed class DeleteAllCommandHandler(ILinesRepository linesRepository) : IRequestHandler<DeleteAllCommand, bool>
    {
        public async Task<bool> Handle(DeleteAllCommand command, CancellationToken cancellationToken)
        {
            return await linesRepository.DeleteAllAsync(cancellationToken);
        }
    }
}
