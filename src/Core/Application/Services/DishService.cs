using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Dishes;
using Application.DTOs.DishSpecifications;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    internal class DishService(
        IDishRepository dishRepository,
        IRestaurantRepository restaurantRepository,
        ICategoryRespository categoryRespository,
        ICacheService cacheService) : IDishService
    {
        public async Task CreateDishAsync(CreateDishRequest request)
        {
            _ = await restaurantRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException<Restaurant>(request.RestaurantId);

            _ = await categoryRespository.GetCategoryByIdAsync(request.CategoryId)
                ?? throw new NotFoundException<Category>(request.CategoryId);

            await dishRepository.CreateDishAsync(Dish.Create(
                request.RestaurantId,
                request.CategoryId,
                request.Name,
                request.Price,
                request.Description,
                request.ImageUrl));

            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        public async Task<IReadOnlyList<DishResponse>> GetDishesAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<DishResponse>>(CacheKeys.Dishes.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var dishes = await dishRepository.GetDishesAsync();

            IReadOnlyList<DishResponse> response = [.. dishes.Select(MapToDishResponse)];

            await cacheService.SetAsync(CacheKeys.Dishes.All, response, TimeSpan.FromDays(14));

            return response;
        }

        public async Task<DishResponse> GetDishByIdAsync(Guid dishId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            return MapToDishResponse(dish);
        }

        public async Task UpdateDishAsync(Guid dishId, UpdateDishRequest request)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            dish.ChangeName(request.Name);
            dish.ChangePrice(request.Price);
            dish.ChangeDescription(request.Description);
            dish.ChangeImageUrl(request.ImageUrl);

            await dishRepository.UpdateDishAsync(dish);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        public async Task RemoveDishAsync(Guid dishId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            await dishRepository.RemoveDishAsync(dish);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        private static DishResponse MapToDishResponse(Dish dish)
        {
            return new DishResponse(
                dish.Id,
                dish.RestaurantId,
                dish.CategoryId,
                dish.Name,
                dish.Price,
                dish.Description,
                dish.ImageUrl,
                [.. dish.Specifications.Select(MapToDishSpecificationResponse)]);
        }

        private static DishSpecificationResponse MapToDishSpecificationResponse(DishSpecification specification)
        {
            return new DishSpecificationResponse(
                specification.Id,
                specification.DishId,
                specification.SpecificationId,
                specification.Value);
        }
    }
}
