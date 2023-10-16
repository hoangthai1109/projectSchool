using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSecondDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewerSong",
                table: "MusicUser");

            migrationBuilder.DropColumn(
                name: "isArtist",
                table: "MusicUser");

            migrationBuilder.RenameColumn(
                name: "MusicCode",
                table: "MusicUser",
                newName: "MusicUserCode");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistType",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistType",
                table: "Playlist",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "MusicType",
                table: "Music",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "MusicStatus",
                table: "Music",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MusicForUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicUserCode = table.Column<string>(type: "TEXT", nullable: true),
                    MusicCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicForUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicForUsers");

            migrationBuilder.DropColumn(
                name: "MusicStatus",
                table: "Music");

            migrationBuilder.RenameColumn(
                name: "MusicUserCode",
                table: "MusicUser",
                newName: "MusicCode");

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistType",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistType",
                table: "Playlist",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewerSong",
                table: "MusicUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isArtist",
                table: "MusicUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MusicType",
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
