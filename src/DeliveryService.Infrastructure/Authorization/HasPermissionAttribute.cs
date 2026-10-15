using Microsoft.AspNetCore.Authorization;

namespace DeliveryService.Infrastructure.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(params string[] permissions)
        {
            Policy = $"Permission:{string.Join(",", permissions)}";
        }
    }
}
