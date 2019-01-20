using Microsoft.EntityFrameworkCore.Migrations;

namespace Antl.WebServer.Infrastructure.Migrations
{
    public partial class AddHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hash",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Events");
        }
    }
}
