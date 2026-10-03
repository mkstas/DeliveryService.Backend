using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Entities
{
    /// <summary>
    /// Represents an establishment within the system.
    /// </summary>
    public class Establishment : Entity
    {
        /// <summary>
        /// The name of the establishment.
        /// </summary>
        public StringBounded Name { get; private set; }

        /// <summary>
        /// The bank account of the establishment.
        /// </summary>
        public BankAccount BusinessAccount { get; private set; }

        /// <summary>
        /// The menu of the establishment, if one has been created.
        /// </summary>
        public Menu? Menu { get; private set; }

        private readonly List<Ingredient> _ingredients = [];
        /// <summary>
        /// The ingredients available to the establishment.
        /// </summary>
        public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();

        private readonly List<EstablishmentAddress> _addresses = [];
        /// <summary>
        /// The addresses of the establishment.
        /// </summary>
        public IReadOnlyList<EstablishmentAddress> Addresses => _addresses.AsReadOnly();

        private readonly List<User> _users = [];
        /// <summary>
        /// The users associated with the establishment.
        /// </summary>
        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private Establishment(StringBounded name, BankAccount businessAccount)
        {
            Name = name;
            BusinessAccount = businessAccount;
        }

        /// <summary>
        /// Creates a new <see cref="Establishment"/> instance.
        /// </summary>
        /// <param name="name">The name of the establishment. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="businessAccount">The bank account of the establishment. Must not be null, empty, and must consist of exactly <see cref="BankAccount.LENGTH"/> digits.</param>
        /// <returns>A new <see cref="Establishment"/> instance.</returns>
        public static Establishment Create(StringBounded name, BankAccount businessAccount) => new(name, businessAccount);

        /// <summary>
        /// Changes the name of the establishment.
        /// </summary>
        /// <param name="newName">The new name to assign to the establishment.</param>
        public void ChangeName(StringBounded newName) => Name = newName;

        /// <summary>
        /// Changes the bank account of the establishment.
        /// </summary>
        /// <param name="newBusinessAccount">The new bank account to assign to the establishment.</param>
        public void ChangeBusinessAccount(BankAccount newBusinessAccount) => BusinessAccount = newBusinessAccount;

        /// <summary>
        /// Creates the menu for the establishment.
        /// </summary>
        public void CreateMenu() => Menu = Menu.Create(Id);

        /// <summary>
        /// Creates an ingredient for the establishment.
        /// </summary>
        /// <param name="name">The name of the ingredient. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        /// <param name="imageUrl">If provided, must not be empty or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateIngredient(StringBounded name, StringBounded? imageUrl = null)
        {
            var ingredient = Ingredient.Create(Id, name, imageUrl);
            _ingredients.Add(ingredient);
        }

        /// <summary>
        /// Removes an ingredient from the establishment.
        /// </summary>
        /// <param name="ingredient">The ingredient to remove.</param>
        public void RemoveIngredient(Ingredient ingredient) => _ingredients.Remove(ingredient);

        /// <summary>
        /// Toggles the active state of the ingredient with the specified identifier.
        /// </summary>
        /// <param name="ingredientId">The identifier of the ingredient to change visibility for.</param>
        public void ChangeIngredientVisibility(Guid ingredientId)
        {
            var ingredient = _ingredients.Find(i => i.Id == ingredientId);
            ingredient?.ChangeVisibility();
        }

        /// <summary>
        /// Adds an address to the establishment.
        /// </summary>
        /// <param name="address">The address of the establishment. Must not be null, empty, or exceed <see cref="StringBounded.MAX_LENGTH"/> characters.</param>
        public void CreateAddress(StringBounded address) => _addresses.Add(EstablishmentAddress.Create(Id, address));

        /// <summary>
        /// Removes an address from the establishment.
        /// </summary>
        /// <param name="address">The address to remove.</param>
        public void RemoveAddress(EstablishmentAddress address) => _addresses.Remove(address);

        /// <summary>
        /// Adds a user to the establishment.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user) => _users.Add(user);

        /// <summary>
        /// Removes a user from the establishment.
        /// </summary>
        /// <param name="user">The user to remove.</param>
        public void RemoveUser(User user) => _users.Remove(user);
    }
}
