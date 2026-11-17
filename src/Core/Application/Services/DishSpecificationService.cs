using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.DishSpecifications;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    internal class DishSpecificationService(
        IDishSpecificationRepository dishSpecificationRepository,
        IDishRepository dishRepository,
        ISpecificationRespository specificationRespository,
        ICacheService cacheService) : IDishSpecificationService
    {
        public async Task CreateDishSpecificationAsync(CreateDishSpecificationRequest request)
        {
            _ = await dishRepository.GetDishByIdAsync(request.DishId)
                ?? throw new NotFoundException<Dish>(request.DishId);

            _ = await specificationRespository.GetSpecificationByIdAsync(request.SpecificationId)
                ?? throw new NotFoundException<Specification>(request.SpecificationId);

            await dishSpecificationRepository.CreateDishSpecificationAsync(
                DishSpecification.Create(request.DishId, request.SpecificationId, request.Value));

            await cacheService.RemoveAsync(CacheKeys.DishSpecifications.All);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        public async Task<IReadOnlyList<DishSpecificationResponse>> GetDishSpecificationsAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<DishSpecificationResponse>>(CacheKeys.DishSpecifications.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var specifications = await dishSpecificationRepository.GetDishSpecificationsAsync();

            IReadOnlyList<DishSpecificationResponse> response = [.. specifications.Select(MapToResponse)];

            await cacheService.SetAsync(CacheKeys.DishSpecifications.All, response, TimeSpan.FromDays(14));

            return response;
        }

        public async Task<DishSpecificationResponse> GetDishSpecificationByIdAsync(Guid dishSpecificationId)
        {
            var specification = await dishSpecificationRepository.GetDishSpecificationByIdAsync(dishSpecificationId)
                ?? throw new NotFoundException<DishSpecification>(dishSpecificationId);

            return MapToResponse(specification);
        }

        public async Task UpdateDishSpecificationAsync(Guid dishSpecificationId, UpdateDishSpecificationRequest request)
        {
            var specification = await dishSpecificationRepository.GetDishSpecificationByIdAsync(dishSpecificationId)
                ?? throw new NotFoundException<DishSpecification>(dishSpecificationId);

            specification.ChangeValue(request.Value);

            await dishSpecificationRepository.UpdateDishSpecificationAsync(specification);
            await cacheService.RemoveAsync(CacheKeys.DishSpecifications.All);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        public async Task RemoveDishSpecificationAsync(Guid dishSpecificationId)
        {
            var specification = await dishSpecificationRepository.GetDishSpecificationByIdAsync(dishSpecificationId)
                ?? throw new NotFoundException<DishSpecification>(dishSpecificationId);

            await dishSpecificationRepository.RemoveDishSpecificationAsync(specification);
            await cacheService.RemoveAsync(CacheKeys.DishSpecifications.All);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        private static DishSpecificationResponse MapToResponse(DishSpecification specification)
        {
            return new DishSpecificationResponse(
                specification.Id,
                specification.DishId,
                specification.SpecificationId,
                specification.Value);
        }
    }
}
