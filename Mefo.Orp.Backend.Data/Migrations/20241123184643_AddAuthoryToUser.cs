using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mefo.Orp.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthoryToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Authority",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "Users");
        }
    }
}
