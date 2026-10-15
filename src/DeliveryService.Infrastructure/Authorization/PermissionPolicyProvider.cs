using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DeliveryService.Infrastructure.Authorization
{
    public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        : DefaultAuthorizationPolicyProvider(options)
    {
        private const string PolicyPrefix = "Permission:";

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var permissionNames = policyName[PolicyPrefix.Length..]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(permissionNames));

                return policy.Build();
            }

            return await base.GetPolicyAsync(policyName);
        }
    }
}
