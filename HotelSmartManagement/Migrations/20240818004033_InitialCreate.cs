using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountNo = table.Column<int>(type: "int", nullable: false),
                    BankAccountBSB = table.Column<int>(type: "int", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    JobHoursPerWeek = table.Column<int>(type: "int", nullable: true),
                    JobActualHoursThisWeek = table.Column<int>(type: "int", nullable: false),
                    JobPayPerHour = table.Column<double>(type: "float", nullable: false),
                    LeaveBalanceInHours = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.UniqueId);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrgencyLevel = table.Column<int>(type: "int", nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.UniqueId);
                    table.ForeignKey(
                        name: "FK_Job_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "UniqueId");
                    table.ForeignKey(
                        name: "FK_Job_Users_ClosedById",
                        column: x => x.ClosedById,
                        principalTable: "Users",
                        principalColumn: "UniqueId");
                    table.ForeignKey(
                        name: "FK_Job_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.UniqueId);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_EmployeeDetails_EmployeeDetailsId",
                        column: x => x.EmployeeDetailsId,
                        principalTable: "EmployeeDetails",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_UserId",
                table: "EmployeeDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_AssignedToId",
                table: "Job",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ClosedById",
                table: "Job",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CreatedById",
                table: "Job",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeDetailsId",
                table: "LeaveRequests",
                column: "EmployeeDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
