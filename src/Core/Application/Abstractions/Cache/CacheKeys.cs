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
            public const string All = "permissions:all";
        }

        /// <summary>
        /// Cache keys for roles.
        /// </summary>
        internal class Roles
        {
            public const string All = "roles:all";
        }

        /// <summary>
        /// Cache keys for users.
        /// </summary>
        internal class Users
        {
            public const string All = "users:all";

            public static string Pending(string emailAddress)
                => $"users:pending:{emailAddress}";

            public static string VerificationCode(string emailAddress)
                => $"users:verification-code:{emailAddress}";
        }

        /// <summary>
        /// Cache keys for restaurants.
        /// </summary>
        internal class Restaurants
        {
            public const string All = "restaurants:all";
        }

        /// <summary>
        /// Cache keys for categories.
        /// </summary>
        internal class Categories
        {
            public const string All = "categories:all";
        }
    }
}
