using System.Collections.Generic;
using Validation.Abstractions.Entities;

namespace Validation.Abstractions.Services.Abstractions
{
    public interface IValidationService<in TEntity> where TEntity : class
    {
        IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
    }
}