using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Music_MusicId",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistUser_Music_MusicId",
                table: "PlaylistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SubcribeItem_appUsers_AppUserid",
                table: "SubcribeItem");

            migrationBuilder.DropIndex(
                name: "IX_SubcribeItem_AppUserid",
                table: "SubcribeItem");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistUser_MusicId",
                table: "PlaylistUser");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_MusicId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "AppUserid",
                table: "SubcribeItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SubcribeItem");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "Playlist");

            migrationBuilder.AddColumn<string>(
                name: "PlayListUserCode",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayListCode",
                table: "Playlist",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusicCode",
                table: "Music",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CartCode",
                table: "Cart",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "appUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MusicCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicCode = table.Column<string>(type: "TEXT", nullable: true),
                    CartCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicPlayList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicCode = table.Column<string>(type: "TEXT", nullable: true),
                    PlayListCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlayList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicPLayListUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicCode = table.Column<string>(type: "TEXT", nullable: true),
                    PlayListUserCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPLayListUser", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicCart");

            migrationBuilder.DropTable(
                name: "MusicPlayList");

            migrationBuilder.DropTable(
                name: "MusicPLayListUser");

            migrationBuilder.DropColumn(
                name: "PlayListUserCode",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "PlayListCode",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "MusicCode",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "CartCode",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "appUsers");

            migrationBuilder.AddColumn<int>(
                name: "AppUserid",
                table: "SubcribeItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SubcribeItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MusicId",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MusicId",
                table: "Playlist",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubcribeItem_AppUserid",
                table: "SubcribeItem",
                column: "AppUserid");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistUser_MusicId",
                table: "PlaylistUser",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_MusicId",
                table: "Playlist",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Music_MusicId",
                table: "Playlist",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistUser_Music_MusicId",
                table: "PlaylistUser",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubcribeItem_appUsers_AppUserid",
                table: "SubcribeItem",
                column: "AppUserid",
                principalTable: "appUsers",
                principalColumn: "id");
        }
    }
}
