using Application.DTOs.Dishes;

namespace Application.Abstractions.Services
{
    public interface IDishService
    {
        Task CreateDishAsync(CreateDishRequest request);

        Task<IReadOnlyList<DishResponse>> GetDishesAsync();

        Task<DishResponse> GetDishByIdAsync(Guid dishId);

        Task UpdateDishAsync(Guid dishId, UpdateDishRequest request);

        Task RemoveDishAsync(Guid dishId);
    }
}
