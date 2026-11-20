using Application.DTOs.DishSpecifications;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing dish specifications.
    /// </summary>
    public interface IDishSpecificationService
    {
        /// <summary>
        /// Creates a new dish specification.
        /// </summary>
        /// <param name="request">The request to create dish specification.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateDishSpecificationAsync(CreateDishSpecificationRequest request);

        /// <summary>
        /// Returns all dish specifications in the system.
        /// </summary>
        /// <returns>All dish specifications in the system.</returns>
        Task<IReadOnlyList<DishSpecificationResponse>> GetDishSpecificationsAsync();

        /// <summary>
        /// Creates a new dish specification for a dish of the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to create dish specification.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateUserDishSpecificationAsync(Guid userId, CreateDishSpecificationRequest request);

        /// <summary>
        /// Updates a dish specification belonging to a dish of the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <param name="request">The request to update dish specification.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateUserDishSpecificationAsync(Guid userId, Guid dishSpecificationId, UpdateDishSpecificationRequest request);

        /// <summary>
        /// Removes a dish specification belonging to a dish of the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveUserDishSpecificationAsync(Guid userId, Guid dishSpecificationId);

        /// <summary>
        /// Returns a dish specification by its identifier.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>The matching dish specification.</returns>
        Task<DishSpecificationResponse> GetDishSpecificationByIdAsync(Guid dishSpecificationId);

        /// <summary>
        /// Updates the specified dish specification.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <param name="request">The request to update dish specification.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateDishSpecificationAsync(Guid dishSpecificationId, UpdateDishSpecificationRequest request);

        /// <summary>
        /// Remove the specified dish specification from the system.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveDishSpecificationAsync(Guid dishSpecificationId);
    }
}
