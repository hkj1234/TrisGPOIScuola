using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrisGPOI.Migrations
{
    /// <inheritdoc />
    public partial class Version006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNumber",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusNumber",
                table: "Users");
        }
    }
}
