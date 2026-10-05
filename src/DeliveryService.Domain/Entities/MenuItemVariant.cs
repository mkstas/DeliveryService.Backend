using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a variant of a menu item within the system.
    /// </summary>
    public class MenuItemVariant : Entity
    {
        /// <summary>
        /// The identifier of the menu item this variant belongs to.
        /// </summary>
        public Guid MenuItemId { get; init; }

        /// <summary>
        /// The price of the variant.
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// The image URL of the variant, if any.
        /// </summary>
        public StringBounded? ImageUrl { get; private set; }

        /// <summary>
        /// Gets the menu item this variant belongs to.
        /// </summary>
        public MenuItem MenuItem { get; init; } = null!;

        private readonly List<MenuItemSpecification> _specifications = [];
        /// <summary>
        /// The specifications of the variant.
        /// </summary>
        public IReadOnlyList<MenuItemSpecification> Specifications => _specifications.AsReadOnly();

        private readonly List<CartItem> _cartItems = [];
        /// <summary>
        /// The cart items associatad with this variant.
        /// </summary>
        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        private MenuItemVariant(Guid menuItemId, Currency price, StringBounded? imageUrl = null)
        {
            Id = Guid.Empty;
            MenuItemId = menuItemId;
            Price = price;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Creates a new <see cref="MenuItemVariant"/> instance.
        /// </summary>
        /// <param name="menuItemId">The identifier of the menu item this variant belongs to.</param>
        /// <param name="price">The price of the variant.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="MenuItemVariant"/> instance.</returns>
        internal static MenuItemVariant Create(Guid menuItemId, Currency price, StringBounded? imageUrl = null)
            => new(menuItemId, price, imageUrl);

        /// <summary>
        /// Changes the price of the variant.
        /// </summary>
        /// <param name="newPrice">The new price to assign to the variant.</param>
        public void ChangePrice(Currency newPrice) => Price = newPrice;

        /// <summary>
        /// Changes the image URL of the variant.
        /// </summary>
        /// <param name="newImageUrl">The new image URL to assign to the variant.</param>
        public void ChangeImageUrl(StringBounded? newImageUrl = null) => ImageUrl = newImageUrl;

        /// <summary>
        /// Creates a specification for the variant.
        /// </summary>
        /// <param name="specificationId">The identifier of the specification to associate.</param>
        /// <param name="value">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateSpecification(Guid specificationId, StringBounded value)
        {
            var menuItemSpecification = MenuItemSpecification.Create(Id, specificationId, value);
            _specifications.Add(menuItemSpecification);
        }

        /// <summary>
        /// Removes a specification from the variant.
        /// </summary>
        /// <param name="specification">The specification to remove.</param>
        public void RemoveSpecification(MenuItemSpecification specification) => _specifications.Remove(specification);
    }
}
