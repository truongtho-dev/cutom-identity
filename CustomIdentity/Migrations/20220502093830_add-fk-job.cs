using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomIdentity.Migrations
{
    public partial class addfkjob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_User_ApplicationUserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ApplicationUserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                schema: "Identity",
                table: "Jobs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_User_UserId",
                schema: "Identity",
                table: "Jobs",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_User_UserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_UserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ApplicationUserId",
                schema: "Identity",
                table: "Jobs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_User_ApplicationUserId",
                schema: "Identity",
                table: "Jobs",
                column: "ApplicationUserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
