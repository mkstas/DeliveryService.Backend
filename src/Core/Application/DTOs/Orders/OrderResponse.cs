namespace Application.DTOs.Orders
{
    public record OrderResponse(
        Guid Id,
        Guid UserId,
        string Status,
        string Address,
        decimal Cost,
        string? Details);
}
