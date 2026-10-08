using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a dish within the system.
    /// </summary>
    public class Dish : Entity
    {
        /// <summary>
        /// The identifier of the establishment this dish belongs to.
        /// </summary>
        public Guid EstablishmentId { get; init; }

        /// <summary>
        /// The identifier of the category this dish belongs to.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// The name of the dish.
        /// </summary>
        public StringBounded Name { get; private set; }

        /// <summary>
        /// The description of the dish, if any.
        /// </summary>
        public StringUnbounded? Description { get; private set; }

        /// <summary>
        /// The establishment this dish belongs to.
        /// </summary>
        public Establishment Establishment { get; init; } = null!;

        /// <summary>
        /// The category this dish belongs to.
        /// </summary>
        public Category Category { get; private set; } = null!;

        private readonly List<Ingredient> _ingredients = [];
        /// <summary>
        /// The ingredients associated with this dish.
        /// </summary>
        public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();

        private readonly List<DishVariant> _variants = [];
        /// <summary>
        /// The variants of the dish.
        /// </summary>
        public IReadOnlyList<DishVariant> Variants => _variants.AsReadOnly();

        private readonly List<Modifier> _modifiers = [];
        /// <summary>
        /// The modifiers associated with the dish.
        /// </summary>
        public IReadOnlyList<Modifier> Modifiers => _modifiers.AsReadOnly();

        private Dish(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new <see cref="Dish"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this dish belongs to.</param>
        /// <param name="categoryId">The identifier of the category this dish belongs to.</param>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="description">If provided, must not be empty.</param>
        /// <returns>A new <see cref="Dish"/> instance.</returns>
        internal static Dish Create(Guid establishmentId, Guid categoryId, StringBounded name, StringUnbounded? description = null)
            => new(establishmentId, categoryId, name, description);

        /// <summary>
        /// Changes the category of the dish.
        /// </summary>
        /// <param name="categoryId">The new category identifier to assign to the dish.</param>
        public void ChangeCategory(Guid categoryId) => CategoryId = categoryId;

        /// <summary>
        /// Changes the name of the dish.
        /// </summary>
        /// <param name="newName">The new name to assign to the dish.</param>
        public void ChangeName(StringBounded newName) => Name = newName;

        /// <summary>
        /// Changes the description of the dish.
        /// </summary>
        /// <param name="newDescription">The new description to assign to the dish.</param>
        public void ChangeDescription(StringUnbounded newDescription) => Description = newDescription;

        /// <summary>
        /// Adds an ingredient to the dish.
        /// </summary>
        /// <param name="ingredient">The ingredient to add.</param>
        public void AddIngredient(Ingredient ingredient) => _ingredients.Add(ingredient);

        /// <summary>
        /// Removes an ingredient from the dish.
        /// </summary>
        /// <param name="ingredient">The ingredient to remove.</param>
        public void RemoveIngredient(Ingredient ingredient) => _ingredients.Remove(ingredient);

        /// <summary>
        /// Adds a variant to the dish.
        /// </summary>
        /// <param name="price">Must not be negative.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateVariant(Currency price, StringBounded? imageUrl = null)
        {
            var variant = DishVariant.Create(Id, price, imageUrl);
            _variants.Add(variant);
        }

        /// <summary>
        /// Removes a variant from the dish.
        /// </summary>
        /// <param name="variant">The variant to remove.</param>
        public void RemoveVariant(DishVariant variant) => _variants.Remove(variant);
    }
}
