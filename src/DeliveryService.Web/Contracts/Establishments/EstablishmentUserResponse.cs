namespace DeliveryService.Web.Contracts.Establishments
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="EmailAddress"></param>
    /// <param name="Name"></param>
    /// <param name="ImageUrl"></param>
    public record EstablishmentUserResponse(
        Guid Id,
        string EmailAddress,
        string Name,
        string? ImageUrl);
}
