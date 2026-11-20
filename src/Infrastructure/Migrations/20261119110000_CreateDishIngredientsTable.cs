using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDishIngredientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dish_ingredients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish_ingredients", x => x.id);
                    table.ForeignKey(
                        name: "FK_dish_ingredients_dishes_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dish_ingredients_ingredients_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dish_ingredients_dish_id_ingredient_id",
                table: "dish_ingredients",
                columns: new[] { "dish_id", "ingredient_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dish_ingredients_ingredient_id",
                table: "dish_ingredients",
                column: "ingredient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dish_ingredients");
        }
    }
}
