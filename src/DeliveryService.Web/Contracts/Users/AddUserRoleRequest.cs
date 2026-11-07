using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Users
{
    /// <summary>
    /// Represents the data required to assign a role to a user.
    /// </summary>
    /// <param name="UserId">The user identifier.</param>
    /// <param name="RoleId">The role identifier.</param>
    public record AddUserRoleRequest(
        [Required(ErrorMessage = "User id is required.")]
        Guid UserId,

        [Required(ErrorMessage = "Role id is required.")]
        Guid RoleId);
}
