using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Persistence.Postgres
{
    /// <summary>
    /// Provides read/write access to specifications.
    /// </summary>
    public interface ISpecificationRepository
    {
        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <param name="specification">The specification to create.</param>
        Task CreateSpecificationAsync(Specification specification);

        /// <summary>
        /// Returns a specification by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <returns>The matching specification, or <c>null</c> if none found.</returns>
        Task<Specification?> GetSpecificationByIdAsync(Guid specificationId);

        /// <summary>
        /// Returns all specifications in the system.
        /// </summary>
        /// <returns>All specifications in the system.</returns>
        Task<List<Specification>> GetSpecificationsAsync();

        /// <summary>
        /// Updates the specified specification.
        /// </summary>
        /// <param name="specification">The specification to update.</param>
        Task UpdateCategoryAsync(Specification specification);
    }
}
