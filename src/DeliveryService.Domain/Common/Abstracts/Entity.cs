namespace DeliveryService.Domain.Common.Abstracts
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a generated identifier.
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
