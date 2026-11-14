namespace Domain.Exceptions
{
    /// <summary>
    /// Thrown when the specified entity not found.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that not found.</typeparam>
    /// <param name="id">The entity identifier</param>
    public class NotFoundException<TEntity>(Guid id)
        : DomainException($"{typeof(TEntity).Name} with id {id} not found.");
}
