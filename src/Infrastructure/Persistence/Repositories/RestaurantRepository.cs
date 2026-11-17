using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IRestaurantRepository"/>.
    /// </summary>
    internal class RestaurantRepository(PersistenceDbContext context) : IRestaurantRepository
    {
        /// <inheritdoc />
        public async Task CreateRestaurantAsync(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Restaurant>(restaurant.Name);
            }
        }

        /// <inheritdoc />
        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await context.Restaurants
                .Include(r => r.Addresses)
                .Include(r => r.Users)
                .Include(r => r.Ingredients)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Restaurant?> GetRestaurantByIdAsync(Guid restaurantId)
        {
            return await context.Restaurants
                .Include(r => r.Addresses)
                .Include(r => r.Users)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);
        }

        /// <inheritdoc />
        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Restaurant>(restaurant.Name);
            }
        }
    }
}

