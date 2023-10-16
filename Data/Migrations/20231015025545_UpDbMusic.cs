using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpDbMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderCode",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "isFolder",
                table: "PlaylistUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderCode",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isFolder",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
