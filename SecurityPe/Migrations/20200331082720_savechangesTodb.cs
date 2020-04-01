using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityPe.Migrations
{
    public partial class savechangesTodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOfSender",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Md5Hash",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "EmailOfSender",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignedData",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOfSender",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SignedData",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "IdOfSender",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Md5Hash",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
