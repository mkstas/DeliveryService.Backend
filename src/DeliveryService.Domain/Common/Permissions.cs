namespace DeliveryService.Domain.Common
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
                new(Read, "Просмотр разрешений"),
            ];
        }

        /// <summary>
        /// Permissions related to role management.
        /// </summary>
        public static class Roles
        {
            public const string Read = "roles.read";
            public const string Create = "roles.create";
            public const string Update = "roles.update";

            /// <summary>
            /// The catalog of role definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр ролей"),
                new(Create, "Создание ролей"),
                new(Update, "Редактирование ролей"),
            ];
        }

        /// <summary>
        /// Permissions related to user management.
        /// </summary>
        public static class Users
        {
            public const string Read = "users.read";
            public const string Create = "users.create";
            public const string Update = "users.update";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр пользователей"),
                new(Create, "Создание пользователей"),
                new(Update, "Редактирование пользователей"),
            ];
        }

        /// <summary>
        ///  Permissions related to establishment management.
        /// </summary>
        public static class Establishment
        {
            public const string Create = "establishment.create";
            public const string Update = "establishment.update";

            /// <summary>
            /// The catalog of establishment definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание заведений"),
                new(Update, "Редактирование заведений"),
            ];
        }

        /// <summary>
        ///  Permissions related to category management.
        /// </summary>
        public static class Categories
        {
            public const string Create = "categories.create";
            public const string Update = "categories.update";

            /// <summary>
            /// The catalog of category definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание категорий"),
                new(Update, "Редактирование категорий"),
            ];
        }

        /// <summary>
        ///  Permissions related to specification management.
        /// </summary>
        public static class Specifications
        {
            public const string Create = "specifications.create";
            public const string Update = "specifications.update";

            /// <summary>
            /// The catalog of specification definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание характерстик"),
                new(Update, "Редактирование характерстик"),
            ];
        }

        /// <summary>
        /// Permissions related to ingredient management.
        /// </summary>
        public static class Ingredients
        {
            public const string Create = "ingredients.create";
            public const string Update = "ingredients.update";

            /// <summary>
            /// The catalog of ingredient definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание ингредиентов"),
                new(Update, "Редактирование ингредиентов"),
            ];
        }

        /// <summary>
        /// Permissions related to modifier management.
        /// </summary>
        public static class Modifiers
        {
            public const string Create = "modifier.create";
            public const string Update = "modifier.update";

            /// <summary>
            /// The catalog of modifier definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание модификаторов"),
                new(Update, "Редактирование модификаторов"),
            ];
        }

        /// <summary>
        /// The catalog of all permission definitions in the system.
        /// </summary>
        public static readonly Definition[] All =
            [.. Self.Catalog, .. Roles.Catalog, .. Users.Catalog, .. Establishment.Catalog, .. Categories.Catalog, ..Specifications.Catalog,
                .. Ingredients.Catalog, .. Modifiers.Catalog];
    }
}
