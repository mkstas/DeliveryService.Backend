using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class EstablishmentService(
        IEstablishmentRepository establishmentRepository,
        IUserRepository userRepository) : IEstablishmentService
    {
        public async Task CreateEstablishmentAsync(StringBounded name, BankAccount businessAccount)
        {
            await establishmentRepository.CreateEstablishmentAsync(Establishment.Create(name, businessAccount));
        }

        public async Task<Establishment> GetEstablishmentByIdAsync(Guid establishmentId)
        {
            return await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);
        }

        public async Task<List<Establishment>> GetEstablishmentsAsync()
        {
            return await establishmentRepository.GetEstablishmentsAsync();
        }

        public async Task UpdateEstablishmentAsync(Guid establishmentId, StringBounded name, BankAccount businessAccount)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);

            establishment.ChangeName(name);
            establishment.ChangeBusinessAccount(businessAccount);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }

        public async Task AddUserEstablishmentAsync(Guid establishmentId, EmailAddress address)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);
            var user = await userRepository.GetUserByEmailAsync(address) ?? throw new NotFoundException<User>(address);

            establishment.AddUser(user);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }

        public async Task RemoveUserEstablishmentAsync(Guid establishmentId, Guid userId)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);
            var user = await userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException<User>(userId);

            establishment.RemoveUser(user);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }

        public async Task AddEstablishmentAddressAsync(Guid establishmentId, StringBounded address)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);

            establishment.CreateAddress(address);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }

        public async Task RemoveEstablishmentAddressAsync(Guid establishmentId, Guid establishmentAddressId)
        {
            var establishment = await establishmentRepository.GetEstablishmentByIdAsync(establishmentId)
                ?? throw new NotFoundException<Establishment>(establishmentId);

            var storedAddress = establishment.Addresses.FirstOrDefault(a => a.Id == establishmentAddressId)
                ?? throw new NotFoundException<EstablishmentAddress>(establishmentAddressId);

            establishment.RemoveAddress(storedAddress);

            await establishmentRepository.UpdateEstablishmentAsync(establishment);
        }
    }
}
