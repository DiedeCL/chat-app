using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityPe.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversations_ConversationId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversation_Conversations_ConversationId",
                table: "UserConversation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversation_Users_UserId",
                table: "UserConversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConversation",
                table: "UserConversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "UserConversation",
                newName: "UserConversations");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversation_ConversationId",
                table: "UserConversations",
                newName: "IX_UserConversations_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ConversationId",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConversations",
                table: "UserConversations",
                columns: new[] { "UserId", "ConversationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Users_UserId",
                table: "UserConversations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Users_UserId",
                table: "UserConversations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConversations",
                table: "UserConversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "UserConversations",
                newName: "UserConversation");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversations_ConversationId",
                table: "UserConversation",
                newName: "IX_UserConversation_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "Message",
                newName: "IX_Message_ConversationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConversation",
                table: "UserConversation",
                columns: new[] { "UserId", "ConversationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversations_ConversationId",
                table: "Message",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversation_Conversations_ConversationId",
                table: "UserConversation",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversation_Users_UserId",
                table: "UserConversation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
