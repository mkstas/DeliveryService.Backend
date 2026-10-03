using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents the association between a menu item and an ingredient.
    /// </summary>
    public class MenuItemIngredient : Entity
    {
        /// <summary>
        /// The identifier of the menu item this association belongs to.
        /// </summary>
        public Guid MenuItemId { get; init; }

        /// <summary>
        /// The identifier of the ingredient this association refers to.
        /// </summary>
        public Guid IngredientId { get; init; }

        /// <summary>
        /// The price of the ingredient, if any.
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// Indicates whether the ingredient is additional.
        /// </summary>
        public bool IsAdditional { get; private set; }

        /// <summary>
        /// The menu item this association belongs to.
        /// </summary>
        public MenuItem MenuItem { get; init; } = null!;

        /// <summary>
        /// The ingredient this association refers to.
        /// </summary>
        public Ingredient Ingredient { get; init; } = null!;

        private MenuItemIngredient(Guid menuItemId, Guid ingredientId, Currency price, bool isAdditional = false)
        {
            Id = Guid.Empty;
            MenuItemId = menuItemId;
            IngredientId = ingredientId;
            Price = price;
            IsAdditional = isAdditional;
        }

        /// <summary>
        /// Creates a new <see cref="MenuItemIngredient"/> instance.
        /// </summary>
        /// <param name="menuItemId">The identifier of the menu item this association belongs to.</param>
        /// <param name="ingredientId">The identifier of the ingredient this association refers to.</param>
        /// <param name="price">The price of the ingredient.</param>
        /// <param name="isAdditional">Indicates whether the ingredient is additional.</param>
        /// <returns>A new <see cref="MenuItemIngredient"/> instance.</returns>
        internal static MenuItemIngredient Create(Guid menuItemId, Guid ingredientId, Currency price, bool isAdditional = false)
            => new(menuItemId, ingredientId, price, isAdditional);

        /// <summary>
        /// Changes the price of the ingredient.
        /// </summary>
        /// <param name="newPrice">The new price to assign to the ingredient.</param>
        public void ChangePrice(Currency newPrice) => Price = newPrice;

        /// <summary>
        /// Toggles whether the ingredient is additional.
        /// </summary>
        public void ChangeAdditional() => IsAdditional = !IsAdditional;
    }
}
