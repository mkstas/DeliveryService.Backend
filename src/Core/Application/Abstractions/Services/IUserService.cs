using Application.DTOs.Users;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user and returns an access token.
        /// </summary>
        /// <param name="request">The request to log in user.</param>
        /// <returns>The generated access token.</returns>
        Task<string> LoginUserAsync(LoginUserRequest request);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        Task CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Confirms the user email address.
        /// </summary>
        /// <param name="request">The request to confirm email.</param>
        Task ConfirmUserEmailAsync(ConfirmEmailRequest request);

        /// <summary>
        /// Resends the confirmation user email address.
        /// </summary>
        /// <param name="request">The request to resend confirmation.</param>
        Task ResendConfirmUserEmailAsync(ResendConfirmEmailRequest request);

        /// <summary>
        /// Returns all users in the system.
        /// </summary>
        /// <returns>All users in the system.</returns>
        Task<IReadOnlyList<UserResponse>> GetUsersAsync();

        /// <summary>
        /// Returns a user by its identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The matching user.</returns>
        Task<UserResponse> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to update user.</param>
        Task UpdateUserAsync(Guid userId, UpdateUserRequest request);

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
    }
}
