using Application.DTOs.CartItems;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing cart items.
    /// </summary>
    public interface ICartItemService
    {
        /// <summary>
        /// Creates a new cart item.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to create cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateCartItemAsync(Guid userId, CreateCartItemRequest request);

        /// <summary>
        /// Returns all cart items of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>All cart items of the specified user.</returns>
        Task<IReadOnlyList<CartItemResponse>> GetCartItemsAsync(Guid userId);

        /// <summary>
        /// Returns a cart item by its identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns>The matching cart item.</returns>
        Task<CartItemResponse> GetCartItemByIdAsync(Guid userId, Guid cartItemId);

        /// <summary>
        /// Updates the specified cart item.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <param name="request">The request to update cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request);

        /// <summary>
        /// Remove the specified cart item from the system.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveCartItemAsync(Guid userId, Guid cartItemId);
    }
}
