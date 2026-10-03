using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.Common.Enums;

namespace DeliveryService.Domain.Exceptions
{
    /// <summary>
    /// Thrown when an order status transition is not allowed.
    /// </summary>
    /// <param name="currentStatus">The status the order is currently in.</param>
    /// <param name="newStatus">The status the order was requested to move to.</param>
    public class InvalidOrderStatusTransitionException(OrderStatus currentStatus, OrderStatus newStatus)
        : DomainException($"Cannot change order status from {currentStatus} to {newStatus}.");
}
