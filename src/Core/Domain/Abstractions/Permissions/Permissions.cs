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
            /// <summary>
            /// The permission to read permissions.
            /// </summary>
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
            /// <summary>
            /// The permission to create roles.
            /// </summary>
            public const string Create = "roles.create";

            /// <summary>
            /// The permission to read roles.
            /// </summary>
            public const string Read = "roles.read";

            /// <summary>
            /// The permission to update roles.
            /// </summary>
            public const string Update = "roles.update";

            /// <summary>
            /// The permission to delete roles.
            /// </summary>
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
            /// <summary>
            /// The permission to create users.
            /// </summary>
            public const string Create = "users.create";

            /// <summary>
            /// The permission to read users.
            /// </summary>
            public const string Read = "users.read";

            /// <summary>
            /// The permission to update users.
            /// </summary>
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
            /// <summary>
            /// The permission to create restaurants.
            /// </summary>
            public const string Create = "restaurants.create";

            /// <summary>
            /// The permission to read restaurants.
            /// </summary>
            public const string Read = "restaurants.read";

            /// <summary>
            /// The permission to update restaurants.
            /// </summary>
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
            /// <summary>
            /// The permission to create categories.
            /// </summary>
            public const string Create = "categories.create";

            /// <summary>
            /// The permission to read categories.
            /// </summary>
            public const string Read = "categories.read";

            /// <summary>
            /// The permission to update categories.
            /// </summary>
            public const string Update = "categories.update";

            /// <summary>
            /// The permission to delete categories.
            /// </summary>
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
            /// <summary>
            /// The permission to create specifications.
            /// </summary>
            public const string Create = "specifications.create";

            /// <summary>
            /// The permission to read specifications.
            /// </summary>
            public const string Read = "specifications.read";

            /// <summary>
            /// The permission to update specifications.
            /// </summary>
            public const string Update = "specifications.update";

            /// <summary>
            /// The permission to delete specifications.
            /// </summary>
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
        /// Permissions related to dish management.
        /// </summary>
        public static class Dishes
        {
            /// <summary>
            /// The permission to create dishes.
            /// </summary>
            public const string Create = "dishes.create";

            /// <summary>
            /// The permission to read dishes.
            /// </summary>
            public const string Read = "dishes.read";

            /// <summary>
            /// The permission to update dishes.
            /// </summary>
            public const string Update = "dishes.update";

            /// <summary>
            /// The permission to delete dishes.
            /// </summary>
            public const string Delete = "dishes.delete";

            /// <summary>
            /// The catalog of dish definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание блюд"),
                new(Read, "Чтение блюд"),
                new(Update, "Обновление блюд"),
                new(Delete, "Удаление блюд"),
            ];
        }

        /// <summary>
        /// Permissions related to dish specification management.
        /// </summary>
        public static class DishSpecifications
        {
            /// <summary>
            /// The permission to create dish specifications.
            /// </summary>
            public const string Create = "dish-specifications.create";

            /// <summary>
            /// The permission to read dish specifications.
            /// </summary>
            public const string Read = "dish-specifications.read";

            /// <summary>
            /// The permission to update dish specifications.
            /// </summary>
            public const string Update = "dish-specifications.update";

            /// <summary>
            /// The permission to delete dish specifications.
            /// </summary>
            public const string Delete = "dish-specifications.delete";

            /// <summary>
            /// The catalog of dish specification definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание характеристик блюд"),
                new(Read, "Чтение характеристик блюд"),
                new(Update, "Обновление характеристик блюд"),
                new(Delete, "Удаление характеристик блюд"),
            ];
        }

        /// <summary>
        /// Permissions related to cart item management.
        /// </summary>
        public static class CartItems
        {
            /// <summary>
            /// The permission to create cart items.
            /// </summary>
            public const string Create = "cart-items.create";

            /// <summary>
            /// The permission to read cart items.
            /// </summary>
            public const string Read = "cart-items.read";

            /// <summary>
            /// The permission to update cart items.
            /// </summary>
            public const string Update = "cart-items.update";

            /// <summary>
            /// The permission to delete cart items.
            /// </summary>
            public const string Delete = "cart-items.delete";

            /// <summary>
            /// The catalog of cart item definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Добавление в корзину"),
                new(Read, "Чтение корзины"),
                new(Update, "Обновление корзины"),
                new(Delete, "Удаление из корзины"),
            ];
        }

        /// <summary>
        /// Permissions related to order management.
        /// </summary>
        public static class Orders
        {
            /// <summary>
            /// The permission to create orders.
            /// </summary>
            public const string Create = "orders.create";

            /// <summary>
            /// The permission to read orders.
            /// </summary>
            public const string Read = "orders.read";

            /// <summary>
            /// The permission to update orders.
            /// </summary>
            public const string Update = "orders.update";

            /// <summary>
            /// The permission to change order status.
            /// </summary>
            public const string UpdateStatus = "orders.update-status";

            /// <summary>
            /// The permission to delete orders.
            /// </summary>
            public const string Delete = "orders.delete";

            /// <summary>
            /// The catalog of order definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Create, "Создание заказов"),
                new(Read, "Чтение заказов"),
                new(Update, "Обновление заказов"),
                new(UpdateStatus, "Изменение статуса заказов"),
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
            /// <summary>
            /// The permissions granted to administrators.
            /// </summary>
            public static readonly string[] Administrator =
            [
                .. All.Select(d => d.SystemName).Except([Orders.Create, Orders.Update, Orders.Delete]),
            ];

            /// <summary>
            /// The permissions granted to clients.
            /// </summary>
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

            /// <summary>
            /// The permissions granted to managers.
            /// </summary>
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
                Orders.UpdateStatus,
            ];
        }
    }
}
