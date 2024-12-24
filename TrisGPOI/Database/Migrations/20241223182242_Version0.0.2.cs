using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrisGPOI.Migrations
{
    /// <inheritdoc />
    public partial class Version002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FotoProfilo",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserVittoriePVP",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(95)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VictoryNormal = table.Column<int>(type: "int", nullable: false),
                    LossesNormal = table.Column<int>(type: "int", nullable: false),
                    GameNormal = table.Column<int>(type: "int", nullable: false),
                    VictoryInfinity = table.Column<int>(type: "int", nullable: false),
                    LossesInfinity = table.Column<int>(type: "int", nullable: false),
                    GameInfinity = table.Column<int>(type: "int", nullable: false),
                    VictoryUltimate = table.Column<int>(type: "int", nullable: false),
                    LossesUltimate = table.Column<int>(type: "int", nullable: false),
                    GameUltimate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVittoriePVP", x => x.Email);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVittoriePVP");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FotoProfilo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");
        }
    }
}
