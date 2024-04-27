using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerPenApi.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoIdioma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Contact = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Science = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Contact = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    SubjectId = table.Column<int>(type: "INT", nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Duration = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SUBJECT",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Duration = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Watched = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COURSE",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TEACHER",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "INT", maxLength: 100, nullable: false),
                    StarDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "Datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_STUDENTCOURSE_COURSE",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENTCOURSE_STUDENT",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_NAME",
                table: "Course",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubjectId",
                table: "Course",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TeacherId",
                table: "Lesson",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LESSON_TITLE",
                table: "Lesson",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_EMAIL",
                table: "Student",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_EMAIL",
                table: "Teacher",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    Datanasc = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
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
                    Tipo = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
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
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    Datanasc = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
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
                    CodMateria = table.Column<int>(type: "INT", nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CriadoEm = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Duracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Imagem = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false)
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
                    DtIncio = table.Column<DateTime>(type: "Datetime", nullable: false),
                    DtFinal = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Progresso = table.Column<int>(type: "INT", maxLength: 100, nullable: false)
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
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    Assistido = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    Duracao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    linkURL = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
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
    }
}
