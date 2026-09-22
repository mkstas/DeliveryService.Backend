using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DeliveryService.Persistence.Postgres
{
    public static class PersistencePostgresExtensions
    {
        /// <summary>
        /// Determines whether a <see cref="DbUpdateException"/> was caused by a PostgreSQL unique-constraint violation.
        /// </summary>
        /// <param name="ex">The database update exception to inspect.</param>
        /// <returns><see langword="true"/> if a unique constraint was violated; otherwise <see langword="false"/>.</returns>
        public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
        {
            return ex.InnerException is PostgresException pg &&
                pg.SqlState == PostgresErrorCodes.UniqueViolation;
        }
    }
}
