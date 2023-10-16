using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser");

            migrationBuilder.DropIndex(
                name: "IX_MusicUser_MusicId",
                table: "MusicUser");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "MusicUser");

            migrationBuilder.DropColumn(
                name: "TotalSong",
                table: "Music");

            migrationBuilder.AddColumn<int>(
                name: "TotalSong",
                table: "Playlist",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SongPath",
                table: "Music",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrustSongPath",
                table: "Music",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSong",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "SongPath",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "TrustSongPath",
                table: "Music");

            migrationBuilder.AddColumn<int>(
                name: "MusicId",
                table: "MusicUser",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalSong",
                table: "Music",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MusicUser_MusicId",
                table: "MusicUser",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id");
        }
    }
}
