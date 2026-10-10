using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Repositories;
using DeliveryService.Domain.Interfaces.Services;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class RoleService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository) : IRoleService
    {
        public async Task CreateRoleAsync(StringBounded name)
        {
            await roleRepository.CreateRoleAsync(Role.Create(name));
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            return await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await roleRepository.GetRolesAsync();
        }

        public async Task UpdateRoleAsync(Guid roleId, StringBounded newName)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);

            role.ChangeName(newName);

            await roleRepository.UpdateRoleAsync(role);
        }

        public async Task AddRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);
            var permission = await permissionRepository.GetPermissionByIdAsync(permissionId) ?? throw new NotFoundException<Permission>(permissionId);

            role.AddPermission(permission);

            await roleRepository.UpdateRoleAsync(role);
        }

        public async Task RemoveRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var role = await roleRepository.GetRoleByIdAsync(roleId) ?? throw new NotFoundException<Role>(roleId);
            var permission = await permissionRepository.GetPermissionByIdAsync(permissionId) ?? throw new NotFoundException<Permission>(permissionId);

            role.RemovePermission(permission);

            await roleRepository.UpdateRoleAsync(role);
        }
    }
}
