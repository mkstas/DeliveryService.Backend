using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a menu within the system.
    /// </summary>
    public class Menu : Entity
    {
        /// <summary>
        /// The identifier of the establishment this menu belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The establishment this menu belongs to.
        /// </summary>
        public Establishment Establishment { get; init; } = null!;

        private readonly List<MenuItem> _menuItems = [];
        /// <summary>
        /// The menu items of this menu.
        /// </summary>
        public IReadOnlyList<MenuItem> MenuItems => _menuItems.AsReadOnly();

        private Menu(Guid establishmentId)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
        }

        /// <summary>
        /// Creates a new <see cref="Menu"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this menu belongs to.</param>
        /// <returns>A new <see cref="Menu"/> instance.</returns>
        internal static Menu Create(Guid establishmentId) => new(establishmentId);
    }
}
