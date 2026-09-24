using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class CompositePkForEmpSchadule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSchedules_EmployeeId",
                table: "EmployeeSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules",
                columns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_EmployeeId",
                table: "EmployeeSchedules",
                column: "EmployeeId");
        }
    }
}
