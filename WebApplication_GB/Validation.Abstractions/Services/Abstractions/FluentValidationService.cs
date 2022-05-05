using System;
using System.Collections.Generic;
using FluentValidation;
using Validation.Abstractions.Entities;

namespace Validation.Abstractions.Services.Abstractions
{
    public abstract class FluentValidationService<TEntity> : AbstractValidator<TEntity>, IValidationService<TEntity>
        where TEntity : class
    {
        public IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item)
        {
            var result = Validate(item);
            if (result is null || result.Errors.Count == 0)
            {
                return ArraySegment<IOperationFailure>.Empty;
            }

            var failures = new List<IOperationFailure>(result.Errors.Count);
            foreach (var error in result.Errors)
            {
                var failure = new OperationFailure
                {
                    PropertyName = error.PropertyName,
                    Description = error.ErrorMessage,
                    Code = error.ErrorCode
                };
                failures.Add(failure);
            }

            return failures;
        }
    }
}