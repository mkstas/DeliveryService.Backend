using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Specifications;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="ISpecificationService"/>.
    /// </summary>
    internal class SpecificationService(
        ISpecificationRespository specificationRespository,
        ICacheService cacheService) : ISpecificationService
    {
        /// <inheritdoc />
        public async Task CreateSpecificationAsync(CreateSpecificationRequest request)
        {
            await specificationRespository.CreateSpecificationAsync(Specification.Create(request.Name));
            await cacheService.RemoveAsync(CacheKeys.Specifications.All);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<SpecificationResponse>> GetSpecificationsAsync()
        {
            var cachedResponse = await cacheService.GetAsync<IReadOnlyList<SpecificationResponse>>(CacheKeys.Specifications.All);

            if (cachedResponse is not null)
                return cachedResponse;

            var specifications = await specificationRespository.GetSpecificationsAsync();

            IReadOnlyList<SpecificationResponse> response = [.. specifications.Select(c => new SpecificationResponse(c.Id, c.Name))];

            await cacheService.SetAsync(CacheKeys.Specifications.All, response, TimeSpan.FromDays(14));

            return response;
        }

        /// <inheritdoc />
        public async Task<SpecificationResponse> GetSpecificationByIdAsync(Guid specificationId)
        {
            var specification = await specificationRespository.GetSpecificationByIdAsync(specificationId)
                ?? throw new NotFoundException<Specification>(specificationId);

            var response = new SpecificationResponse(specification.Id, specification.Name);

            return response;
        }

        /// <inheritdoc />
        public async Task UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationRequest request)
        {
            var specification = await specificationRespository.GetSpecificationByIdAsync(specificationId)
                ?? throw new NotFoundException<Specification>(specificationId);

            specification.ChangeName(request.Name);

            await specificationRespository.UpdateSpecificationAsync(specification);
            await cacheService.RemoveAsync(CacheKeys.Specifications.All);
        }

        /// <inheritdoc />
        public async Task RemoveSpecificationAsync(Guid specificationId)
        {
            var specification = await specificationRespository.GetSpecificationByIdAsync(specificationId)
                ?? throw new NotFoundException<Specification>(specificationId);

            await specificationRespository.RemoveSpecificationAsync(specification);
            await cacheService.RemoveAsync(CacheKeys.Specifications.All);
        }
    }
}
