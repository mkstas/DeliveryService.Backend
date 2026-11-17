using Application.DTOs.Specifications;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing specifications.
    /// </summary>
    public interface ISpecificationService
    {
        /// <summary>
        /// Creates a new specification.
        /// </summary>
        Task CreateSpecificationAsync(CreateSpecificationRequest request);

        /// <summary>
        /// Returns all specifications in the system.
        /// </summary>
        /// <returns>All specifications in the system.</returns>
        Task<IReadOnlyList<SpecificationResponse>> GetSpecificationsAsync();

        /// <summary>
        /// Returns a specification by its identifier.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <returns>The matching specification.</returns>
        Task<SpecificationResponse> GetSpecificationByIdAsync(Guid specificationId);

        /// <summary>
        /// Updates the name of the specified specification.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        /// <param name="request">The request to update specification.</param>
        Task UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationRequest request);

        /// <summary>
        /// Remove the specified specification from the system.
        /// </summary>
        /// <param name="specificationId">The specification identifier.</param>
        Task RemoveSpecificationAsync(Guid specificationId);
    }
}
