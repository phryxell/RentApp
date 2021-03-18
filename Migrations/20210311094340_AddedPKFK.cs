using Microsoft.EntityFrameworkCore.Migrations;

namespace RentApp.Migrations
{
    public partial class AddedPKFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_RentItems_RentItemsRentItemId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_RentItemsRentItemId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RentItemsRentItemId",
                table: "Rents");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentItemId",
                table: "Rents",
                column: "RentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_RentItems_RentItemId",
                table: "Rents",
                column: "RentItemId",
                principalTable: "RentItems",
                principalColumn: "RentItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_RentItems_RentItemId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_RentItemId",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "RentItemsRentItemId",
                table: "Rents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentItemsRentItemId",
                table: "Rents",
                column: "RentItemsRentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_RentItems_RentItemsRentItemId",
                table: "Rents",
                column: "RentItemsRentItemId",
                principalTable: "RentItems",
                principalColumn: "RentItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
