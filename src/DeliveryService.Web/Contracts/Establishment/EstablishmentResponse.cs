namespace DeliveryService.Web.Contracts.Establishment
{
    public record EstablishmentResponse(
        Guid Id,
        string Name,
        string BusinessAccount,
        List<EstablishmentAddressResponse> Addresses);
}
