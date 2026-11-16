using System.Security.Cryptography;

namespace Application.Common
{
    /// <summary>
    /// Provides verification code generating.
    /// </summary>
    internal class VerificationCode
    {
        /// <summary>
        /// Generates a verification code.
        /// </summary>
        /// <returns>Generated verification code</returns>
        public static string Generate()
        {
            var bytes = RandomNumberGenerator.GetBytes(4);
            var value = BitConverter.ToUInt32(bytes, 0) % 1000000;

            return value.ToString("D6");
        }
    }
}
