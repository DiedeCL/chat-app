using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityPe.Migrations
{
    public partial class changedDomeinCLasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConversations");

            migrationBuilder.DropColumn(
                name: "ContentOfMessage",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "PublicKey",
                table: "UserKeys",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrivateKey",
                table: "UserKeys",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptedAesIV",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptedAesKey",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptedContentOfMessage",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Md5Hash",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Conversations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserOneId",
                table: "Conversations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTwoId",
                table: "Conversations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_UserId",
                table: "Conversations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_UserId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "EncryptedAesIV",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EncryptedAesKey",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EncryptedContentOfMessage",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Md5Hash",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "UserOneId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "UserTwoId",
                table: "Conversations");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PublicKey",
                table: "UserKeys",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PrivateKey",
                table: "UserKeys",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentOfMessage",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserConversations",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversations", x => new { x.UserId, x.ConversationId });
                    table.ForeignKey(
                        name: "FK_UserConversations_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConversations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId");
        }
    }
}
