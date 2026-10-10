using DeliveryService.Domain.Common.Abstracts;

namespace DeliveryService.Domain.Exceptions
{
    public class NotFoundException<TEntity>(object id)
        : DomainException($"{typeof(TEntity).Name} with ID {id} not found.")
    {
    }
}
