namespace DeliveryService.Domain.Common.Abstracts
{
    /// <summary>
    /// Base class for domain-specific exceptions.
    /// </summary>
    public abstract class DomainException(string message) : Exception(message)
    {
    }
}
