using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides operations for managing permissions.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Returns all permissions in the system.
        /// </summary>
        /// <returns>All permissions in the system.</returns>
        Task<List<Permission>> GetPermissionsAsync();
    }
}
