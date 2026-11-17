using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to specifications.
    /// </summary>
    public interface ISpecificationRespository
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
        Task UpdateSpecificationAsync(Specification specification);

        /// <summary>
        /// Removes the specified specification.
        /// </summary>
        /// <param name="specification">The specification to remove.</param>
        Task RemoveSpecificationAsync(Specification specification);
    }
}
