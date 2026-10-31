using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAlternateKeyy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_FromEmplo~",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployeeSched~",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_FromEmployeeScheduleSiteId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployeeScheduleSiteId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "FromEmployeeScheduleDate",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "FromEmployeeScheduleEmployeeId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "FromEmployeeScheduleShiftId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "FromEmployeeScheduleSiteId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ToEmployeeScheduleDate",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ToEmployeeScheduleEmployeeId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ToEmployeeScheduleShiftId",
                table: "SwapRequests");

            migrationBuilder.RenameColumn(
                name: "ToEmployeeScheduleSiteId",
                table: "SwapRequests",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ToEmployeeScheduleId",
                table: "SwapRequests",
                column: "ToEmployeeScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_FromEmployeeScheduleId",
                table: "SwapRequests",
                column: "FromEmployeeScheduleId",
                principalTable: "EmployeeSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_ToEmployeeScheduleId",
                table: "SwapRequests",
                column: "ToEmployeeScheduleId",
                principalTable: "EmployeeSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_FromEmployeeScheduleId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_ToEmployeeScheduleId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ToEmployeeScheduleId",
                table: "SwapRequests");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SwapRequests",
                newName: "ToEmployeeScheduleSiteId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FromEmployeeScheduleDate",
                table: "SwapRequests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "FromEmployeeScheduleEmployeeId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromEmployeeScheduleShiftId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromEmployeeScheduleSiteId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ToEmployeeScheduleDate",
                table: "SwapRequests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "ToEmployeeScheduleEmployeeId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToEmployeeScheduleShiftId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_FromEmployeeScheduleSiteId",
                table: "SwapRequests",
                columns: new[] { "FromEmployeeScheduleEmployeeId", "FromEmployeeScheduleDate", "FromEmployeeScheduleShiftId", "FromEmployeeScheduleSiteId" });

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployeeScheduleSiteId",
                table: "SwapRequests",
                columns: new[] { "ToEmployeeScheduleEmployeeId", "ToEmployeeScheduleDate", "ToEmployeeScheduleShiftId", "ToEmployeeScheduleSiteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_FromEmployeeScheduleEmployeeId_FromEmployeeScheduleDate_FromEmployeeScheduleShiftId_FromEmplo~",
                table: "SwapRequests",
                columns: new[] { "FromEmployeeScheduleEmployeeId", "FromEmployeeScheduleDate", "FromEmployeeScheduleShiftId", "FromEmployeeScheduleSiteId" },
                principalTable: "EmployeeSchedules",
                principalColumns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_EmployeeSchedules_ToEmployeeScheduleEmployeeId_ToEmployeeScheduleDate_ToEmployeeScheduleShiftId_ToEmployeeSched~",
                table: "SwapRequests",
                columns: new[] { "ToEmployeeScheduleEmployeeId", "ToEmployeeScheduleDate", "ToEmployeeScheduleShiftId", "ToEmployeeScheduleSiteId" },
                principalTable: "EmployeeSchedules",
                principalColumns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
