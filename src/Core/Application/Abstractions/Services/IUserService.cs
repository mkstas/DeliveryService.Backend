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

        /// <summary>
        /// Uploads and assigns an image to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="stream">The image content stream.</param>
        /// <param name="contentType">The content type of the image.</param>
        /// <param name="extension">The file extension of the image, including the leading dot.</param>
        /// <returns>The name of the uploaded image.</returns>
        Task<string> UploadUserImageAsync(Guid userId, Stream stream, string contentType, string extension);

        /// <summary>
        /// Returns an image by its name.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>The image content stream and its content type.</returns>
        Task<(Stream Stream, string ContentType)> GetUserImageAsync(string imageName);

        /// <summary>
        /// Removes the image of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task RemoveUserImageAsync(Guid userId);
    }
}
