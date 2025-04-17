using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodBadHabitsTracker.Application.Exceptions;
using ZkmBusTimetables.Application.Exceptions;
using ValidationException = GoodBadHabitsTracker.Application.Exceptions.ValidationException;

namespace ZkmBusTimetables.Application.Abstractions.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var errors = validators.Select(validator => validator.Validate(context))
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationResult => new ValidationError(
                    validationResult.PropertyName,
                    validationResult.ErrorMessage
                ))
                .ToList();

            if (errors.Count() > 0)
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}