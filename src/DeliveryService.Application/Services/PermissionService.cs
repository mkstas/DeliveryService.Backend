using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Interfaces.Repositories;
using DeliveryService.Domain.Interfaces.Services;

namespace DeliveryService.Application.Services
{
    public class PermissionService(IPermissionRepository permissionRepository) : IPermissionService
    {
        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return await permissionRepository.GetPermissionsAsync();
        }
    }
}
