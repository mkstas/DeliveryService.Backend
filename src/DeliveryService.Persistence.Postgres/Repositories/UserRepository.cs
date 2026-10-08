using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Repositories;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class UserRepository(DeliveryServiceDbContext context) : IUserRepository
    {
        public async Task CreateUserAsync(User user)
        {
            context.Users.Add(user);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<User>(user.EmailAddress);
            }
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.PaymentMethods)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByEmailAsync(EmailAddress address)
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.PaymentMethods)
                .FirstOrDefaultAsync(u => u.EmailAddress == address);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.PaymentMethods)
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<User>(user.EmailAddress);
            }
        }
    }
}
