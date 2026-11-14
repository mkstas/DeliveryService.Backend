namespace Application.Abstractions.Cache
{
    /// <summary>
    ///
    /// </summary>
    internal class CacheKeys
    {
        internal class Permission
        {
            public const string All = "permissions:all";
        }

        public const string UsersPending = "users:pending";
        public const string UsersVerificationCode = "users:verification-code";
    }
}
