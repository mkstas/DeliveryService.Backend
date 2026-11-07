namespace DeliveryService.Web.Contracts.Establishments
{
    /// <summary>
    /// Represents an establishment.
    /// </summary>
    /// <param name="Id">The establishment's identifier.</param>
    /// <param name="Name">The establishment's name.</param>
    /// <param name="BusinessAccount">The establishment's business bank account.</param>
    /// <param name="Addresses">The addresses of the establishment.</param>
    public record EstablishmentResponse(
        Guid Id,
        string Name,
        string BusinessAccount,
        List<EstablishmentAddressResponse> Addresses);
}
