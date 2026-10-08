using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents an address of an establishment within the system.
    /// </summary>
    public class EstablishmentAddress : Entity
    {
        /// <summary>
        /// The identifier of the establishment this address belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The address of the establishment.
        /// </summary>
        public StringBounded Address { get; init; }

        /// <summary>
        /// The establishment this address belongs to.
        /// </summary>
        public Establishment Establishment { get; init; } = null!;

        private EstablishmentAddress(Guid establishmentId, StringBounded address)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            Address = address;
        }

        /// <summary>
        /// Creates a new <see cref="EstablishmentAddress"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this address belongs to.</param>
        /// <param name="address">The establishment address. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="EstablishmentAddress"/> instance.</returns>
        internal static EstablishmentAddress Create(Guid establishmentId, StringBounded address) => new(establishmentId, address);
    }
}
