using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityPe.Migrations
{
    public partial class addedDbsetUserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_AspNetUsers_UserId",
                table: "UserKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserKey",
                table: "UserKey");

            migrationBuilder.RenameTable(
                name: "UserKey",
                newName: "UserKeys");

            migrationBuilder.RenameIndex(
                name: "IX_UserKey_UserId",
                table: "UserKeys",
                newName: "IX_UserKeys_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserKeys",
                table: "UserKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserKeys_AspNetUsers_UserId",
                table: "UserKeys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKeys_AspNetUsers_UserId",
                table: "UserKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserKeys",
                table: "UserKeys");

            migrationBuilder.RenameTable(
                name: "UserKeys",
                newName: "UserKey");

            migrationBuilder.RenameIndex(
                name: "IX_UserKeys_UserId",
                table: "UserKey",
                newName: "IX_UserKey_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserKey",
                table: "UserKey",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_AspNetUsers_UserId",
                table: "UserKey",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
