using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RoleId"></param>
    public record AddUserRoleRequest(
        [Required(ErrorMessage = "User id is required.")]
        Guid UserId,

        [Required(ErrorMessage = "Role id is required.")]
        Guid RoleId);
}
