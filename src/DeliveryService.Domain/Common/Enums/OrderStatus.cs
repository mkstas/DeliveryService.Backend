namespace DeliveryService.Domain.Common.Enums
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been created.
        /// </summary>
        Created,

        /// <summary>
        /// The order is being cooked.
        /// </summary>
        Cooking,

        /// <summary>
        /// The order is being delivered.
        /// </summary>
        Delivering,

        /// <summary>
        /// The order has been completed.
        /// </summary>
        Completed,

        /// <summary>
        /// The order has been cancelled.
        /// </summary>
        Cancelled
    }
}
