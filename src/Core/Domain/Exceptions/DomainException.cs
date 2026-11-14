namespace Domain.Exceptions
{
    /// <summary>
    /// Base class for domain-specific exceptions.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public abstract class DomainException(string message) : Exception(message);
}
