using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing modifiers.
    /// </summary>
    public interface IModifierService
    {
        /// <summary>
        /// Creates a new modifier.
        /// </summary>
        /// <param name="establishmentId">The establishment identifier.</param>
        /// <param name="name">The name of the modifier to create.</param>
        /// <param name="price">The price of the modifier to create.</param>
        Task CreateModifierAsync(Guid establishmentId, StringBounded name, Currency price);

        /// <summary>
        /// Updates the specified modifier.
        /// </summary>
        /// <param name="modifierId">The modifier identifier.</param>
        /// <param name="newName">The new name of the modifier.</param>
        /// <param name="newPrice">The new price of the modifier.</param>
        Task UpdateModifierAsync(Guid modifierId, StringBounded newName, Currency newPrice);
    }
}
