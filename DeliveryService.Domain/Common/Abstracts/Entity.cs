namespace DeliveryService.Domain.Common.Abstracts
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Unique identifier of the entity.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Initializes a new instance with a unique identifier.
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
