using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class TrocandoTipoDoCampoMateria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Materia",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Tipo",
                table: "Materia",
                type: "TINYINT",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)",
                oldMaxLength: 20);
        }
    }
}
