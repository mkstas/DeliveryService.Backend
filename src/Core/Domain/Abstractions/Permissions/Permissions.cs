namespace Domain.Abstractions.Permissions
{
    /// <summary>
    /// Defines the permissions available in the system.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// Describes a permission system name and display name.
        /// </summary>
        /// <param name="SystemName">The internal system name of the permission.</param>
        /// <param name="DisplayName">The display name of the permission.</param>
        public sealed record Definition(string SystemName, string DisplayName);

        /// <summary>
        /// Permissions related to permission management itself.
        /// </summary>
        public static class Self
        {
            public const string Read = "permissions.read";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Чтение разрешений"),
            ];
        }

        /// <summary>
        /// Permissions related to role management.
        /// </summary>
        public static class Roles
        {
            public const string Create = "roles.create";
            public const string Read = "roles.read";
            public const string Update = "roles.update";
            public const string Delete = "roles.delete";

            /// <summary>
            /// The catalog of role definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание ролей"),
                new(Read, "Чтение ролей"),
                new(Update, "Обновление ролей"),
                new(Delete, "Удаление ролей"),
            ];
        }

        /// <summary>
        /// Permissions related to user management.
        /// </summary>
        public static class Users
        {
            public const string Create = "users.create";
            public const string Read = "users.read";
            public const string Update = "users.update";

            /// <summary>
            /// The catalog of user definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание пользователей"),
                new(Read, "Чтение пользователей"),
                new(Update, "Обновление пользователей"),
            ];
        }

        /// <summary>
        /// Permissions related to restaurant management.
        /// </summary>
        public static class Restaurants
        {
            public const string Create = "restaurants.create";
            public const string Read = "restaurants.read";
            public const string Update = "restaurants.update";

            /// <summary>
            /// The catalog of restaurant definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание ресторанов"),
                new(Read, "Чтение ресторанов"),
                new(Update, "Обновление ресторанов"),
            ];
        }

        /// <summary>
        /// Permissions related to category management.
        /// </summary>
        public static class Categories
        {
            public const string Create = "categories.create";
            public const string Read = "categories.read";
            public const string Update = "categories.update";
            public const string Delete = "categories.delete";

            /// <summary>
            /// The catalog of category definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание категорий"),
                new(Read, "Чтение категорий"),
                new(Update, "Обновление категорий"),
                new(Delete, "Удаление категорий"),
            ];
        }

        /// <summary>
        /// Permissions related to specification management.
        /// </summary>
        public static class Specifications
        {
            public const string Create = "specifications.create";
            public const string Read = "specifications.read";
            public const string Update = "specifications.update";
            public const string Delete = "specifications.delete";

            /// <summary>
            /// The catalog of specification definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание характеристик"),
                new(Read, "Чтение характеристик"),
                new(Update, "Обновление характеристик"),
                new(Delete, "Удаление характеристик"),
            ];
        }

        /// <summary>
        /// The catalog of all permission definitions in the system.
        /// </summary>
        public static readonly Definition[] All = [.. Self.Catalog, .. Roles.Catalog, .. Users.Catalog, .. Restaurants.Catalog,
            .. Categories.Catalog, ..Specifications.Catalog];
    }
}
