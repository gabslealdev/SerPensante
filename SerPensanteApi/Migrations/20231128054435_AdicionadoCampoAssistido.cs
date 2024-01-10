using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoAssistido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BOOL",
                table: "Aula",
                newName: "Assistido");

            migrationBuilder.AlterColumn<bool>(
                name: "Assistido",
                table: "Aula",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Assistido",
                table: "Aula",
                newName: "BOOL");

            migrationBuilder.AlterColumn<bool>(
                name: "BOOL",
                table: "Aula",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldDefaultValue: false);
        }
    }
}
