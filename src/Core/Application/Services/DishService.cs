using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.Abstractions.Storage;
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
        IIngredientRepository ingredientRepository,
        ICacheService cacheService,
        IStorageService storageService) : IDishService
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

        public async Task<IReadOnlyList<DishResponse>> GetDishesAsync(DishFilterQuery? filter = null)
        {
            filter ??= new DishFilterQuery();

            if (filter.IsEmpty)
            {
                var cachedResponse = await cacheService.GetAsync<IReadOnlyList<DishResponse>>(CacheKeys.Dishes.All);

                if (cachedResponse is not null)
                    return cachedResponse;

                var dishes = await dishRepository.GetDishesAsync();

                IReadOnlyList<DishResponse> response = [.. dishes.Select(MapToDishResponse)];

                await cacheService.SetAsync(CacheKeys.Dishes.All, response, TimeSpan.FromDays(14));

                return response;
            }

            var restaurantIds = filter.RestaurantIds?.Where(id => id != Guid.Empty).Distinct().ToList();

            var filteredDishes = await dishRepository.GetDishesAsync(
                filter.Search,
                categoryId: filter.CategoryId,
                minPrice: filter.MinPrice,
                maxPrice: filter.MaxPrice,
                restaurantIds: restaurantIds);

            return [.. filteredDishes.Select(MapToDishResponse)];
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DishResponse>> GetDishesByRestaurantAsync(Guid restaurantId, string? search = null)
        {
            _ = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var dishes = await dishRepository.GetDishesAsync(search, restaurantId);

            return [.. dishes.Select(MapToDishResponse)];
        }

        /// <inheritdoc />
        public async Task<DishResponse> CreateUserDishAsync(Guid userId, CreateDishRequest request)
        {
            var restaurant = await restaurantRepository.GetUserRestaurantAsync(userId)
                ?? throw new NotFoundException<Restaurant>(userId);

            _ = await categoryRespository.GetCategoryByIdAsync(request.CategoryId)
                ?? throw new NotFoundException<Category>(request.CategoryId);

            var dish = Dish.Create(
                restaurant.Id,
                request.CategoryId,
                request.Name,
                request.Price,
                request.Description,
                request.ImageUrl);

            await dishRepository.CreateDishAsync(dish);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);

            return MapToDishResponse(dish);
        }

        /// <inheritdoc />
        public async Task<Guid> EnsureUserOwnsDishAsync(Guid userId, Guid dishId)
        {
            var restaurant = await restaurantRepository.GetUserRestaurantAsync(userId)
                ?? throw new NotFoundException<Restaurant>(userId);

            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            if (dish.RestaurantId != restaurant.Id)
                throw new NotFoundException<Dish>(dishId);

            return dish.Id;
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
                [.. dish.Specifications.Select(MapToDishSpecificationResponse)],
                [.. dish.Ingredients.Select(MapToDishIngredientResponse)]);
        }

        private static DishIngredientResponse MapToDishIngredientResponse(DishIngredient dishIngredient)
        {
            return new DishIngredientResponse(
                dishIngredient.Id,
                dishIngredient.IngredientId,
                dishIngredient.Ingredient.Name);
        }

        private static DishSpecificationResponse MapToDishSpecificationResponse(DishSpecification specification)
        {
            return new DishSpecificationResponse(
                specification.Id,
                specification.DishId,
                specification.SpecificationId,
                specification.Value);
        }

        /// <inheritdoc />
        public async Task<string> UploadDishImageAsync(Guid dishId, Stream stream, string contentType, string extension)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            var newImageName = $"dish-{Guid.NewGuid()}{extension}";
            var previousImageName = dish.ImageUrl;

            await storageService.UploadFileAsync(newImageName, stream, contentType);

            dish.ChangeImageUrl(newImageName);

            await dishRepository.UpdateDishAsync(dish);

            if (previousImageName is not null && previousImageName != newImageName)
            {
                await storageService.DeleteFileAsync(previousImageName);
            }

            await cacheService.RemoveAsync(CacheKeys.Dishes.All);

            return newImageName;
        }

        /// <inheritdoc />
        public async Task<(Stream Stream, string ContentType)> GetDishImageAsync(string imageName)
        {
            return await storageService.DownloadFileAsync(imageName);
        }

        /// <inheritdoc />
        public async Task RemoveDishImageAsync(Guid dishId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            if (dish.ImageUrl is null)
            {
                return;
            }

            var imageKey = dish.ImageUrl;

            dish.ChangeImageUrl(null);

            await dishRepository.UpdateDishAsync(dish);
            await storageService.DeleteFileAsync(imageKey);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        /// <inheritdoc />
        public async Task AddDishIngredientAsync(Guid dishId, AddDishIngredientRequest request)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            await AddIngredientToDishAsync(dish, request);
        }

        /// <inheritdoc />
        public async Task RemoveDishIngredientAsync(Guid dishId, Guid dishIngredientId)
        {
            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            await RemoveIngredientFromDishAsync(dish, dishIngredientId);
        }

        /// <inheritdoc />
        public async Task AddUserDishIngredientAsync(Guid userId, Guid dishId, AddDishIngredientRequest request)
        {
            var dish = await GetUserDishAsync(userId, dishId);

            await AddIngredientToDishAsync(dish, request);
        }

        /// <inheritdoc />
        public async Task RemoveUserDishIngredientAsync(Guid userId, Guid dishId, Guid dishIngredientId)
        {
            var dish = await GetUserDishAsync(userId, dishId);

            await RemoveIngredientFromDishAsync(dish, dishIngredientId);
        }

        private async Task<Dish> GetUserDishAsync(Guid userId, Guid dishId)
        {
            var restaurant = await restaurantRepository.GetUserRestaurantAsync(userId)
                ?? throw new NotFoundException<Restaurant>(userId);

            var dish = await dishRepository.GetDishByIdAsync(dishId)
                ?? throw new NotFoundException<Dish>(dishId);

            if (dish.RestaurantId != restaurant.Id)
                throw new NotFoundException<Dish>(dishId);

            return dish;
        }

        private async Task AddIngredientToDishAsync(Dish dish, AddDishIngredientRequest request)
        {
            var ingredient = await ingredientRepository.GetByIdAsync(request.IngredientId)
                ?? throw new NotFoundException<Ingredient>(request.IngredientId);

            // The ingredient must belong to the restaurant that owns the dish.
            if (ingredient.RestaurantId != dish.RestaurantId)
                throw new NotFoundException<Ingredient>(request.IngredientId);

            if (dish.Ingredients.Any(di => di.IngredientId == ingredient.Id))
                throw new ConflictException<DishIngredient>(ingredient.Name);

            await dishRepository.AddDishIngredientAsync(DishIngredient.Create(dish.Id, ingredient.Id));
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }

        private async Task RemoveIngredientFromDishAsync(Dish dish, Guid dishIngredientId)
        {
            var dishIngredient = dish.Ingredients.FirstOrDefault(di => di.Id == dishIngredientId)
                ?? throw new NotFoundException<DishIngredient>(dishIngredientId);

            await dishRepository.RemoveDishIngredientAsync(dishIngredient);
            await cacheService.RemoveAsync(CacheKeys.Dishes.All);
        }
    }
}
