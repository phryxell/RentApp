using Microsoft.EntityFrameworkCore.Migrations;

namespace RentApp.Migrations
{
    public partial class AddedFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_RentItems_RentItemsRentItemId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RentItemsRentItemId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RentItemsRentItemId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RentItemId",
                table: "Ratings",
                column: "RentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_RentItems_RentItemId",
                table: "Ratings",
                column: "RentItemId",
                principalTable: "RentItems",
                principalColumn: "RentItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_RentItems_RentItemId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RentItemId",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "RentItemsRentItemId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RentItemsRentItemId",
                table: "Ratings",
                column: "RentItemsRentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_RentItems_RentItemsRentItemId",
                table: "Ratings",
                column: "RentItemsRentItemId",
                principalTable: "RentItems",
                principalColumn: "RentItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
