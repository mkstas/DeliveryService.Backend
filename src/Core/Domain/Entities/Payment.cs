using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a user payment within the system.
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// The required length of the card number.
        /// </summary>
        public const int CARD_NUMBER_LENGTH = 16;

        /// <summary>
        /// The identifier of the user who owns this payment.
        /// </summary>
        public Guid UserId { get; private init; }

        /// <summary>
        /// The internal name of the permission.
        /// </summary>
        public string CardNumber { get; private init; }

        /// <summary>
        /// The user who owns this payment.
        /// </summary>
        public User User { get; init; } = null!;

        [JsonConstructor]
        private Payment(Guid userId, string cardNumber)
        {
            Id = Guid.Empty;
            UserId = userId;
            CardNumber = cardNumber;
        }

        /// <summary>
        /// Creates a new <see cref="Payment"/> instance.
        /// </summary>
        /// <param name="userId">The identifier of the user who owns the payment.</param>
        /// <param name="cardNumber">Must not be null or empty, must consist of exactly <see cref="Payment.CARD_NUMBER_LENGTH"/> digits.</param>
        /// <returns>A new <see cref="Payment"/> instance.</returns>
        internal static Payment Create(Guid userId, string cardNumber)
        {
            ValidationHelper.CheckNotNullOrEmpty(cardNumber, nameof(cardNumber));
            ValidationHelper.CheckEqualToNumber(cardNumber.Length, CARD_NUMBER_LENGTH, nameof(cardNumber));

            return new(userId, cardNumber);
        }
    }
}
