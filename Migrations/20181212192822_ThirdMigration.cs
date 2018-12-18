using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Customers",
                newName: "SupplierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                newName: "IX_Customers_SupplierUserId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerUserId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerUserId",
                table: "Customers",
                column: "CustomerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_CustomerUserId",
                table: "Customers",
                column: "CustomerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_SupplierUserId",
                table: "Customers",
                column: "SupplierUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_CustomerUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_SupplierUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerUserId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "SupplierUserId",
                table: "Customers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_SupplierUserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
