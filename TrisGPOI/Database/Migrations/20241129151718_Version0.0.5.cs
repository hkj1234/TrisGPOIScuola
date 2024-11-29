using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrisGPOI.Migrations
{
    /// <inheritdoc />
    public partial class Version005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OPT",
                table: "OPT");

            migrationBuilder.RenameTable(
                name: "OPT",
                newName: "OTP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OTP",
                table: "OTP",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OTP",
                table: "OTP");

            migrationBuilder.RenameTable(
                name: "OTP",
                newName: "OPT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OPT",
                table: "OPT",
                column: "Email");
        }
    }
}
