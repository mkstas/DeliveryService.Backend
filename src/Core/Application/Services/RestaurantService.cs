using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Restaurants;
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
        ICacheService cacheService) : IRestaurantService
    {
        /// <inheritdoc />
        public async Task CreateRestaurantAsync(CreateRestaurantRequest request)
        {
            await restaurantRepository.CreateRestaurantAsync(Restaurant.Create(request.Name));
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

            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            await cacheService.RemoveAsync(CacheKeys.Restaurants.All);
        }

        /// <inheritdoc />
        public async Task AddRestaurantUserAsync(Guid restaurantId, AddRestaurantUserRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(restaurantId)
                ?? throw new NotFoundException<Restaurant>(restaurantId);

            var user = await userRepository.GetUserByEmailAsync(request.EmailAddress)
                ?? throw new InvalidOperationException("Cannot add user to restaurant.");

            restaurant.AddUser(user);

            await restaurantRepository.UpdateRestaurantAsync(restaurant);

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

        private static RestaurantResponse MapToRestaurantResponse(Restaurant restaurant)
        {
            return new RestaurantResponse(
                restaurant.Id,
                restaurant.Name,
                restaurant.ImageUrl,
                [.. restaurant.Addresses.Select(a => new RestaurantAddressResponse(a.Id, a.Address))],
                [.. restaurant.Users.Select(u => new RestaurantUserResponse(u.Id, u.EmailAddress, u.Name, u.ImageUrl))]);
        }
    }
}
