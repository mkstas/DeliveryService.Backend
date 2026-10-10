using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace DeliveryService.Infrastructure.Common
{
    /// <summary>
    /// Provides password hashing and verification using Argon2id.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int DegreeOfParallelism = 4;
        private const int MemorySize = 65536;
        private const int Iterations = 2;

        /// <summary>
        /// Generates a salted Argon2id hash for the specified password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The base64-encoded hash.</returns>
        public static string Generate(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = ToHash(password, salt);
            byte[] hashBytes = new byte[SaltSize + HashSize];

            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a password against a previously generated hash.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="base64Hash">The base64-encoded hash to verify against.</param>
        /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
        public static bool Verify(string password, string base64Hash)
        {
            byte[] hashBytes = Convert.FromBase64String(base64Hash);

            if (hashBytes.Length != SaltSize + HashSize)
            {
                return false;
            }

            byte[] storedSalt = new byte[SaltSize];
            byte[] storedHash = new byte[HashSize];

            Array.Copy(hashBytes, 0, storedSalt, 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

            byte[] computedHash = ToHash(password, storedSalt);

            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }

        private static byte[] ToHash(string password, byte[] salt)
        {
            using var argon2id = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySize,
                Iterations = Iterations
            };

            return argon2id.GetBytes(HashSize);
        }
    }
}
