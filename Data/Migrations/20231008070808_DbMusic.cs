using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePlaylist",
                table: "Playlist",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MusicCode",
                table: "Music",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "musicName",
                table: "Music",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePlaylist",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "musicName",
                table: "Music");

            migrationBuilder.AlterColumn<int>(
                name: "MusicCode",
                table: "Music",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
