using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class SpecificationService(ISpecificationRepository specificationRepository) : ISpecificationService
    {
        public async Task CreateSpecificationAsync(StringBounded name)
        {
            await specificationRepository.CreateSpecificationAsync(Specification.Create(name));
        }

        public async Task<Specification> GetSpecificationByIdAsync(Guid specificationId)
        {
            return await specificationRepository.GetSpecificationByIdAsync(specificationId)
                ?? throw new NotFoundException<Specification>(specificationId);
        }

        public async Task<List<Specification>> GetSpecificationsAsync()
        {
            return await specificationRepository.GetSpecificationsAsync();
        }

        public async Task UpdateCpecificationAsync(Guid specificationId, StringBounded newName)
        {
            var specification = await specificationRepository.GetSpecificationByIdAsync(specificationId)
                ?? throw new NotFoundException<Specification>(specificationId);

            specification.ChangeName(newName);

            await specificationRepository.UpdateCategoryAsync(specification);
        }
    }
}
