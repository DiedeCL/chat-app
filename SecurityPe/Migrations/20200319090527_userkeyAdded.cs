using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityPe.Migrations
{
    public partial class userkeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PrivateKey",
                table: "UserKey",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PublicKey",
                table: "UserKey",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "UserKey");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "UserKey");
        }
    }
}
