using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents an order placed by a user.
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// The identifier of the user who placed the order.
        /// </summary>
        public Guid UserId { get; private init; }

        /// <summary>
        /// The status of the order.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// The delivery address of the order.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// The total cost of the order.
        /// </summary>
        public decimal Cost { get; private set; }

        /// <summary>
        /// The additional order details, if any.
        /// </summary>
        public string? Details { get; private set; }

        /// <summary>
        /// The comment left by the user, if any.
        /// </summary>
        public string? Comment { get; private set; }

        /// <summary>
        /// The user who placed the order.
        /// </summary>
        public User User { get; private init; } = null!;

        [JsonConstructor]
        private Order(Guid userId, string status, string address, decimal cost, string? details, string? comment)
        {
            UserId = userId;
            Status = status;
            Address = address;
            Cost = cost;
            Details = details;
            Comment = comment;
        }

        /// <summary>
        /// Creates a new <see cref="Order"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user who placed the order.</param>
        /// <param name="status">The status of the order. Must not be null or empty.</param>
        /// <param name="address">The delivery address of the order. Must not be null or empty.</param>
        /// <param name="cost">The total cost of the order. Must not be negative.</param>
        /// <param name="details">The additional order details, if any.</param>
        /// <param name="comment">The comment left by the user, if any.</param>
        /// <returns>A new <see cref="Order"/> instance.</returns>
        public static Order Create(Guid userId, string status, string address, decimal cost, string? details = null, string? comment = null)
        {
            ValidationHelper.CheckNotNullOrEmpty(status, nameof(status));
            ValidationHelper.CheckNotNullOrEmpty(address, nameof(address));
            ArgumentOutOfRangeException.ThrowIfNegative(cost);

            return new(userId, status, address, cost, details, comment);
        }

        /// <summary>
        /// Changes the status of the order.
        /// </summary>
        /// <param name="newStatus">The new status to assign. Must not be null or empty.</param>
        public void ChangeStatus(string newStatus)
        {
            ValidationHelper.CheckNotNullOrEmpty(newStatus, nameof(newStatus));

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the delivery address of the order.
        /// </summary>
        /// <param name="newAddress">The new delivery address to assign. Must not be null or empty.</param>
        public void ChangeAddress(string newAddress)
        {
            ValidationHelper.CheckNotNullOrEmpty(newAddress, nameof(newAddress));

            Address = newAddress;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the total cost of the order.
        /// </summary>
        /// <param name="newCost">The new cost to assign. Must not be negative.</param>
        public void ChangeCost(decimal newCost)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(newCost);

            Cost = newCost;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the additional order details.
        /// </summary>
        /// <param name="newDetails">The new details, or <c>null</c> to remove them.</param>
        public void ChangeDetails(string? newDetails)
        {
            Details = newDetails;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Changes the comment left by the user.
        /// </summary>
        /// <param name="newComment">The new comment, or <c>null</c> to remove it.</param>
        public void ChangeComment(string? newComment)
        {
            Comment = newComment;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
