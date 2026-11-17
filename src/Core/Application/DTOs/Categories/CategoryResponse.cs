namespace Application.DTOs.Categories
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    /// <param name="Id">The category identifier.</param>
    /// <param name="Name">The category name.</param>
    public record CategoryResponse(
        Guid Id,
        string Name);
}
