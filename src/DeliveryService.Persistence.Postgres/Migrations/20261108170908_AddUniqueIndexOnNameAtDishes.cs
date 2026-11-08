using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnNameAtDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_dishes_name",
                table: "dishes",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_dishes_name",
                table: "dishes");
        }
    }
}
