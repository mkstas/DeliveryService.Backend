using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IDishSpecificationRepository
    {
        Task CreateDishSpecificationAsync(DishSpecification dishSpecification);

        Task<DishSpecification?> GetDishSpecificationByIdAsync(Guid dishSpecificationId);

        Task<List<DishSpecification>> GetDishSpecificationsAsync();

        Task UpdateDishSpecificationAsync(DishSpecification dishSpecification);

        Task RemoveDishSpecificationAsync(DishSpecification dishSpecification);
    }
}
