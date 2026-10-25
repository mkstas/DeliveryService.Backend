using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Auth
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="EmailAddress"></param>
    public record SendConfirmationRequest(
        [Required]
        string EmailAddress);
}
