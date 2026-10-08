using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Provides read/write access to users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        Task CreateUserAsync(User user);

        /// <summary>
        /// Returns a user by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The matching user, or <c>null</c> if none found.</returns>
        Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Returns a user by its email address, or <c>null</c> if not found.
        /// </summary>
        /// <param name="address">The email address to search for.</param>
        /// <returns>The matching user, or <c>null</c> if none found.</returns>
        Task<User?> GetUserByEmailAsync(EmailAddress address);

        /// <summary>
        /// Returns all users in the system.
        /// </summary>
        /// <returns>All users in the system.</returns>
        Task<List<User>> GetUsersAsync();

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        Task UpdateUserAsync(User user);
    }
}
