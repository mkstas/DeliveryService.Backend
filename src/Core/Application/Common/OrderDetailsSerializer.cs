using System.Text.Json;
using Application.DTOs.Orders;

namespace Application.Common
{
    /// <summary>
    /// Serializes and deserializes the dishes stored in order details.
    /// </summary>
    internal static class OrderDetailsSerializer
    {
        private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// Serializes dishes into the order details JSON.
        /// </summary>
        /// <param name="dishes">The dishes to serialize.</param>
        /// <returns>A JSON string, or <c>null</c> when there are no dishes.</returns>
        public static string? Serialize(IReadOnlyList<OrderDish>? dishes)
        {
            return dishes is null ? null : JsonSerializer.Serialize(dishes, Options);
        }

        /// <summary>
        /// Deserializes dishes from the order details JSON.
        /// </summary>
        /// <param name="details">The order details JSON.</param>
        /// <returns>The deserialized dishes, or <c>null</c> when details are absent.</returns>
        public static IReadOnlyList<OrderDish>? Deserialize(string? details)
        {
            return string.IsNullOrWhiteSpace(details)
                ? null
                : JsonSerializer.Deserialize<List<OrderDish>>(details, Options);
        }
    }
}
