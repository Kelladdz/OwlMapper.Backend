using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Lines.Update
{
    internal sealed class UpdateCommandHandler(ILinesRepository linesRepository) : IRequestHandler<UpdateCommand, bool>
    {
        public async Task<bool> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            var name = command.Name;
            var linesDocument = command.LinesDocument;

            return await linesRepository.UpdateAsync(name, linesDocument, cancellationToken);
        }
    }
}
