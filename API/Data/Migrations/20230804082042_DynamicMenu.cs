using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DynamicMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dynamicMenu",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MenuName = table.Column<string>(type: "TEXT", nullable: true),
                    MenuUri = table.Column<string>(type: "TEXT", nullable: true),
                    isParent = table.Column<int>(type: "INTEGER", nullable: false),
                    idParent = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuLv = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dynamicMenu", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dynamicMenu");
        }
    }
}
