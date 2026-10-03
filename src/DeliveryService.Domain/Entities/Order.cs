using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.Common.Enums;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents an order within the system.
    /// </summary>
    public class Order : Entity
    {
        /// <summary>
        /// The identifier of the user who placed this order.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// The total cost of the order.
        /// </summary>
        public Currency Cost { get; init; }

        /// <summary>
        /// The delivery address of the order.
        /// </summary>
        public StringBounded Address { get; init; }

        /// <summary>
        /// The comment to the order, if any.
        /// </summary>
        public StringUnbounded? Comment { get; private set; }

        /// <summary>
        /// The current status of the order.
        /// </summary>
        public OrderStatus Status { get; private set; }

        /// <summary>
        /// The moment the order was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// The moment the order was last modified.
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// The user who placed this order.
        /// </summary>
        public User User { get; init; } = null!;

        private readonly List<OrderItem> _orderItems = [];
        /// <summary>
        /// The order items of this order.
        /// </summary>
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order(Guid userId, Currency cost, StringBounded address, StringUnbounded? comment = null)
        {
            Id = Guid.Empty;
            UserId = userId;
            Cost = cost;
            Address = address;
            Comment = comment;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        /// <summary>
        /// Creates a new <see cref="Order"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user who places the order.</param>
        /// <param name="cost">The total cost of the order.</param>
        /// <param name="address">Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="comment">If provided, must not be empty.</param>
        /// <returns>A new <see cref="Order"/> instance.</returns>
        internal static Order Create(Guid userId, Currency cost, StringBounded address, StringUnbounded? comment = null)
            => new(userId, cost, address, comment);

        /// <summary>
        /// Changes the comment of the order.
        /// </summary>
        /// <param name="newComment">The new comment to assign to the order.</param>
        public void ChangeComment(StringUnbounded? newComment) => Comment = newComment;

        /// <summary>
        /// Creates an order item for the order.
        /// </summary>
        /// <param name="menuItemVariantId">The identifier of the menu item variant the item refers to.</param>
        /// <param name="price">The price of a single unit of the menu item variant.</param>
        /// <param name="quantity">The quantity of the menu item variant.</param>
        /// <param name="ingredients">The identifiers of the ingredients associated with this order item.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="quantity"/> is zero.
        /// </exception>
        public void CreateOrderItem(Guid menuItemVariantId, Currency price, uint quantity, List<MenuItemIngredient> ingredients)
            => _orderItems.Add(OrderItem.Create(Id, menuItemVariantId, price, quantity, ingredients));

        /// <summary>
        /// Starts cooking the order.
        /// </summary>
        public void StartCooking() => ChangeStatus(OrderStatus.Cooking);

        /// <summary>
        /// Starts delivering the order.
        /// </summary>
        public void StartDelivering() => ChangeStatus(OrderStatus.Delivering);

        /// <summary>
        /// Completes the order.
        /// </summary>
        public void Complete() => ChangeStatus(OrderStatus.Completed);

        /// <summary>
        /// Cancels the order. Can only be performed when the order is in the Created state.
        /// </summary>
        public void Cancel() => ChangeStatus(OrderStatus.Cancelled);

        private void ChangeStatus(OrderStatus newStatus)
        {
            if (!IsAllowedTransition(Status, newStatus))
            {
                throw new InvalidOrderStatusTransitionException(Status, newStatus);
            }

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        private static bool IsAllowedTransition(OrderStatus current, OrderStatus next) => (current, next) switch
        {
            (OrderStatus.Created, OrderStatus.Cooking) => true,
            (OrderStatus.Created, OrderStatus.Cancelled) => true,
            (OrderStatus.Cooking, OrderStatus.Delivering) => true,
            (OrderStatus.Delivering, OrderStatus.Completed) => true,
            _ => false
        };
    }
}
