using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class StudentCourseMap : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.ToTable("StudentCourse");

        builder.HasKey(x => new {x.StudentId, x.CourseId});

        builder.Property(x => x.CourseId)
            .IsRequired()
            .HasColumnName("CourseId");
        
        builder.Property(x => x.StudentId)
            .IsRequired()
            .HasColumnName("StudentId");
            

        builder.Property(x => x.StartDate)
            .HasColumnName("StarDate")
            .HasColumnType("Datetime");

        builder.Property(x => x.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("Datetime");
        
        builder.Property(x => x.Progress)
            .HasColumnName("Progress")
            .HasColumnType("INT")
            .HasMaxLength(100);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.StudentCourses)
            .HasForeignKey(x => x.StudentId)
            .HasConstraintName("FK_STUDENTCOURSE_STUDENT")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Course)
            .WithMany(x => x.StudentsCourse)
            .HasForeignKey(x => x.CourseId)
            .HasConstraintName("FK_STUDENTCOURSE_COURSE")
            .OnDelete(DeleteBehavior.Cascade);
    }
}