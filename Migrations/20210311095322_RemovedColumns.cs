using Microsoft.EntityFrameworkCore.Migrations;

namespace RentApp.Migrations
{
    public partial class RemovedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "RentItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "RentItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
