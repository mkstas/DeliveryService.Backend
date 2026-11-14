namespace Domain.Permissions
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
            public const string Read = "roles.read";
            public const string Create = "roles.create";
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

        public static readonly Definition[] All = [.. Self.Catalog, .. Roles.Catalog];
    }
}
