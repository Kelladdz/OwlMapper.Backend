using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Application.Conditions;
using ZkmBusTimetables.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Lines.Insert
{
    public sealed class InsertCommandValidator : AbstractValidator<InsertCommand>
    {
        private readonly ILinesRepository _linesRepository;
        public InsertCommandValidator(ILinesRepository linesRepository)
        {
            _linesRepository = linesRepository;
            RuleFor(x => x.Request.LineName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(3).WithMessage("Name must not exceed 3 characters.")
                .MustAsync(IsUniqueName).WithMessage("The specified name already exists.");
            RuleFor(x => x.Request.RouteStops)
                .Custom((value, context) =>
                {
                    if (value.Count < 2)
                        context.AddFailure("Route must have at least 2 stops.");
                    else
                    {
                        var valueSortedByOrder = value.OrderBy(x => x.Order).ToList();
                        for (int i = 0; i < valueSortedByOrder.Count; i++)
                        {
                            if (i > 0 && valueSortedByOrder[i].TimeToTravelInMinutes < valueSortedByOrder[i - 1].TimeToTravelInMinutes)
                                context.AddFailure($"None of route stops can't have less timeToTravel than previous.");
                            if (i > 0 && valueSortedByOrder[i].Order - valueSortedByOrder[i - 1].Order != 1)
                                context.AddFailure($"Invalid order.");
                        }
                    }
                });
            RuleFor(x => x.Request.Departures)
                .Custom((value, context) =>
                {
                    if (!value.Any())
                        context.AddFailure("Line must have at least one departure request.");
                    else
                    {
                        foreach (var departure in value)
                        {
                            if (ValidationConditions.IsScheduleDayValid(departure.ScheduleDay))
                                context.AddFailure($"Invalid schedule day value: {departure.ScheduleDay}");
                            if (ValidationConditions.IsDepartureTimeValid(departure.Time))
                                context.AddFailure($"Invalid time: {departure.Time}");
                        }
                    }
                });
            RuleFor(x => x.Request.RouteLinePoints)
                .Custom((value, context) =>
                {
                    if (value.Count < 2)
                        context.AddFailure("Route must have at least two route line points.");
                    else
                    {
                        var valueSortedByOrder = value.OrderBy(x => x.Order).ToList();
                        for (int i = 0; i < valueSortedByOrder.Count; i++)
                        {
                            if (ValidationConditions.IsCoordinateValid(valueSortedByOrder[0].Coordinate))
                                context.AddFailure($"Invalid coordinate: {valueSortedByOrder[0].Order}. {valueSortedByOrder[0].Coordinate}.");
                            if (i > 0 && valueSortedByOrder[i].Order - valueSortedByOrder[i - 1].Order != 1)
                                context.AddFailure($"Invalid order.");
                        }
                    }
                });  


        }
        private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
         => await _linesRepository.GetAsync(name, cancellationToken) != null;
    }
}
