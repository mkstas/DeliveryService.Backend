namespace Domain.Exceptions
{
    /// <summary>
    /// Thrown when an entity already exists.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that already exists.</typeparam>
    /// <param name="value">The value that caused the conflict.</param>
    public class ConflictException<TEntity>(string value)
        : DomainException($"{typeof(TEntity).Name} with value {value} already exists.");
}
