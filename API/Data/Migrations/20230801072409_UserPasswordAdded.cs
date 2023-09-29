using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserPasswordAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hashPass",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "passCode",
                table: "appUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "appUsers",
                newName: "id");

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "appUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "appUsers",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "appUsers");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "appUsers",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "hashPass",
                table: "appUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passCode",
                table: "appUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
