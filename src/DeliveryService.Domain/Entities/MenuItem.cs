using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a menu item within the system.
    /// </summary>
    public class MenuItem : Entity
    {
        /// <summary>
        /// The identifier of the establishment this menu item belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The identifier of the category this menu item belongs to.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// The name of the menu item.
        /// </summary>
        public StringBounded Name { get; private set; }

        /// <summary>
        /// The description of the menu item, if any.
        /// </summary>
        public StringUnbounded? Description { get; private set; }

        /// <summary>
        /// The establishment this menu item belongs to.
        /// </summary>
        public Menu Menu { get; init; } = null!;

        /// <summary>
        /// The category this menu item belongs to.
        /// </summary>
        public Category Category { get; private set; } = null!;

        private readonly List<MenuItemIngredient> _ingredients = [];
        /// <summary>
        /// The ingredients of the menu item.
        /// </summary>
        public IReadOnlyList<MenuItemIngredient> Ingredients => _ingredients.AsReadOnly();

        private readonly List<MenuItemVariant> _variants = [];
        /// <summary>
        /// The variants of the menu item.
        /// </summary>
        public IReadOnlyList<MenuItemVariant> Variants => _variants.AsReadOnly();

        private MenuItem(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new <see cref="MenuItem"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this menu item belongs to.</param>
        /// <param name="categoryId">The identifier of the category this menu item belongs to.</param>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="description">If provided, must not be empty.</param>
        /// <returns>A new <see cref="MenuItem"/> instance.</returns>
        internal static MenuItem Create(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null)
            => new(establishmentId, categoryId, name, description);

        /// <summary>
        /// Changes the category of the menu item.
        /// </summary>
        /// <param name="categoryId">The new category identifier to assign to the menu item.</param>
        public void ChangeCategory(Guid categoryId) => CategoryId = categoryId;

        /// <summary>
        /// Changes the name of the menu item.
        /// </summary>
        /// <param name="newName">The new name to assign to the menu item.</param>
        public void ChangeName(StringBounded newName) => Name = newName;

        /// <summary>
        /// Changes the description of the menu item.
        /// </summary>
        /// <param name="newDescription">The new description to assign to the menu item.</param>
        public void ChangeDescription(StringUnbounded newDescription) => Description = newDescription;

        /// <summary>
        /// Adds an ingredient association to the menu item.
        /// </summary>
        /// <param name="ingredientId">The identifier of the ingredient to associate.</param>
        /// <param name="price">The price of the ingredient.</param>
        public void CreateIngredient(Guid ingredientId, Currency price)
        {
            var ingredient = MenuItemIngredient.Create(Id, ingredientId, price);
            _ingredients.Add(ingredient);
        }

        /// <summary>
        /// Removes the specified ingredient association from the menu item.
        /// </summary>
        /// <param name="ingredient">The ingredient association to remove.</param>
        public void RemoveIngredient(MenuItemIngredient ingredient) => _ingredients.Remove(ingredient);

        /// <summary>
        /// Adds a variant to the menu item.
        /// </summary>
        /// <param name="price">The price of the variant.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateVariant(Currency price, StringBounded? imageUrl = null)
        {
            var menuItemVariant = MenuItemVariant.Create(Id, price, imageUrl);
            _variants.Add(menuItemVariant);
        }

        /// <summary>
        /// Removes a variant from the menu item.
        /// </summary>
        /// <param name="variant">The variant to remove.</param>
        public void RemoveVariant(MenuItemVariant variant) => _variants.Remove(variant);
    }
}
