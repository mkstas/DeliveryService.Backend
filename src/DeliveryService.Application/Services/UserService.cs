using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Repositories;
using DeliveryService.Domain.Interfaces.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Infrastructure.Common;

namespace DeliveryService.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IAuthorizationJwtProvider authorizationJwtProvider) : IUserService
    {
        public async Task CreateUserAsync(EmailAddress address, string password, StringBounded name)
        {
            var passwordHash = PasswordHasher.Generate(password);
            var user = User.Create(address, StringUnbounded.Create(passwordHash), name);

            await userRepository.CreateUserAsync(user);
        }

        public async Task<string> LoginUserAsync(EmailAddress address, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(address)
                ?? throw new UnauthorizedAccessException("Incorrect email address or password.");

            if (!PasswordHasher.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Incorrect email address or password.");
            }

            return authorizationJwtProvider.GenerateAccessToken(user.Id);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task UpdateUserEmailAsync(Guid userId, EmailAddress address)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            user.ChangeEmailAddress(address);

            await userRepository.UpdateUserAsync(user);
        }

        public async Task AddUserRoleAsync(Guid userId, Guid roleId)
        {
             var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);
             var role = await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);

             user.AddRole(role);

             await userRepository.UpdateUserAsync(user);
        }

        public async Task RemoveUserRoleAsync(Guid userId, Guid roleId)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);
            var role = await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);

            user.RemoveRole(role);

            await userRepository.UpdateUserAsync(user);
        }

        public async Task AddPaymentMethodAsync(Guid userId, CardNumber cardNumber)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            user.CreatePaymentMethod(cardNumber);

            await userRepository.UpdateUserAsync(user);
        }

        public async Task RemovePaymentMethodAsync(Guid userId, Guid paymentMethodId)
        {
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);
            var paymentMethod = user.PaymentMethods.FirstOrDefault(pm => pm.Id == paymentMethodId)
                ?? throw new NotFoundException<PaymentMethod>(paymentMethodId);

            user.RemovePaymentMethod(paymentMethod);

            await userRepository.UpdateUserAsync(user);
        }
    }
}
