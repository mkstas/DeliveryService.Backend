using Application.Abstractions.Auth;
using Application.Abstractions.Cache;
using Application.Abstractions.Email;
using Application.Abstractions.Services;
using Application.Abstractions.Storage;
using Application.Common;
using Application.DTOs.Users;
using Domain.Abstractions.Permissions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="IUserService"/>.
    /// </summary>
    internal class UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ICacheService cacheService,
        IEmailService emailService,
        IJwtService authorizationJwtService,
        IStorageService storageService) : IUserService
    {
        /// <inheritdoc />
        public async Task<string> LoginUserAsync(LoginUserRequest request)
        {
            var user = await userRepository.GetUserByEmailAsync(request.EmailAddress)
                ?? throw new UnauthorizedAccessException("Incorrect email address or password.");

            if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Incorrect email address or password.");

            return authorizationJwtService.GenerateAccessToken(user.Id);
        }

        /// <inheritdoc />
        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var clientRole = await roleRepository.GetRoleByNameAsync(RoleNames.Client)
                ?? throw new InvalidOperationException("Cannot create user.");

            var pendingUser = new PendingUser(
                request.EmailAddress,
                PasswordHasher.Generate(request.Password),
                request.Name,
                [clientRole.Id]);

            var code = VerificationCode.Generate();

            await cacheService.SetAsync(CacheKeys.Users.Pending(request.EmailAddress), pendingUser, TimeSpan.FromHours(2));
            await cacheService.SetAsync(CacheKeys.Users.VerificationCode(request.EmailAddress), code, TimeSpan.FromMinutes(10));

            await emailService.SendEmailAsync(
                request.EmailAddress,
                "Подтверждение электронной почты Delivery Service.",
                $"Код подтверждения: {code}. Код действителен в течение {TimeSpan.FromMinutes(10).TotalMinutes} минут.");
        }

        /// <inheritdoc />
        public async Task CreateUserByAdminAsync(CreateUserRequest request)
        {
            var clientRole = await roleRepository.GetRoleByNameAsync(RoleNames.Client)
                ?? throw new InvalidOperationException("Cannot create user.");

            var user = User.Create(request.EmailAddress, PasswordHasher.Generate(request.Password), request.Name);
            user.AddRole(clientRole);

            await userRepository.CreateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task ConfirmUserEmailAsync(ConfirmEmailRequest request)
        {
            var pendingUser = await cacheService.GetAsync<PendingUser>(CacheKeys.Users.Pending(request.EmailAddress))
                ?? throw new InvalidOperationException("Cannot to confirm email address");

            var cachedCode = await cacheService.GetAsync<string>(CacheKeys.Users.VerificationCode(request.EmailAddress))
                ?? throw new InvalidOperationException("Cannot to confirm email address");

            if (cachedCode != request.Code)
                throw new UnauthorizedAccessException("Invalid or expired verification code.");

            var user = User.Create(pendingUser.EmailAddress, pendingUser.PasswordHash, pendingUser.Name);

            foreach (var roleId in pendingUser.RoleIds)
            {
                var role = await roleRepository.GetRoleByIdAsync(roleId)
                    ?? throw new InvalidOperationException("Cannot to confirm email address");

                user.AddRole(role);
            }

            await userRepository.CreateUserAsync(user);

            await cacheService.RemoveAsync(CacheKeys.Users.Pending(request.EmailAddress));
            await cacheService.RemoveAsync(CacheKeys.Users.VerificationCode(request.EmailAddress));
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task ResendConfirmUserEmailAsync(ResendConfirmEmailRequest request)
        {
            _ = await cacheService.GetAsync<PendingUser>(CacheKeys.Users.Pending(request.EmailAddress))
                ?? throw new InvalidOperationException("Cannot to resend email confirmation.");

            var code = VerificationCode.Generate();

            await cacheService.SetAsync(CacheKeys.Users.VerificationCode(request.EmailAddress), code, TimeSpan.FromMinutes(10));

            await emailService.SendEmailAsync(
                request.EmailAddress,
                "Подтверждение электронной почты Delivery Service.",
                $"Код подтверждения: {code}. Код действителен в течение {TimeSpan.FromMinutes(10).TotalMinutes} минут.");
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserResponse>> GetUsersAsync(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var cachedResponse = await cacheService.GetAsync<IReadOnlyList<UserResponse>>(CacheKeys.Users.All);

                if (cachedResponse is not null)
                    return cachedResponse;

                var users = await userRepository.GetUsersAsync();

                IReadOnlyList<UserResponse> response = [.. users.Select(MapToResponse)];

                await cacheService.SetAsync(CacheKeys.Users.All, response, TimeSpan.FromDays(1));

                return response;
            }

            var filteredUsers = await userRepository.GetUsersAsync(search);

            return [.. filteredUsers.Select(MapToResponse)];
        }

        private static UserResponse MapToResponse(User user)
        {
            return new UserResponse(user.Id, user.EmailAddress, user.Name, user.ImageUrl, user.RestaurantId,
                [.. user.Roles.Select(r => new UserRoleResponse(r.Id, r.Name))],
                [.. user.Payments.Select(p => new PaymentResponse(p.Id, p.CardNumber))]);
        }

        /// <inheritdoc />
        public async Task<UserResponse> GetUserByIdAsync(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var response = new UserResponse(user.Id, user.EmailAddress, user.Name, user.ImageUrl, user.RestaurantId,
                [.. user.Roles.Select(r => new UserRoleResponse(r.Id, r.Name))],
                [.. user.Payments.Select(p => new PaymentResponse(p.Id, p.CardNumber))]);

            return response;
        }

        /// <inheritdoc />
        public async Task UpdateUserAsync(Guid userId, UpdateUserRequest request)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            user.ChangeName(request.Name);

            await userRepository.UpdateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task AddUserRoleAsync(Guid userId, Guid roleId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            user.AddRole(role);

            await userRepository.UpdateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task RemoveUserRoleAsync(Guid userId, Guid roleId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            user.RemoveRole(role);

            await userRepository.UpdateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task AddPaymentAsync(Guid userId, AddPaymentRequest request)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            if (await userRepository.PaymentCardExistsAsync(request.CardNumber))
                throw new ConflictException<Payment>(request.CardNumber);

            user.AddPayment(request.CardNumber);

            await userRepository.UpdateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task RemovePaymentAsync(Guid userId, Guid paymentId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var payment = user.Payments.FirstOrDefault(pm => pm.Id == paymentId)
                ?? throw new NotFoundException<Payment>(paymentId);

            user.RemovePayment(payment);

            await userRepository.UpdateUserAsync(user);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task<string> UploadUserImageAsync(Guid userId, Stream stream, string contentType, string extension)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var newImageName = $"{Guid.NewGuid()}{extension}";
            var previousImageName = user.ImageUrl is null ? null : user.ImageUrl;

            await storageService.UploadFileAsync(newImageName, stream, contentType);

            user.ChangeImageUrl(newImageName);

            await userRepository.UpdateUserAsync(user);

            if (previousImageName is not null && previousImageName != newImageName)
            {
                await storageService.DeleteFileAsync(previousImageName);
            }

            await cacheService.RemoveAsync(CacheKeys.Users.All);

            return newImageName;
        }

        /// <inheritdoc />
        public async Task<(Stream Stream, string ContentType)> GetUserImageAsync(string imageName)
        {
            return await storageService.DownloadFileAsync(imageName);
        }

        /// <inheritdoc />
        public async Task RemoveUserImageAsync(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            if (user.ImageUrl is null)
            {
                return;
            }

            var imageKey = user.ImageUrl;

            user.ChangeImageUrl(null);

            await userRepository.UpdateUserAsync(user);
            await storageService.DeleteFileAsync(imageKey);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }


    }
}
