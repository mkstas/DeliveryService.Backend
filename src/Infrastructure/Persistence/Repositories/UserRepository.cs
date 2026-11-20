using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IUserRepository"/>.
    /// </summary>
    internal class UserRepository(PersistenceDbContext context) : IUserRepository
    {
        /// <inheritdoc />
        public async Task CreateUserAsync(User user)
        {
            context.Users.Add(user);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<User>(user.EmailAddress);
            }
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<User>> GetUsersAsync(string? search = null)
        {
            var query = context.Users
                .Include(u => u.Roles)
                .Include(u => u.Payments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var pattern = search.Trim().ToLower();
                query = query.Where(u => u.EmailAddress.ToLower().Contains(pattern));
            }

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.Payments)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string address)
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.Payments)
                .FirstOrDefaultAsync(u => u.EmailAddress == address);
        }

        /// <inheritdoc />
        public async Task<bool> PaymentCardExistsAsync(string cardNumber)
        {
            return await context.Users.AnyAsync(u => u.Payments.Any(p => p.CardNumber == cardNumber));
        }

        /// <inheritdoc />
        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<User>(user.EmailAddress);
            }
        }
    }
}

