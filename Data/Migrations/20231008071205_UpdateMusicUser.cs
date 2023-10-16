using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMusicUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser");

            migrationBuilder.AlterColumn<int>(
                name: "MusicId",
                table: "MusicUser",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "MusicCode",
                table: "MusicUser",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser");

            migrationBuilder.DropColumn(
                name: "MusicCode",
                table: "MusicUser");

            migrationBuilder.AlterColumn<int>(
                name: "MusicId",
                table: "MusicUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicUser_Music_MusicId",
                table: "MusicUser",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
