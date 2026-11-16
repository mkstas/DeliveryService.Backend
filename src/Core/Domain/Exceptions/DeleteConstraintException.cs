using Domain.Abstractions;

namespace Domain.Exceptions
{
    /// <summary>
    /// Thrown when the specified entity cannot be deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that cannot be deleted.</typeparam>
    /// <param name="id">The entity identifier.</param>
    public class DeleteConstraintException<TEntity>(Guid id)
        : DomainException($"{typeof(TEntity).Name} with id {id} cannot be deleted.");
}
