using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestShiftSwap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftSwapRequests",
                columns: table => new
                {
                    FromEmployeeScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmployeeScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromEmployeeScheduleEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromEmployeeScheduleDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FromEmployeeScheduleShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromEmployeeScheduleSiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmployeeScheduleEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmployeeScheduleDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToEmployeeScheduleShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmployeeScheduleSiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSwapRequests", x => new { x.FromEmployeeScheduleId, x.ToEmployeeScheduleId });
                    table.ForeignKey(
                        name: "FK_ShiftSwapRequests_EmployeeSchedules_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_From~",
                        columns: x => new { x.FromEmployeeScheduleEmployeeId, x.FromEmployeeScheduleDate, x.FromEmployeeScheduleShiftId, x.FromEmployeeScheduleSiteId },
                        principalTable: "EmployeeSchedules",
                        principalColumns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShiftSwapRequests_EmployeeSchedules_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployee~",
                        columns: x => new { x.ToEmployeeScheduleEmployeeId, x.ToEmployeeScheduleDate, x.ToEmployeeScheduleShiftId, x.ToEmployeeScheduleSiteId },
                        principalTable: "EmployeeSchedules",
                        principalColumns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwapRequests_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_FromEmployeeScheduleSi~",
                table: "ShiftSwapRequests",
                columns: new[] { "FromEmployeeScheduleEmployeeId", "FromEmployeeScheduleDate", "FromEmployeeScheduleShiftId", "FromEmployeeScheduleSiteId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwapRequests_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployeeScheduleSiteId",
                table: "ShiftSwapRequests",
                columns: new[] { "ToEmployeeScheduleEmployeeId", "ToEmployeeScheduleDate", "ToEmployeeScheduleShiftId", "ToEmployeeScheduleSiteId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftSwapRequests");
        }
    }
}
