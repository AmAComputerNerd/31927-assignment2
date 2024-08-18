using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemovePFPAndAddVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureFileName",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureFileName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
