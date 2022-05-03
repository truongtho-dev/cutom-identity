using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomIdentity.Migrations
{
    public partial class add_assign_to : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignTo",
                schema: "Identity",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignTo",
                schema: "Identity",
                table: "Jobs");
        }
    }
}
