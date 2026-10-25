using DeliveryService.Domain.Interfaces.Persistence.Redis;
using StackExchange.Redis;

namespace DeliveryService.Persistence.Redis.Repositories
{
    public class VerificationCodeStore(IDatabase database) : IVerificationCodeStore
    {
        private const string KeyPrefix = "email-verification:";

        public async Task SetCodeAsync(string email, string code, TimeSpan ttl)
        {
            var key = GetKey(email);

            await database.StringSetAsync(key, code, ttl);
        }

        public async Task<string?> GetCodeAsync(string email)
        {
            var key = GetKey(email);
            var value = await database.StringGetAsync(key);

            return value.HasValue ? value.ToString() : null;
        }

        public async Task RemoveCodeAsync(string email)
        {
            var key = GetKey(email);

            await database.KeyDeleteAsync(key);
        }

        private static string GetKey(string email) => $"{KeyPrefix}{email.Trim().ToLowerInvariant()}";
    }
}
