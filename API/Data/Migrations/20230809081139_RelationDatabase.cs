using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qa_appUsers_AppUserid",
                table: "Qa");

            migrationBuilder.RenameColumn(
                name: "AppUserid",
                table: "Qa",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Qa_AppUserid",
                table: "Qa",
                newName: "IX_Qa_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Qa",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Qa_appUsers_AppUserId",
                table: "Qa",
                column: "AppUserId",
                principalTable: "appUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qa_appUsers_AppUserId",
                table: "Qa");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Qa",
                newName: "AppUserid");

            migrationBuilder.RenameIndex(
                name: "IX_Qa_AppUserId",
                table: "Qa",
                newName: "IX_Qa_AppUserid");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserid",
                table: "Qa",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Qa_appUsers_AppUserid",
                table: "Qa",
                column: "AppUserid",
                principalTable: "appUsers",
                principalColumn: "id");
        }
    }
}
