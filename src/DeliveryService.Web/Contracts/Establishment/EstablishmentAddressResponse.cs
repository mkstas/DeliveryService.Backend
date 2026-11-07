namespace DeliveryService.Web.Contracts.Establishment
{
    /// <summary>
    /// Represents an address of an establishment.
    /// </summary>
    /// <param name="Id">The establishment address's identifier.</param>
    /// <param name="Address">The establishment address value.</param>
    public record EstablishmentAddressResponse(
        Guid Id,
        string Address);
}
