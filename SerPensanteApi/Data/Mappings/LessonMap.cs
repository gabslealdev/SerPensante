using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerPensanteApi.Models;

namespace SerPensanteApi.Data.Mappings;

public class LessonMap : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lesson");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).ValueGeneratorOnAdd().UseIdenityColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Duration)
            .IsRequired()
            .HasColumnName("Duration")
            .HasColumnType("DATETIME");

        builder.Property(x => x.LinkUrl)
            .HasColumnName("linkURL")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Watched)
            .HasColumnType("BIT")
            .HasColumnName("Watched")
            .HasDefaultValue(0);

        builder.HasIndex(x => x.Title, "IX_LESSON_TITLE").IsUnique();  

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.TeacherId)
            .HasConstraintName("FK_TEACHER")
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Course)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.CourseId)
            .HasConstraintName("FK_COURSE")
            .OnDelete(DeleteBehavior.NoAction);
    }
}