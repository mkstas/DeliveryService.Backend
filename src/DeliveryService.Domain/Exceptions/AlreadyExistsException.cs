using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Exceptions
{
    /// <summary>
    /// Thrown when an entity of the specified type already exists.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that already exists.</typeparam>
    /// <param name="value">The value that caused the conflict.</param>
    public class AlreadyExistsException<TEntity>(object value)
        : DomainException($"{typeof(TEntity).Name} with value {value} already exists.");
}
