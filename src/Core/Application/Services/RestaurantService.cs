using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.Abstractions.Storage;
using Application.DTOs.Restaurants;
using Domain.Abstractions.Permissions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="IRestaurantService"/>.
    /// </summary>
    internal class RestaurantService(
        IRestaurantRepository restaurantRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ICacheService cacheService,
        IStorageService storageService) : IRestaurantService
    {
        /// <inheritdoc />
        public async Task CreateRestaurantAsync(CreateRestaurantRequest request)
        {
            await restaurantRepository.CreateRestaurantAsync(Restaurant.Create(request.Name, request.ImageUrl));
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<RestaurantResponse>> GetRestaurantsAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<RestaurantResponse>>(CacheKeys.Restaurants.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var restaurants = await restaurantRepository.GetRestaurantsAsync();

            IReadOnlyList<RestaurantResponse> response = [.. restaurants.Select(MapToRestaurantResponse)];

            await cacheService.SetAsync(CacheKeys.Restaurants.All, response, TimeSpan.FromHours(12));

            return response;
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            return MapToRestaurantResponse(restaurant);
        }

        /// <inheritdoc />
        public async Task UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            restaurant.ChangeName(request.Name);
            restaurant.ChangeImageUrl(request.ImageUrl);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task AddRestaurantUserAsync(Guid restaurantId, AddRestaurantUserRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var user = await userRepository.GetUserByEmailAsync(request.EmailAddress)
                ?? throw new ConflictException<User>(request.EmailAddress);

            if (user.RestaurantId is not null && user.RestaurantId != restaurant.Id)
                throw new ConflictException<User>(request.EmailAddress);

            var managerRole = await roleRepository.GetRoleByNameAsync(RoleNames.Manager)
                ?? throw new InvalidOperationException("Cannot assign manager role.");

            if (!user.Roles.Any(r => r.Id == managerRole.Id))
                user.AddRole(managerRole);

            restaurant.AddUser(user);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await userRepository.UpdateUserAsync(user);

            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task RemoveRestaurantUserAsync(Guid restaurantId, Guid userId)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var user = await userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException<User>(userId);

            if (user.RestaurantId != restaurant.Id)
                throw new NotFoundException<User>(userId);

            var managerRole = await roleRepository.GetRoleByNameAsync(RoleNames.Manager);
            if (managerRole is not null)
            {
                var assignedRole = user.Roles.FirstOrDefault(r => r.Id == managerRole.Id);
                if (assignedRole is not null)
                    user.RemoveRole(assignedRole);
            }

            restaurant.RemoveUser(user);

            await userRepository.UpdateUserAsync(user);

            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
            await cacheService.RemoveAsync(CacheKeys.Users.All);
        }

        /// <inheritdoc />
        public async Task AddRestaurantAddressAsync(Guid restaurantId, AddRestaurantAddressRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            restaurant.AddAddress(request.Address);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task RemoveRestaurantAddressAsync(Guid restaurantId, Guid addressId)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var address = restaurant.Addresses.FirstOrDefault(a => a.Id == addressId)
                ?? throw new NotFoundException<RestaurantAddress>(addressId);

            restaurant.RemoveAddress(address);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task UpdateRestaurantAddressAsync(Guid restaurantId, Guid addressId, UpdateRestaurantAddressRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var address = restaurant.Addresses.FirstOrDefault(a => a.Id == addressId)
                ?? throw new NotFoundException<RestaurantAddress>(addressId);

            address.ChangeAddress(request.Address);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        private static RestaurantResponse MapToRestaurantResponse(Restaurant restaurant)
        {
            return new RestaurantResponse(
                restaurant.Id,
                restaurant.Name,
                restaurant.ImageUrl,
                [.. restaurant.Addresses.Select(a => new RestaurantAddressResponse(a.Id, a.Address))],
                [.. restaurant.Users.Select(u => new RestaurantUserResponse(u.Id, u.EmailAddress, u.Name, u.ImageUrl))],
                [.. restaurant.Ingredients.Select(i => new IngredientResponse(i.Id, i.RestaurantId, i.Name))]);
        }

        /// <inheritdoc />
        public async Task AddRestaurantIngredientAsync(Guid restaurantId, AddRestaurantIngredientRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            restaurant.AddIngredient(request.Name);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task UpdateRestaurantIngredientAsync(Guid restaurantId, Guid ingredientId, UpdateRestaurantIngredientRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var ingredient = restaurant.Ingredients.FirstOrDefault(i => i.Id == ingredientId)
                ?? throw new NotFoundException<Ingredient>(ingredientId);

            ingredient.ChangeName(request.Name);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task RemoveRestaurantIngredientAsync(Guid restaurantId, Guid ingredientId)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var ingredient = restaurant.Ingredients.FirstOrDefault(i => i.Id == ingredientId)
                ?? throw new NotFoundException<Ingredient>(ingredientId);

            restaurant.RemoveIngredient(ingredient);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task<string> UploadRestaurantImageAsync(Guid restaurantId, Stream stream, string contentType, string extension)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var newImageName = $"restaurant-{Guid.NewGuid()}{extension}";
            var previousImageName = restaurant.ImageUrl;

            await storageService.UploadFileAsync(newImageName, stream, contentType);

            restaurant.ChangeImageUrl(newImageName);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);

            if (previousImageName is not null && previousImageName != newImageName)
            {
                await storageService.DeleteFileAsync(previousImageName);
            }

            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);

            return newImageName;
        }

        /// <inheritdoc />
        public async Task<(Stream Stream, string ContentType)> GetRestaurantImageAsync(string imageName)
        {
            return await storageService.DownloadFileAsync(imageName);
        }

        /// <inheritdoc />
        public async Task RemoveRestaurantImageAsync(Guid restaurantId)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            if (restaurant.ImageUrl is null)
            {
                return;
            }

            var imageKey = restaurant.ImageUrl;

            restaurant.ChangeImageUrl(null);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await storageService.DeleteFileAsync(imageKey);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }
    }
}
