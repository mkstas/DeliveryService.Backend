using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides operations for managing users.
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="address">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        Task CreateUserAsync(EmailAddress address, string password);

        /// <summary>
        /// Authenticates a user and returns an access token.
        /// </summary>
        /// <param name="address">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The generated access token.</returns>
        Task<string> LoginUserAsync(EmailAddress address, string password);

        /// <summary>
        /// Returns a user by its identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The matching user.</returns>
        Task<User> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Returns all users in the system.
        /// </summary>
        /// <returns>All users in the system.</returns>
        Task<List<User>> GetUsersAsync();

        /// <summary>
        /// Assigns a role to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        Task AddUserRoleAsync(Guid userId, Guid roleId);

        /// <summary>
        /// Removes a role from the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        Task RemoveUserRoleAsync(Guid userId, Guid roleId);

        /// <summary>
        /// Adds a payment method to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cardNumber">The card number to add.</param>
        Task AddPaymentMethodAsync(Guid userId, CardNumber cardNumber);

        /// <summary>
        /// Removes a payment method from the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="paymentMethodId">The payment method identifier.</param>
        Task RemovePaymentMethodAsync(Guid userId, Guid paymentMethodId);
    }
}
