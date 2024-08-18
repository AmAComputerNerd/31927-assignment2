using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedDescriptionToRoomDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");
        }
    }
}
