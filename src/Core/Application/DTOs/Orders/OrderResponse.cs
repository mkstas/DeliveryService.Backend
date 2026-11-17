namespace Application.DTOs.Orders
{
    /// <summary>
    /// Represents an order.
    /// </summary>
    /// <param name="Id">The order identifier.</param>
    /// <param name="UserId">The user identifier.</param>
    /// <param name="Status">The order status.</param>
    /// <param name="Address">The delivery address.</param>
    /// <param name="Cost">The order cost.</param>
    /// <param name="Details">The additional order details, if any.</param>
    public record OrderResponse(
        Guid Id,
        Guid UserId,
        string Status,
        string Address,
        decimal Cost,
        string? Details);
}
