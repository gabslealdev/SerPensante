using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class ModeloInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    Datanasc = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Tipo = table.Column<byte>(type: "TINYINT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    Datanasc = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Duracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CriadoEm = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CodMateria = table.Column<int>(type: "INT", nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MATERIA",
                        column: x => x.CodMateria,
                        principalTable: "Materia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlunoCurso",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    Progresso = table.Column<int>(type: "INT", maxLength: 100, nullable: false),
                    DtIncio = table.Column<DateTime>(type: "Datetime", nullable: false),
                    DtFinal = table.Column<DateTime>(type: "Datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoCurso", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_ALUNOCURSO_ALUNO",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ALUNOCURSO_CURSO",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Duracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CURSO",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROFESSOR",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Matricula");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUNO_EMAIL",
                table: "Aluno",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlunoCurso_CursoId",
                table: "AlunoCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_CursoId",
                table: "Aula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_ProfessorId",
                table: "Aula",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AULA_TITULO",
                table: "Aula",
                column: "Titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curso_CodMateria",
                table: "Curso",
                column: "CodMateria");

            migrationBuilder.CreateIndex(
                name: "IX_CURSO_NOME",
                table: "Curso",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROFESSOR_EMAIL",
                table: "Professor",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoCurso");

            migrationBuilder.DropTable(
                name: "Aula");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Materia");
        }
    }
}
