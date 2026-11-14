using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Permissions;
using Application.DTOs.Roles;
using Domain.Abstractions.Repositories;
using Domain.Entities.Catalog;
using Domain.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="IRoleService"/>.
    /// </summary>
    internal class RoleService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        ICacheService cacheService) : IRoleService
    {
        /// <inheritdoc />
        public async Task CreateRoleAsync(CreateRoleRequest request)
        {
            await roleRepository.CreateRoleAsync(Role.Create(request.Name));
            await cacheService.RemoveAsync(CacheKeys.Roles.All);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<RoleResponse>> GetRolesAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<RoleResponse>>(CacheKeys.Roles.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var roles = await roleRepository.GetRolesAsync();

            IReadOnlyList<RoleResponse> response = [.. roles.Select(r => new RoleResponse(r.Id, r.Name,
                [.. r.Permissions.Select(p => new PermissionResponse(p.Id, p.DisplayName))]))];

            await cacheService.SetAsync(CacheKeys.Roles.All, response, TimeSpan.FromDays(7));

            return response;
        }

        /// <inheritdoc />
        public async Task<RoleResponse> GetRoleByIdAsync(Guid roleId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            var response = new RoleResponse(role.Id, role.Name,
                [.. role.Permissions.Select(p => new PermissionResponse(p.Id, p.DisplayName))]);

            return response;
        }

        /// <inheritdoc />
        public async Task UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            role.ChangeName(request.Name);

            await roleRepository.UpdateRoleAsync(role);
            await cacheService.RemoveAsync(CacheKeys.Roles.All);
        }

        /// <inheritdoc />
        public async Task DeleteRoleAsync(Guid roleId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            await roleRepository.RemoveRoleAsync(role);
            await cacheService.RemoveAsync(CacheKeys.Roles.All);
        }

        /// <inheritdoc />
        public async Task AddRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            var permission = await permissionRepository.GetPermissionByIdAsync(permissionId)
                ?? throw new NotFoundException<Permission>(permissionId);

            role.AddPermission(permission);

            await roleRepository.UpdateRoleAsync(role);
        }

        /// <inheritdoc />
        public async Task RemoveRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new NotFoundException<Role>(roleId);

            var permission = await permissionRepository.GetPermissionByIdAsync(permissionId)
                ?? throw new NotFoundException<Permission>(permissionId);

            role.RemovePermission(permission);

            await roleRepository.UpdateRoleAsync(role);
        }
    }
}
