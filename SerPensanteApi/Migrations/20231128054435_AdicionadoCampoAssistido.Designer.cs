﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SerPensanteApi.Data;

#nullable disable

namespace SerPenApi.Migrations
{
    [DbContext(typeof(SpensanteDataContext))]
    [Migration("20231128054435_AdicionadoCampoAssistido")]
    partial class AdicionadoCampoAssistido
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SerPensanteApi.Models.Aluno", b =>
                {
                    b.Property<int>("Matricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Matricula"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<DateTime>("Datanasc")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Datanasc");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Imagem");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Telefone");

                    b.HasKey("Matricula");

                    b.HasIndex(new[] { "Email" }, "IX_ALUNO_EMAIL")
                        .IsUnique();

                    b.ToTable("Aluno", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.AlunoCurso", b =>
                {
                    b.Property<int>("AlunoId")
                        .HasColumnType("int")
                        .HasColumnName("AlunoId");

                    b.Property<int>("CursoId")
                        .HasColumnType("int")
                        .HasColumnName("CursoId");

                    b.Property<DateTime>("DtInicio")
                        .HasColumnType("Datetime")
                        .HasColumnName("DtIncio");

                    b.Property<DateTime>("Dtfinal")
                        .HasColumnType("Datetime")
                        .HasColumnName("DtFinal");

                    b.Property<int>("Progresso")
                        .HasMaxLength(100)
                        .HasColumnType("INT")
                        .HasColumnName("Progresso");

                    b.HasKey("AlunoId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("AlunoCurso", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Aula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Assistido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Assistido");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Duracao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Duracao");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("linkURL");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Titulo");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex(new[] { "Titulo" }, "IX_AULA_TITULO")
                        .IsUnique();

                    b.ToTable("Aula", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<int>("CodMateria")
                        .HasColumnType("INT")
                        .HasColumnName("CodMateria");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CriadoEm");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Descricao");

                    b.Property<DateTime>("Duracao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Duracao");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Imagem");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("linkURL");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("CodMateria");

                    b.HasIndex(new[] { "Nome" }, "IX_CURSO_NOME")
                        .IsUnique();

                    b.ToTable("Curso", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.Property<byte>("Tipo")
                        .HasMaxLength(20)
                        .HasColumnType("TINYINT")
                        .HasColumnName("Tipo");

                    b.HasKey("Id");

                    b.ToTable("Materia", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Professor", b =>
                {
                    b.Property<int>("Matricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Matricula"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<DateTime>("Datanasc")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Datanasc");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Imagem");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Telefone");

                    b.HasKey("Matricula");

                    b.HasIndex(new[] { "Email" }, "IX_PROFESSOR_EMAIL")
                        .IsUnique();

                    b.ToTable("Professor", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.AlunoCurso", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Aluno", "Aluno")
                        .WithMany("AlunoCursos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ALUNOCURSO_ALUNO");

                    b.HasOne("SerPensanteApi.Models.Curso", "Curso")
                        .WithMany("AlunosCurso")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ALUNOCURSO_CURSO");

                    b.Navigation("Aluno");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Aula", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Curso", "Curso")
                        .WithMany("Aulas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_CURSO");

                    b.HasOne("SerPensanteApi.Models.Professor", "Professor")
                        .WithMany("Aulas")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_PROFESSOR");

                    b.Navigation("Curso");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Curso", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Materia", "Materia")
                        .WithMany("Cursos")
                        .HasForeignKey("CodMateria")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_MATERIA");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Aluno", b =>
                {
                    b.Navigation("AlunoCursos");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Curso", b =>
                {
                    b.Navigation("AlunosCurso");

                    b.Navigation("Aulas");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Materia", b =>
                {
                    b.Navigation("Cursos");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Professor", b =>
                {
                    b.Navigation("Aulas");
                });
#pragma warning restore 612, 618
        }
    }
}
