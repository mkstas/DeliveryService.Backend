using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing specifications.
    /// </summary>
    public interface ISpecificationService
    {
        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <param name="name">The name of the specification to create.</param>
        Task CreateSpecificationAsync(StringBounded name);

        /// <summary>
        /// Returns a specification by its identifier.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <returns>The matching specification.</returns>
        Task<Specification> GetSpecificationByIdAsync(Guid specificationId);

        /// <summary>
        /// Returns all specifications in the system.
        /// </summary>
        /// <returns>All specifications in the system.</returns>
        Task<List<Specification>> GetSpecificationsAsync();

        /// <summary>
        /// Updates the name of the specified specification.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <param name="newName">The new name of the specification.</param>
        Task UpdateCpecificationAsync(Guid specificationId, StringBounded newName);
    }
}
