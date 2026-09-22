using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Exceptions
{
    public class AlreadyExistsException<TEntity>(object value)
        : DomainException($"{typeof(TEntity).Name} with value {value} is already exists.");
}
