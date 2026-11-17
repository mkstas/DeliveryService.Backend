using System.Text.Json.Serialization;
using Domain.Abstractions;
using Domain.Helpers;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a specification within the system.
    /// </summary>
    public class Specification : BaseEntity
    {
        /// <summary>
        /// The name of the specification.
        /// </summary>
        public string Name { get; private set; }

        [JsonConstructor]
        private Specification(string name) => Name = name;

        /// <summary>
        /// Creates a new <see cref="Specification"/> instance.
        /// </summary>
        /// <param name="name">Must not be null or empty.</param>
        /// <returns>A new <see cref="Specification"/> instance.</returns>
        public static Specification Create(string name)
        {
            ValidationHelper.CheckNotNullOrEmpty(name, nameof(name));

            return new(name);
        }

        /// <summary>
        /// Changes the name of the specification.
        /// </summary>
        /// <param name="newName">The new name to assign to the specification.</param>
        public void ChangeName(string newName) => Name = newName;
    }
}
