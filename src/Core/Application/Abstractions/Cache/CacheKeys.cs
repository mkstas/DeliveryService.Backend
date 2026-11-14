namespace Application.Abstractions.Cache
{
    /// <summary>
    /// Provides the cache keys.
    /// </summary>
    internal class CacheKeys
    {
        /// <summary>
        /// Cache keys for permissions.
        /// </summary>
        internal class Permission
        {
            /// <summary>
            /// The cache key for all permissions.
            /// </summary>
            public const string All = "permissions:all";
        }

        /// <summary>
        /// Cache keys for roles.
        /// </summary>
        internal class Roles
        {
            /// <summary>
            /// The cache key for all roles.
            /// </summary>
            public const string All = "roles:all";
        }

        /// <summary>
        /// Cache keys for users.
        /// </summary>
        internal class Users
        {
            /// <summary>
            /// The cache key for pending users.
            /// </summary>
            public const string UsersPending = "users:pending";

             /// <summary>
            /// The cache key for user verification codes.
            /// </summary>
            public const string UsersVerificationCode = "users:verification-code";
        }
    }
}
