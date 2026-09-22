using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DeliveryService.Persistence.Postgres
{
    /// <summary>
    /// Provides extension methods for the PostgreSQL persistence layer.
    /// </summary>
    public static class PersistencePostgresExtensions
    {
        /// <summary>
        /// Determines whether a <see cref="DbUpdateException"/> was caused by a PostgreSQL unique-constraint violation.
        /// </summary>
        /// <param name="ex">The database update exception to inspect.</param>
        /// <returns><c>true</c> if a unique constraint was violated; otherwise <c>false</c>.</returns>
        public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
        {
            return ex.InnerException is PostgresException pg &&
                pg.SqlState == PostgresErrorCodes.UniqueViolation;
        }
    }
}
