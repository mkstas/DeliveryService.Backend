using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents an address of a restaurant within the system.
    /// </summary>
    public class RestaurantAddress : BaseEntity
    {
        /// <summary>
        /// The identifier of the restaurant this address belongs to.
        /// </summary>
        public Guid RestaurantId { get; init; }

        /// <summary>
        /// The address of the restaurant.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// The restaurant this address belongs to.
        /// </summary>
        public Restaurant Restaurant { get; init; } = null!;

        private RestaurantAddress(Guid restaurantId, string address)
        {
            Id = Guid.Empty;
            RestaurantId = restaurantId;
            Address = address;
        }

        /// <summary>
        /// Creates a new <see cref="RestaurantAddress"/> instance.
        /// </summary>
        /// <param name="restaurantId">The identifier of the restaurant this address belongs to.</param>
        /// <param name="address">Must not be null or empty.</param>
        /// <returns>A new <see cref="RestaurantAddress"/> instance.</returns>
        internal static RestaurantAddress Create(Guid restaurantId, string address)
        {
            ValidationHelper.CheckNotNullOrEmpty(address, nameof(address));

            return new(restaurantId, address);
        }

        /// <summary>
        /// Changes the address of the restaurant.
        /// </summary>
        /// <param name="newAddress">The new address to assign. Must not be null or empty.</param>
        public void ChangeAddress(string newAddress)
        {
            ValidationHelper.CheckNotNullOrEmpty(newAddress, nameof(newAddress));

            Address = newAddress;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
