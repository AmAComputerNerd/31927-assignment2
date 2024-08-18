using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToDateAffected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Events",
                newName: "DateAffected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAffected",
                table: "Events",
                newName: "DateCreated");
        }
    }
}
