using Domain.Abstractions.Repositories;
using Domain.Entities.Catalog;
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
        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return await context.Users
                .Include(u => u.Roles)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string address)
        {
            return await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.EmailAddress == address);
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
