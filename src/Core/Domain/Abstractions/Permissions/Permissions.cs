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

        public static class Dishes
        {
            public const string Create = "dishes.create";
            public const string Read = "dishes.read";
            public const string Update = "dishes.update";
            public const string Delete = "dishes.delete";

            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание блюд"),
                new(Read, "Чтение блюд"),
                new(Update, "Обновление блюд"),
                new(Delete, "Удаление блюд"),
            ];
        }

        public static class DishSpecifications
        {
            public const string Create = "dish-specifications.create";
            public const string Read = "dish-specifications.read";
            public const string Update = "dish-specifications.update";
            public const string Delete = "dish-specifications.delete";

            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание характеристик блюд"),
                new(Read, "Чтение характеристик блюд"),
                new(Update, "Обновление характеристик блюд"),
                new(Delete, "Удаление характеристик блюд"),
            ];
        }

        public static class CartItems
        {
            public const string Create = "cart-items.create";
            public const string Read = "cart-items.read";
            public const string Update = "cart-items.update";
            public const string Delete = "cart-items.delete";

            public static readonly Definition[] Catalog =
            [
                new(Create, "Добавление в корзину"),
                new(Read, "Чтение корзины"),
                new(Update, "Обновление корзины"),
                new(Delete, "Удаление из корзины"),
            ];
        }

        public static class Orders
        {
            public const string Create = "orders.create";
            public const string Read = "orders.read";
            public const string Update = "orders.update";
            public const string Delete = "orders.delete";

            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание заказов"),
                new(Read, "Чтение заказов"),
                new(Update, "Обновление заказов"),
                new(Delete, "Удаление заказов"),
            ];
        }

        /// <summary>
        /// The catalog of all permission definitions in the system.
        /// </summary>
        public static readonly Definition[] All = [.. Self.Catalog, .. Roles.Catalog, .. Users.Catalog, .. Restaurants.Catalog,
            .. Categories.Catalog, ..Specifications.Catalog, .. Dishes.Catalog, .. DishSpecifications.Catalog,
            .. CartItems.Catalog, .. Orders.Catalog];

        /// <summary>
        /// The default permission sets for the built-in roles.
        /// </summary>
        public static class DefaultSets
        {
            public static readonly string[] Administrator = [.. All.Select(d => d.SystemName)];

            public static readonly string[] Client =
            [
                Restaurants.Read,
                Categories.Read,
                Specifications.Read,
                Dishes.Read,
                DishSpecifications.Read,
                CartItems.Create,
                CartItems.Read,
                CartItems.Update,
                CartItems.Delete,
                Orders.Create,
                Orders.Read,
                Orders.Update,
                Orders.Delete,
            ];

            public static readonly string[] Manager =
            [
                Roles.Read,
                Users.Read,
                Users.Update,
                Restaurants.Read,
                Restaurants.Update,
                Categories.Create,
                Categories.Read,
                Categories.Update,
                Categories.Delete,
                Specifications.Create,
                Specifications.Read,
                Specifications.Update,
                Specifications.Delete,
                Dishes.Create,
                Dishes.Read,
                Dishes.Update,
                Dishes.Delete,
                DishSpecifications.Create,
                DishSpecifications.Read,
                DishSpecifications.Update,
                DishSpecifications.Delete,
                Orders.Read,
                Orders.Update,
                Orders.Delete,
            ];
        }
    }
}
