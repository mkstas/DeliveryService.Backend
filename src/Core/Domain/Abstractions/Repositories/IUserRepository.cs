using Domain.Entities;

namespace Domain.Abstractions.Repositories
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
        /// Returns users in the system, optionally filtered by email address.
        /// </summary>
        /// <param name="search">Optional substring to match against the email address.</param>
        /// <returns>The matching users.</returns>
        Task<IReadOnlyList<User>> GetUsersAsync(string? search = null);

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
        Task<User?> GetUserByEmailAsync(string address);

        /// <summary>
        /// Returns whether the specified card number is already used.
        /// </summary>
        /// <param name="cardNumber">The card number to search for.</param>
        /// <returns><c>true</c> if the card number is already used; otherwise, <c>false</c>.</returns>
        Task<bool> PaymentCardExistsAsync(string cardNumber);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        Task UpdateUserAsync(User user);
    }
}
