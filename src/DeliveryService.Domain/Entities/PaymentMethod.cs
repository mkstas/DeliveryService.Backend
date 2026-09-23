using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents a payment method within the system.
    /// </summary>
    public class PaymentMethod : Entity
    {
        /// <summary>
        /// The identifier of the user who owns this payment method.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// The card number of the payment method.
        /// </summary>
        public CardNumber CardNumber { get; init; }

        /// <summary>
        /// The user who owns this payment method.
        /// </summary>
        public User? User { get; init; }

        private PaymentMethod(Guid userId, CardNumber cardNumber)
        {
            Id = Guid.Empty;
            UserId = userId;
            CardNumber = cardNumber;
        }

        /// <summary>
        /// Creates a new <see cref="PaymentMethod"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user who owns the payment method.</param>
        /// <param name="cardNumber">The card number of the payment method.</param>
        /// <returns>A new <see cref="PaymentMethod"/> instance.</returns>
        internal static PaymentMethod Create(Guid userId, CardNumber cardNumber) => new(userId, cardNumber);
    }
}
