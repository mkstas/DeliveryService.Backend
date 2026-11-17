using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; private init; }

        public string Status { get; private set; }

        public string Address { get; private set; }

        public decimal Cost { get; private set; }

        public string? Details { get; private set; }

        public User User { get; private init; } = null!;

        [JsonConstructor]
        private Order(Guid userId, string status, string address, decimal cost, string? details)
        {
            UserId = userId;
            Status = status;
            Address = address;
            Cost = cost;
            Details = details;
        }

        public static Order Create(Guid userId, string status, string address, decimal cost, string? details = null)
        {
            ValidationHelper.CheckNotNullOrEmpty(status, nameof(status));
            ValidationHelper.CheckNotNullOrEmpty(address, nameof(address));
            ArgumentOutOfRangeException.ThrowIfNegative(cost);

            return new(userId, status, address, cost, details);
        }

        public void ChangeStatus(string newStatus)
        {
            ValidationHelper.CheckNotNullOrEmpty(newStatus, nameof(newStatus));

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeAddress(string newAddress)
        {
            ValidationHelper.CheckNotNullOrEmpty(newAddress, nameof(newAddress));

            Address = newAddress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeCost(decimal newCost)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(newCost);

            Cost = newCost;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDetails(string? newDetails)
        {
            Details = newDetails;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
