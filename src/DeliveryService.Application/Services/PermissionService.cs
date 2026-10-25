using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;

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
