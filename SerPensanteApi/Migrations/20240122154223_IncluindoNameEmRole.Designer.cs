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
    [DbContext(typeof(SpenDataContext))]
    [Migration("20240122154223_IncluindoNameEmRole")]
    partial class IncluindoNameEmRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SerPenApi.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Active");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<DateTime>("Duration")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Duration");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Image");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("linkURL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INT")
                        .HasColumnName("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex(new[] { "Name" }, "IX_COURSE_NAME")
                        .IsUnique();

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Duration")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Duration");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("linkURL");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Title");

                    b.Property<bool>("Watched")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Watched");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.HasIndex(new[] { "Title" }, "IX_LESSON_TITLE")
                        .IsUnique();

                    b.ToTable("Lesson", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 7000L);

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Active");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("BirthDate");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Contact");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "IX_STUDENT_EMAIL")
                        .IsUnique();

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentId");

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseId");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Datetime")
                        .HasColumnName("EndDate");

                    b.Property<int>("Progress")
                        .HasMaxLength(100)
                        .HasColumnType("INT")
                        .HasColumnName("Progress");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Datetime")
                        .HasColumnName("StarDate");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourse", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Science")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Science");

                    b.HasKey("Id");

                    b.ToTable("Subject", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 4000L);

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Active");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("BirthDate");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Contact");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "IX_TEACHER_EMAIL")
                        .IsUnique();

                    b.ToTable("Teacher", (string)null);
                });

            modelBuilder.Entity("SerPensanteApi.Models.Course", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_SUBJECT");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Lesson", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_COURSE");

                    b.HasOne("SerPensanteApi.Models.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TEACHER");

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Student", b =>
                {
                    b.HasOne("SerPenApi.Models.Role", "Role")
                        .WithMany("StudentRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_STUDENT_ROLE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SerPensanteApi.Models.StudentCourse", b =>
                {
                    b.HasOne("SerPensanteApi.Models.Course", "Course")
                        .WithMany("StudentsCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_STUDENTCOURSE_COURSE");

                    b.HasOne("SerPensanteApi.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_STUDENTCOURSE_STUDENT");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Teacher", b =>
                {
                    b.HasOne("SerPenApi.Models.Role", "Role")
                        .WithMany("TeacherRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TEACHER_ROLE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SerPenApi.Models.Role", b =>
                {
                    b.Navigation("StudentRoles");

                    b.Navigation("TeacherRoles");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Course", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("StudentsCourse");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Subject", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("SerPensanteApi.Models.Teacher", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
