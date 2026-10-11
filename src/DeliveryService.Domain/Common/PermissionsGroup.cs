namespace DeliveryService.Domain.Common
{
    /// <summary>
    /// Defines the permissions available in the system.
    /// </summary>
    public static class PermissionsGroup
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
        public static class Permissions
        {
            public const string Read = "permissions.read";
            public const string Update = "permissions.update";

            /// <summary>
            /// The catalog of permission definitions.
            /// </summary>
            public static readonly Definition[] Catalog =
            [
                new(Read, "Просмотр разрешений"),
                new(Update, "Редактирование разрешений"),
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
        /// The catalog of all permission definitions in the system.
        /// </summary>
        public static readonly Definition[] All =
            [.. Permissions.Catalog, .. Roles.Catalog, .. Users.Catalog, .. Access.Catalog];
    }
}
