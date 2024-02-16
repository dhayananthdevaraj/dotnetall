using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sqewgrsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Products_ProductID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Bookings",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ProductID",
                table: "Bookings",
                newName: "IX_Bookings_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Products_ProductId",
                table: "Bookings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Products_ProductId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Bookings",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ProductId",
                table: "Bookings",
                newName: "IX_Bookings_ProductID");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings",
                column: "OrderID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Products_ProductID",
                table: "Bookings",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
