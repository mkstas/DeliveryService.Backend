using Application.Abstractions.Auth;
using Application.Abstractions.Cache;
using Application.Abstractions.Email;
using Application.Abstractions.Services;
using Application.Common;
using Application.DTOs.Users;
using Domain.Abstractions.Repositories;
using Domain.Entities.Catalog;
using Domain.Exceptions;
using Domain.Permissions;

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
        IAuthorizationJwtService authorizationJwtService) : IUserService
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
            var clientRole = await roleRepository.GetRoleByNameAsync(Roles.Client)
                ?? throw new InvalidOperationException("Cannot create user.");

            var passwordHash = PasswordHasher.Generate(request.Password);
            var code = VerificationCode.Generate();
            var user = User.Create(request.EmailAddress, passwordHash, request.Name);

            user.AddRole(clientRole);

            await cacheService.SetAsync(CacheKeys.Users.Pending(user.EmailAddress), user, TimeSpan.FromHours(2));
            await cacheService.SetAsync(CacheKeys.Users.VerificationCode(user.EmailAddress), code, TimeSpan.FromMinutes(10));

            await emailService.SendEmailAsync(
                user.EmailAddress,
                "Подтверждение элекронной почты Delivery Service.",
                $"Код подтверждения: {code}. Код действителен в течение {TimeSpan.FromMinutes(10).TotalMinutes} минут.");
        }

        /// <inheritdoc />
        public async Task ConfirmUserEmailAsync(ConfirmEmailRequest request)
        {
            var user = await cacheService.GetAsync<User>(CacheKeys.Users.Pending(request.EmailAddress))
                ?? throw new InvalidOperationException("Cannot to confirm email address");

            var cachedCode = await cacheService.GetAsync<string>(CacheKeys.Users.VerificationCode(request.EmailAddress))
                ?? throw new InvalidOperationException("Cannot to confirm email address");

            if (cachedCode != request.Code)
                throw new UnauthorizedAccessException("Invalid or expired verification code.");

            await userRepository.CreateUserAsync(user);
            await cacheService.RemoveByPrefixAsync(CacheKeys.Users.Pending(user.EmailAddress));
            await cacheService.RemoveByPrefixAsync(CacheKeys.Users.VerificationCode(user.EmailAddress));
        }

        /// <inheritdoc />
        public async Task ResendConfirmUserEmailAsync(ResendConfirmEmailRequest request)
        {
            var code = VerificationCode.Generate();

            await cacheService.SetAsync(CacheKeys.Users.VerificationCode(request.EmailAddress), code, TimeSpan.FromMinutes(10));

            await emailService.SendEmailAsync(
                request.EmailAddress,
                "Подтверждение элекронной почты Delivery Service.",
                $"Код подтверждения: {code}. Код действителен в течение {TimeSpan.FromMinutes(10).TotalMinutes} минут.");
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserResponse>> GetUsersAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<UserResponse>>(CacheKeys.Users.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var users = await userRepository.GetUsersAsync();

            IReadOnlyList<UserResponse> response = [.. users.Select(u => new UserResponse(
                u.Id, u.EmailAddress, u.Name, u.ImageUrl,
                [.. u.Roles.Select(r => new UserRoleResponse(r.Id, r.Name))]))];

            await cacheService.SetAsync(CacheKeys.Users.All, response, TimeSpan.FromDays(1));

            return response;
        }

        /// <inheritdoc />
        public async Task<UserResponse> GetUserByIdAsync(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            var response = new UserResponse(user.Id, user.EmailAddress, user.Name, user.ImageUrl,
                [.. user.Roles.Select(r => new UserRoleResponse(r.Id, r.Name))]);

            return response;
        }

        /// <inheritdoc />
        public async Task UpdateUserAsync(Guid userId, UpdateUserRequest request)
        {
            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            user.ChangeName(request.Name);

            await userRepository.UpdateUserAsync(user);
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
        }
    }
}
