using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates the default administrator account if it does not already exist.
        /// </summary>
        /// <param name="address">The administrator's email address.</param>
        /// <param name="password">The administrator's password.</param>
        /// <param name="name">The administrator's name.</param>
        Task EnsureAdministratorAsync(EmailAddress address, string password, StringBounded name);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="address">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        Task CreateUserAsync(EmailAddress address, string password, StringBounded name);

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
        /// Updates the email address of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="address">The new email address.</param>
        Task UpdateUserEmailAsync(Guid userId, EmailAddress address);

        /// <summary>
        /// Updates the name of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newName">The new name.</param>
        Task UpdateUserBioAsync(Guid userId, StringBounded newName);

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
