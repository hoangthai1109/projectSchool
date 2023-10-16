using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaylistType",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDatePls",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PlaylistUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalListon",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSong",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isUserUpload",
                table: "PlaylistUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaylistType",
                table: "Playlist",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MusicType",
                table: "Music",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistType",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "ReleaseDatePls",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "TotalListon",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "TotalSong",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "isUserUpload",
                table: "PlaylistUser");

            migrationBuilder.DropColumn(
                name: "PlaylistType",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "MusicType",
                table: "Music");
        }
    }
}
