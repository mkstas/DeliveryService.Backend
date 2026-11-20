using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM cart_items;");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "cart_items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_user_id",
                table: "cart_items",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_items_users_user_id",
                table: "cart_items",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_items_users_user_id",
                table: "cart_items");

            migrationBuilder.DropIndex(
                name: "IX_cart_items_user_id",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "cart_items");
        }
    }
}
