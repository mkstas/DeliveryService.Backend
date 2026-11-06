using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Persistence.Postgres
{
    /// <summary>
    /// Provides read/write access to modifiers.
    /// </summary>
    public interface IModifierRepository
    {
        /// <summary>
        /// Returns a modifier by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="modifierId">The modifier identifier.</param>
        /// <returns>The matching modifier, or <c>null</c> if none found.</returns>
        Task<Modifier?> GetModifierByIdAsync(Guid modifierId);

        /// <summary>
        /// Updates the specified modifier.
        /// </summary>
        /// <param name="modifier">The modifier to update.</param>
        Task UpdateModifierAsync(Modifier modifier);
    }
}
