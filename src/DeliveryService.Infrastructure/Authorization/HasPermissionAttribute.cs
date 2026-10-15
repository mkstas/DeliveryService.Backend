using Microsoft.AspNetCore.Authorization;

namespace DeliveryService.Infrastructure.Authorization
{
    /// <summary>
    /// Authorizes access to a controller or action when the current user holds the specified permissions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasPermissionAttribute"/> class.
        /// </summary>
        /// <param name="permissions">The system names of the required permissions.</param>
        public HasPermissionAttribute(params string[] permissions)
        {
            Policy = $"Permission:{string.Join(",", permissions)}";
        }
    }
}
