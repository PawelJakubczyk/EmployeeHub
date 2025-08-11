using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_colums_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Teams",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Teams",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");
        }
    }
}
