namespace DeliveryService.Domain.Common
{
    /// <summary>
    /// Defines the permissions available in the system.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// Describes a permission's system name and display name.
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
            public const string Delete = "roles.delete";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр ролей"),
                new(Create, "Создание ролей"),
                new(Update, "Редактирование ролей"),
                new(Delete, "Удаление ролей"),
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
            public const string Delete = "users.delete";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр пользователей"),
                new(Create, "Создание пользователей"),
                new(Update, "Редактирование пользователей"),
                new(Delete, "Удаление пользователей"),
            ];
        }

        /// <summary>
        /// Permissions related to access management.
        /// </summary>
        public static class Access
        {
            public const string ManageUserRoles = "access.manage_user_roles";
            public const string ManageRolePermissions = "access.manage_role_permissions";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(ManageUserRoles, "Управление ролями пользователей"),
                new(ManageRolePermissions, "Управление разрешениями ролей"),
            ];
        }

        /// <summary>
        ///  Permissions related to establishment management.
        /// </summary>
        public static class Establishment
        {
            public const string Read = "establishment.read";
            public const string Create = "establishment.create";
            public const string Update = "establishment.update";

            /// <summary>
            /// The catalog of establishment definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр заведений"),
                new(Create, "Создание заведений"),
                new(Update, "Редактирование заведений"),
            ];
        }

        /// <summary>
        /// The catalog of all permission definitions in the system.
        /// </summary>
        public static readonly Definition[] All =
            [.. Self.Catalog, .. Roles.Catalog, .. Users.Catalog, .. Access.Catalog, .. Establishment.Catalog];
    }
}
