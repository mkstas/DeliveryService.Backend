namespace Domain.Exceptions
{
    /// <summary>
    /// Thrown when the specified entity cannot be deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to delete.</typeparam>
    /// <param name="reason">The reason why entity cannot be deleted.</param>
    public class DeleteConstraintException<TEntity>(string reason)
        : DomainException($"Cannot delete {typeof(TEntity).Name}: {reason}.");
}
