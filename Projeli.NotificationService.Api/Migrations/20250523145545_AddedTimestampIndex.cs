using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeli.NotificationService.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedTimestampIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Timestamp",
                table: "Notifications",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_Timestamp",
                table: "Notifications");
        }
    }
}
