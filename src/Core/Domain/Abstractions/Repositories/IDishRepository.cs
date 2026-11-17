using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IDishRepository
    {
        Task CreateDishAsync(Dish dish);

        Task<Dish?> GetDishByIdAsync(Guid dishId);

        Task<List<Dish>> GetDishesAsync();

        Task UpdateDishAsync(Dish dish);

        Task RemoveDishAsync(Dish dish);
    }
}
