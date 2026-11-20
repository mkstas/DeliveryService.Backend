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
        /// Returns specifications in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the specification name.</param>
        /// <returns>The matching specifications.</returns>
        Task<List<Specification>> GetSpecificationsAsync(string? search = null);

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
