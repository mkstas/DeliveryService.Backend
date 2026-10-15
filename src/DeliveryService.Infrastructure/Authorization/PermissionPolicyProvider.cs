using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DeliveryService.Infrastructure.Authorization
{
    /// <summary>
    /// Provides authorization policies for permission-based requirements.
    /// </summary>
    /// <param name="options">The authorization options.</param>
    public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        : DefaultAuthorizationPolicyProvider(options)
    {
        private const string PolicyPrefix = "Permission:";

        /// <summary>
        /// Resolves an authorization policy by name.
        /// </summary>
        /// <param name="policyName">The name of the policy to resolve.</param>
        /// <returns>The resolved authorization policy, or <c>null</c> if none is found.</returns>
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
