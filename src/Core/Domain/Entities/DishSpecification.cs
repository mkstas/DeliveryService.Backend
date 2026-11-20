using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a specification value assigned to a dish.
    /// </summary>
    public class DishSpecification : BaseEntity
    {
        /// <summary>
        /// The identifier of the dish.
        /// </summary>
        public Guid DishId { get; private init; }

        /// <summary>
        /// The identifier of the specification.
        /// </summary>
        public Guid SpecificationId { get; private init; }

        /// <summary>
        /// The value of the specification.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The dish the specification is assigned to.
        /// </summary>
        public Dish Dish { get; private init; } = null!;

        /// <summary>
        /// The specification assigned to the dish.
        /// </summary>
        public Specification Specification { get; private init; } = null!;

        [JsonConstructor]
        private DishSpecification(Guid dishId, Guid specificationId, string value)
        {
            DishId = dishId;
            SpecificationId = specificationId;
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="DishSpecification"/> instance.
        /// </summary>
        /// <param name="dishId">The identifier of the dish.</param>
        /// <param name="specificationId">The identifier of the specification.</param>
        /// <param name="value">The value of the specification. Must not be null or empty.</param>
        /// <returns>A new <see cref="DishSpecification"/> instance.</returns>
        public static DishSpecification Create(Guid dishId, Guid specificationId, string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(value));

            return new(dishId, specificationId, value);
        }

        /// <summary>
        /// Changes the specification value.
        /// </summary>
        /// <param name="newValue">The new value to assign. Must not be null or empty.</param>
        public void ChangeValue(string newValue)
        {
            ValidationHelper.CheckNotNullOrEmpty(newValue, nameof(newValue));

            Value = newValue;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
