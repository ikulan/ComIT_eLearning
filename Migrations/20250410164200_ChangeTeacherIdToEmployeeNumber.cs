using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComIT_eLearning.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTeacherIdToEmployeeNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherProfiles");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "TeacherProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "TeacherProfiles");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "TeacherProfiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
