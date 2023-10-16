using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbMusic2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayListId",
                table: "MusicUser",
                newName: "isArtist");

            migrationBuilder.RenameColumn(
                name: "IsFolder",
                table: "MusicUser",
                newName: "ViewerSong");

            migrationBuilder.AddColumn<string>(
                name: "FolderCode",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlFolder",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderCode",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "ImageUrlFolder",
                table: "PlaylistUser");

            migrationBuilder.RenameColumn(
                name: "isArtist",
                table: "MusicUser",
                newName: "PlayListId");

            migrationBuilder.RenameColumn(
                name: "ViewerSong",
                table: "MusicUser",
                newName: "IsFolder");
        }
    }
}
