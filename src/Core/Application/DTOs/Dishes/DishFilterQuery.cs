using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Dishes
{
    /// <summary>
    /// Represents optional filters for querying dishes.
    /// </summary>
    public sealed class DishFilterQuery : IValidatableObject
    {
        public string? Search { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public Guid? CategoryId { get; set; }

        public List<Guid>? RestaurantIds { get; set; }

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(Search) &&
            MinPrice is null &&
            MaxPrice is null &&
            CategoryId is null &&
            (RestaurantIds is null || RestaurantIds.Count == 0);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinPrice is < 0)
                yield return new ValidationResult("Minimum price cannot be negative.", [nameof(MinPrice)]);

            if (MaxPrice is < 0)
                yield return new ValidationResult("Maximum price cannot be negative.", [nameof(MaxPrice)]);

            if (MinPrice is not null && MaxPrice is not null && MinPrice > MaxPrice)
                yield return new ValidationResult(
                    "Minimum price cannot be greater than maximum price.",
                    [nameof(MinPrice), nameof(MaxPrice)]);
        }
    }
}
