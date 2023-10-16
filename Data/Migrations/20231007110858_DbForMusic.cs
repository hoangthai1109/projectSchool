using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbForMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "appUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleDefault",
                table: "appUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    owner = table.Column<string>(type: "TEXT", nullable: true),
                    isMain = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    isAlbum = table.Column<int>(type: "INTEGER", nullable: false),
                    Album = table.Column<string>(type: "TEXT", nullable: true),
                    TotalSong = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubcribeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PakageType = table.Column<int>(type: "INTEGER", nullable: false),
                    PakageDescript = table.Column<string>(type: "TEXT", nullable: true),
                    PakageValue = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppUserid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcribeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubcribeItem_appUsers_AppUserid",
                        column: x => x.AppUserid,
                        principalTable: "appUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdBy = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMusic = table.Column<int>(type: "INTEGER", nullable: false),
                    MusicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayListId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFolder = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    MadeBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicUser_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_MusicId",
                table: "Cart",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicUser_MusicId",
                table: "MusicUser",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcribeItem_AppUserid",
                table: "SubcribeItem",
                column: "AppUserid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "MusicUser");

            migrationBuilder.DropTable(
                name: "SubcribeItem");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "RoleDefault",
                table: "appUsers");
        }
    }
}
