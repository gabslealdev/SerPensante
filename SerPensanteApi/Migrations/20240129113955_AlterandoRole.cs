using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STUDENT_ROLE",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_TEACHER_ROLE",
                table: "Teacher");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_RoleId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Student_RoleId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Student",
                newName: "Role");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Teacher",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Student",
                newName: "RoleId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Name1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RoleId",
                table: "Teacher",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RoleId",
                table: "Student",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_STUDENT_ROLE",
                table: "Student",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEACHER_ROLE",
                table: "Teacher",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
