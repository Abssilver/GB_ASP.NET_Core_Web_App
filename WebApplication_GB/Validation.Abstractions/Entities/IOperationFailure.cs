namespace Validation.Abstractions.Entities
{
    public interface IOperationFailure
    {
        string PropertyName { get; }
        string Description { get; }
        string Code { get; }
    }
}