using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(Guid userId);
        Task CreateCartItem(Guid userId, Guid dishVariantId, List<Modifier> modifiers, uint quantity);
        Task RemoveCartItem(Guid userId, Guid cartItemId, uint quantity);
    }
}
