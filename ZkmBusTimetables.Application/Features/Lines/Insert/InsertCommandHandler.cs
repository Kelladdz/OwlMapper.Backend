using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Application.Features.Lines.Insert;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Lines.Create
{
    internal sealed class InsertCommandHandler(IMapper mapper, ILinesRepository linesRepository) : IRequestHandler<InsertCommand, LineResponse>
    {
        public async Task<LineResponse> Handle(InsertCommand command, CancellationToken cancellationToken)
        {
            var line = mapper.Map<Line>(command.Request);

            var createdLine = await linesRepository.InsertAsync(line, cancellationToken);
            return new LineResponse(createdLine);
        }
    }
}
