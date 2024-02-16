using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sqewg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Orders_OrderID",
                table: "Bookings",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Orders_OrderID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_OrderID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Bookings");
        }
    }
}
