using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents an ingredient within the system.
    /// </summary>
    public class Ingredient : Entity
    {
        /// <summary>
        /// The identifier of the establishment this ingredient belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        public StringBounded Name { get; private set; }

        /// <summary>
        /// The image URL of the ingredient, if any.
        /// </summary>
        public StringBounded? ImageUrl { get; private set; }

        /// <summary>
        /// Indicates whether the ingredient is visible/active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// The establishment this ingredient belongs to.
        /// </summary>
        public Establishment Establishment { get; init; } = null!;

        private readonly List<MenuItemIngredient> _menuItemIngredients = [];
        /// <summary>
        /// The menu item ingredients that reference this ingredient.
        /// </summary>
        public IReadOnlyList<MenuItemIngredient> MenuItemIngredients => _menuItemIngredients.AsReadOnly();

        private readonly List<CartItem> _cartItems = [];
        /// <summary>
        /// Gets the cart items that include this ingredient.
        /// </summary>
        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        private Ingredient(Guid establishmentId, StringBounded name, StringBounded? imageUrl = null)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            Name = name;
            ImageUrl = imageUrl;
            IsActive = true;
        }

        /// <summary>
        /// Creates a new <see cref="Ingredient"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this ingredient belongs to.</param>
        /// <param name="name">The name of the ingredient. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Ingredient"/> instance.</returns>
        internal static Ingredient Create(Guid establishmentId, StringBounded name, StringBounded? imageUrl = null) => new(establishmentId, name, imageUrl);

        /// <summary>
        /// Changes the name of the ingredient.
        /// </summary>
        /// <param name="newName">The new name to assign to the ingredient.</param>
        public void ChangeName(StringBounded newName) => Name = newName;

        /// <summary>
        /// Changes the image URL of the ingredient.
        /// </summary>
        /// <param name="newImageUrl">The new image URL to assign to the ingredient.</param>
        public void ChangeImageUrl(StringBounded? newImageUrl) => ImageUrl = newImageUrl;

        /// <summary>
        /// Toggles the active state of the ingredient.
        /// </summary>
        public void ChangeVisibility() => IsActive = !IsActive;
    }
}
