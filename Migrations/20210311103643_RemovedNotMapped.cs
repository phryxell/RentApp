using Microsoft.EntityFrameworkCore.Migrations;

namespace RentApp.Migrations
{
    public partial class RemovedNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentItems_ImageModals_ImageModalImageId",
                table: "RentItems");

            migrationBuilder.DropIndex(
                name: "IX_RentItems_ImageModalImageId",
                table: "RentItems");

            migrationBuilder.DropColumn(
                name: "ImageModalImageId",
                table: "RentItems");

            migrationBuilder.CreateIndex(
                name: "IX_RentItems_ImageId",
                table: "RentItems",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentItems_ImageModals_ImageId",
                table: "RentItems",
                column: "ImageId",
                principalTable: "ImageModals",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentItems_ImageModals_ImageId",
                table: "RentItems");

            migrationBuilder.DropIndex(
                name: "IX_RentItems_ImageId",
                table: "RentItems");

            migrationBuilder.AddColumn<int>(
                name: "ImageModalImageId",
                table: "RentItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentItems_ImageModalImageId",
                table: "RentItems",
                column: "ImageModalImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentItems_ImageModals_ImageModalImageId",
                table: "RentItems",
                column: "ImageModalImageId",
                principalTable: "ImageModals",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
