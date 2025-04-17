using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComIT_eLearning.Migrations
{
    /// <inheritdoc />
    public partial class InvitationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationRole",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsInvitationUsed",
                table: "AspNetUsers",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AspNetUsers",
                newName: "IsInvitationUsed");

            migrationBuilder.AddColumn<string>(
                name: "InvitationRole",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
