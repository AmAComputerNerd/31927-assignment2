using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    /// <inheritdoc />
    public partial class ExtraDbSetsAddedToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Users_AssignedToId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Users_ClosedById",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Users_CreatedById",
                table: "Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.RenameIndex(
                name: "IX_Job_CreatedById",
                table: "Jobs",
                newName: "IX_Jobs_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Job_ClosedById",
                table: "Jobs",
                newName: "IX_Jobs_ClosedById");

            migrationBuilder.RenameIndex(
                name: "IX_Job_AssignedToId",
                table: "Jobs",
                newName: "IX_Jobs_AssignedToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "UniqueId");

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.UniqueId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_AssignedToId",
                table: "Jobs",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "UniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_ClosedById",
                table: "Jobs",
                column: "ClosedById",
                principalTable: "Users",
                principalColumn: "UniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_CreatedById",
                table: "Jobs",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UniqueId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_AssignedToId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_ClosedById",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_CreatedById",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_CreatedById",
                table: "Job",
                newName: "IX_Job_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_ClosedById",
                table: "Job",
                newName: "IX_Job_ClosedById");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_AssignedToId",
                table: "Job",
                newName: "IX_Job_AssignedToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "UniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Users_AssignedToId",
                table: "Job",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "UniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Users_ClosedById",
                table: "Job",
                column: "ClosedById",
                principalTable: "Users",
                principalColumn: "UniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Users_CreatedById",
                table: "Job",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UniqueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
