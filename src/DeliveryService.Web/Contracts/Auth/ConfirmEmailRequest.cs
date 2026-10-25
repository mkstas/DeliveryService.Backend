using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Web.Contracts.Auth
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="EmailAddress"></param>
    /// <param name="Code"></param>
    public record ConfirmEmailRequest(
        [Required]
        string EmailAddress,

        [Required]
        string Code);
}
