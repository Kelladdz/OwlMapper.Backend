using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodBadHabitsTracker.Application.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationError> errors) : base("Validation failed")
        {
            Errors = errors;
        }
        public IEnumerable<ValidationError> Errors { get; }
    }

    public sealed record ValidationError(string PropertyName, string ErrorMessage);
}