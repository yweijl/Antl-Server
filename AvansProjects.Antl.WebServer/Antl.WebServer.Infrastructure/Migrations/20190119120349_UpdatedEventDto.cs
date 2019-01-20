using Microsoft.EntityFrameworkCore.Migrations;

namespace Antl.WebServer.Infrastructure.Migrations
{
    public partial class UpdatedEventDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventOwnerId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventOwnerId",
                table: "Events",
                column: "EventOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_EventOwnerId",
                table: "Events",
                column: "EventOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_EventOwnerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventOwnerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventOwnerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Events");
        }
    }
}
