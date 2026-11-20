using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to cart items.
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        /// Creates a new cart item.
        /// </summary>
        /// <param name="cartItem">The cart item to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateCartItemAsync(CartItem cartItem);

        /// <summary>
        /// Returns a cart item by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="userId">The identifier of the user the cart item belongs to.</param>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns>The matching cart item, or <c>null</c> if none found.</returns>
        Task<CartItem?> GetCartItemByIdAsync(Guid userId, Guid cartItemId);

        /// <summary>
        /// Returns all cart items for the specified user.
        /// </summary>
        /// <param name="userId">The identifier of the user whose cart items to return.</param>
        /// <returns>All cart items for the specified user.</returns>
        Task<List<CartItem>> GetCartItemsAsync(Guid userId);

        /// <summary>
        /// Updates the specified cart item.
        /// </summary>
        /// <param name="cartItem">The cart item to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateCartItemAsync(CartItem cartItem);

        /// <summary>
        /// Removes the specified cart item.
        /// </summary>
        /// <param name="cartItem">The cart item to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveCartItemAsync(CartItem cartItem);
    }
}
