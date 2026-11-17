using Application.DTOs.DishSpecifications;

namespace Application.Abstractions.Services
{
    public interface IDishSpecificationService
    {
        Task CreateDishSpecificationAsync(CreateDishSpecificationRequest request);

        Task<IReadOnlyList<DishSpecificationResponse>> GetDishSpecificationsAsync();

        Task<DishSpecificationResponse> GetDishSpecificationByIdAsync(Guid dishSpecificationId);

        Task UpdateDishSpecificationAsync(Guid dishSpecificationId, UpdateDishSpecificationRequest request);

        Task RemoveDishSpecificationAsync(Guid dishSpecificationId);
    }
}
