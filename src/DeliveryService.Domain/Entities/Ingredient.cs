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
        /// The establishment this ingredient belongs to.
        /// </summary>
        public Establishment Establishment { get; init; } = null!;

        private readonly List<Dish> _dishes = [];
        /// <summary>
        /// The dishes associated with this ingredient.
        /// </summary>
        public IReadOnlyList<Dish> Dishes => _dishes.AsReadOnly();

        private Ingredient(Guid establishmentId, StringBounded name)
        {
            Id = Guid.Empty;
            EstablishmentId = establishmentId;
            Name = name;
        }

        /// <summary>
        /// Creates a new <see cref="Ingredient"/> instance.
        /// </summary>
        /// <param name="establishmentId">The identifier of the establishment this ingredient belongs to.</param>
        /// <param name="name">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <returns>A new <see cref="Ingredient"/> instance.</returns>
        internal static Ingredient Create(Guid establishmentId, StringBounded name) => new(establishmentId, name);

        /// <summary>
        /// Changes the name of the ingredient.
        /// </summary>
        /// <param name="newName">The new name to assign to the ingredient.</param>
        public void ChangeName(StringBounded newName) => Name = newName;
    }
}
