using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing establishments.
    /// </summary>
    public interface IEstablishmentService
    {
        /// <summary>
        /// Creates a new establishment.
        /// </summary>
        /// <param name="name">The name of the establishment to create.</param>
        /// <param name="businessAccount">The business bank account of the establishment.</param>
        Task CreateEstablishmentAsync(StringBounded name, BankAccount businessAccount);

        /// <summary>
        /// Returns an establishment by its identifier.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <returns>The matching establishment.</returns>
        Task<Establishment> GetEstablishmentByIdAsync(Guid establishmentId);

        /// <summary>
        /// Returns all establishments in the system.
        /// </summary>
        /// <returns>All establishments in the system.</returns>
        Task<List<Establishment>> GetEstablishmentsAsync();

        /// <summary>
        /// Updates the specified establishment.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <param name="name">The new name of the establishment.</param>
        /// <param name="businessAccount">The new business bank account of the establishment.</param>
        Task UpdateEstablishmentAsync(Guid establishmentId, StringBounded name, BankAccount businessAccount);

        /// <summary>
        /// Associates a user with the specified establishment.
        /// </summary>
        /// <param name="address">The user email address.</param>
        /// <param name="establishmentId">The establishment identifier.</param>
        Task AddUserEstablishmentAsync(Guid establishmentId, EmailAddress address);

        /// <summary>
        /// Removes the association between a user and the specified establishment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="establishmentId">The establishment identifier.</param>
        Task RemoveUserEstablishmentAsync(Guid establishmentId, Guid userId);

        /// <summary>
        /// Adds an address to the specified establishment.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <param name="address">The address to add.</param>
        Task AddEstablishmentAddressAsync(Guid establishmentId, StringBounded address);

        /// <summary>
        /// Removes an address from the specified establishment.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <param name="establishmentAddressId">The establishment address identifier.</param>
        Task RemoveEstablishmentAddressAsync(Guid establishmentId, Guid establishmentAddressId);
    }
}
