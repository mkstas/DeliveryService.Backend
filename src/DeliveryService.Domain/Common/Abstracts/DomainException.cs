namespace DeliveryService.Domain.Common.Abstracts
{
    public abstract class DomainException(string message) : Exception(message)
    {
    }
}
