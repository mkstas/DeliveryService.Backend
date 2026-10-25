using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Persistence.Postgres
{
    /// <summary>
    /// Provides read/write access to establishments.
    /// </summary>
    public interface IEstablishmentRepository
    {
        /// <summary>
        /// Creates a new establishment.
        /// </summary>
        /// <param name="establishment">The establishment to create.</param>
        Task CreateEstablishmentAsync(Establishment establishment);

        /// <summary>
        /// Returns an establishment by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <returns>The matching establishment, or <c>null</c> if none found.</returns>
        Task<Establishment?> GetEstablishmentByIdAsync(Guid establishmentId);

        /// <summary>
        /// Returns all establishments in the system.
        /// </summary>
        /// <returns>All establishments in the system.</returns>
        Task<List<Establishment>> GetEstablishmentsAsync();

        /// <summary>
        /// Updates the specified establishment.
        /// </summary>
        /// <param name="establishment">The establishment to update.</param>
        Task UpdateEstablishmentAsync(Establishment establishment);
    }
}
