using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class CriandoCampoAssistido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BOOL",
                table: "Aula",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOOL",
                table: "Aula");
        }
    }
}
