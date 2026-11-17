using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    public class DishSpecification : BaseEntity
    {
        public Guid DishId { get; private init; }

        public Guid SpecificationId { get; private init; }

        public string Value { get; private set; }

        public Dish Dish { get; private init; } = null!;

        public Specification Specification { get; private init; } = null!;

        [JsonConstructor]
        private DishSpecification(Guid dishId, Guid specificationId, string value)
        {
            DishId = dishId;
            SpecificationId = specificationId;
            Value = value;
        }

        public static DishSpecification Create(Guid dishId, Guid specificationId, string value)
        {
            ValidationHelper.CheckNotNullOrEmpty(value, nameof(value));

            return new(dishId, specificationId, value);
        }

        public void ChangeValue(string newValue)
        {
            ValidationHelper.CheckNotNullOrEmpty(newValue, nameof(newValue));

            Value = newValue;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
