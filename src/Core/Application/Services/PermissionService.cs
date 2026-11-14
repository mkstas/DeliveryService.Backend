using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Permissions;
using Domain.Abstractions.Repositories;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="IPermissionService"/>.
    /// </summary>
    internal class PermissionService(
        IPermissionRepository permissionRepository,
        ICacheService cacheService) : IPermissionService
    {
        /// <inheritdoc />
        public async Task<IReadOnlyList<PermissionResponse>> GetPermissionsAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<PermissionResponse>>(CacheKeys.Permission.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var permissions = await permissionRepository.GetPermissionsAsync();

            IReadOnlyList<PermissionResponse> response = [.. permissions.Select(p => new PermissionResponse(p.Id, p.DisplayName))];

            await cacheService.SetAsync(CacheKeys.Permission.All, response, TimeSpan.FromHours(24));

            return response;
        }
    }
}
