namespace Domain.Entities.Common
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public Guid Id { get; private init; } = Guid.CreateVersion7();

        /// <summary>
        /// The date when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

        /// <summary>
        /// The date when the entity was updated.
        /// </summary>
        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    }
}
