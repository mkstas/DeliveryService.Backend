using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_restaurants_RestaurantId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "users",
                newName: "restaurant_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_RestaurantId",
                table: "users",
                newName: "IX_users_restaurant_id");

            migrationBuilder.RenameColumn(
                name: "IsBuiltIn",
                table: "roles",
                newName: "is_built_in");

            migrationBuilder.AddForeignKey(
                name: "FK_users_restaurants_restaurant_id",
                table: "users",
                column: "restaurant_id",
                principalTable: "restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_restaurants_restaurant_id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "restaurant_id",
                table: "users",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_users_restaurant_id",
                table: "users",
                newName: "IX_users_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "is_built_in",
                table: "roles",
                newName: "IsBuiltIn");

            migrationBuilder.AddForeignKey(
                name: "FK_users_restaurants_RestaurantId",
                table: "users",
                column: "RestaurantId",
                principalTable: "restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
