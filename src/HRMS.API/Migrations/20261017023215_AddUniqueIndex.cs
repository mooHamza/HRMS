using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_EmployeeId_Date_ShiftId_SiteId",
                table: "EmployeeSchedules",
                columns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSchedules_EmployeeId_Date_ShiftId_SiteId",
                table: "EmployeeSchedules");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeSchedules_Id",
                table: "EmployeeSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSchedules",
                table: "EmployeeSchedules",
                columns: new[] { "EmployeeId", "Date", "ShiftId", "SiteId" });
        }
    }
}
